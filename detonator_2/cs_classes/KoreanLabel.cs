using Godot;
using Godot.Collections;
using System;
using KoreanTyper;
using k_sex = KoreanTyper.stringExtensions;
using SmartFormat;
using SmartFormat.Extensions;
using Godot.NativeInterop;

[Tool]
[GlobalClass]
public partial class KoreanLabel : Label
{    
    [Signal] public delegate void finishedEventHandler();

    [Export(PropertyHint.MultilineText)] private String target_text = "";
    [ExportToolButton("!INDEXING")] private Callable execute_indexing => Callable.From(josa_indexing);
    [Export] public Dictionary<int, String> dict = new();

    [ExportToolButton("!FORMAT")] private Callable _format_josa => Callable.From(format_josa);

    [Export] public KoreanTypingEffector effector;

    [Export] public bool playing { get => _playing; set => Play(value); }
    private bool _playing = false;
    [Export] public float play_scale = 1.0f;

    [Export(PropertyHint.Range, "0.0, 1.0, 0.001")] public float progress { get => _progress; set => setProgress(value); }
    private float _progress = 0.0f;

    public KoreanLabel()
    {
        effector = new KoreanTypingEffector();
    }

    public override void _Process(double _delta)
    {
        if (playing)
        {
            if (effector != null)
            {
                progress += play_scale * 0.01f;
                if (Text == effector.text)
                {
                    EmitSignalfinished();
                    playing = false;
                }
            }
        }
    }

    private void Play(bool toggle)
    {
        _playing = toggle;
        if (toggle)
        {
            if (_progress == 1.0f)
            {
                _progress = 0.0f;
            }
        }
    }

    private void setProgress(float value)
    {
        _progress = (float)Mathf.Clamp(value, 0.0, 1.0);
        if (effector != null)
        {
            effector.out_text = k_sex.Typing(effector.text, _progress);
            this.Text = effector.out_text;
        }
    }

    public void format_josa()
    {
        String result = target_text;

        if (dict.Count > 0)
        {
            String[] arr = target_text.Split(" ");

            foreach (String s in arr)
            {
                RegEx rex = new RegEx();
                rex.Compile("(?<={)(.*)(?=})");
                RegExMatch search = rex.Search(s);
                bool is_arg = (search != null) ? true : false;

                if (is_arg)
                {
                    int idx = s[1].ToString().ToInt();
                    String new_str = s.Replace(s[1], '0');
                    var formatted = Smart.Format(new_str, dict[idx]);
                    result = result.Replace(s, formatted);
                }
            }
        }

        effector.text = result;
    }

    public void josa_indexing()
    {
        if (target_text.Length > 0)
        {
            dict.Clear();

            String[] arr = target_text.Split(" ");

            foreach (String s in arr)
            {
                RegEx rex = new RegEx();
                rex.Compile("(?<={)(.*)(?=})");
                RegExMatch search = rex.Search(s);
                String result = (search != null) ? search.GetString() : "";
                if (result.Length > 0)
                {
                    String[] idx_n_josa = result.Split(":");
                    int idx = idx_n_josa[0].ToInt();
                    dict.Add(idx, "");
                }
            }
        }
    }
}
