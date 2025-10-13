using Godot;
using System;
using KoreanTyper;
using k_sex = KoreanTyper.stringExtensions;

using Godot.Collections;


[Tool]
[GlobalClass]
public partial class KoreanTypingEffector : Resource
{

    [Signal] public delegate void value_changedEventHandler();

    [Export(PropertyHint.MultilineText)] public String text { get => _text; set => setStr(value); }
    private String _text = "";
    
    public String out_text { set => setOutText(value); get => _out_text; }
    private String _out_text = "";

    public KoreanTypingEffector()
    {

    }

    public static KoreanTypingEffector create(String init_text)
    {
        KoreanTypingEffector res = new KoreanTypingEffector();
        res.text = init_text;
        res.out_text = "";

        return res;
    }

    private void setStr(String value)
    {
        _text = value;
        out_text = "";
    }

    private void setOutText(String value)
    {
        _out_text = value;
    }



}
