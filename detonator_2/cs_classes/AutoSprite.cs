using System;
using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class AutoSprite : Sprite2D
{
    public enum State
    {
        IDLE,
        PHYSICS,
    }

    private long id = -1;

    [Signal] public delegate void startEventHandler(String sprite_name);
    [Signal] public delegate void stoppedEventHandler(String sprite_name);
    [Signal] public delegate void finishedEventHandler(String sprite_name);
    [Signal] public delegate void loopedEventHandler(String sprite_name);

    [Export] public State state = State.PHYSICS;
    [Export] public bool playing { set => set_play(value); get => _playing; }
    [Export] public bool repeat = false;
    [Export] public float fps { set => setFps(value); get => _fps; }
    [Export] public float time_scale { set => setTimeScale(value); get => _time_scale; }
    [Export] public bool paused { set; get; }
    [Export] public Dictionary<int, String> trigger_lines = new();

    private float time { set => setTime(value); get => _time; }

    private bool _paused = false;
    private bool _playing = false;
    private float _fps = 10.0f;
    private float _time = 0f;
    private float _time_scale = 1.0f;

    public AutoSprite()
    {

    }

    public override void _EnterTree()
    {
        base._EnterTree();

        VisibilityChanged += on_visible_changed;

        AutoSpriteComponent parent = GetParentOrNull<AutoSpriteComponent>();
        if (parent != null)
        {
            if (!parent.Visible) Visible = false;
            parent.add_sprite(this);
            Renamed += parent.on_child_renamed;
        }

        if (!Engine.IsEditorHint())
        {
            if (parent != null)
            {
                start += parent.on_played;
                stopped += parent.on_stopped;
                looped += parent.on_looped;
                finished += parent.on_finished;
            }
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        VisibilityChanged -= on_visible_changed;

        AutoSpriteComponent parent = GetParentOrNull<AutoSpriteComponent>();
        if (parent != null)
        {
            parent.remove_sprite(this);
            Renamed -= parent.on_child_renamed;
        }

        if (!Engine.IsEditorHint())
        {
            if (parent != null)
            {
                start -= parent.on_played;
                stopped -= parent.on_stopped;
                looped -= parent.on_looped;
                finished -= parent.on_finished;
            }
        }

    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (state == State.IDLE)
        {
            if (playing && !paused)
            {
                time -= (float)delta * time_scale;
                if (time == 0.0f)
                {
                    time = 1.0f / fps;
                    next_frame();
                }
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (state == State.PHYSICS)
        {
            if (playing && !paused)
            {
                time -= (float)delta * time_scale;
                if (time == 0.0f)
                {
                    time = 1.0f / fps;
                    next_frame();
                }
            }
        }
    }

    public void on_visible_changed()
    {
        AutoSpriteComponent component = GetParentOrNull<AutoSpriteComponent>();
        if (component != null)
        {
            if (!component.Visible)
            {
                this.Visible = false;
                return;
            }
            
            if (Visible)
            {
                component.index = GetIndex();
            }
            else
            {
                if (component.current_sprite == this)
                {
                    component.index = -1;
                }
            }
        }

        playing = Visible ? true : false;
    }

    public void next_frame()
    {
        if (FrameCoords.X < Hframes - 1)
        {
            FrameCoords = FrameCoords with { X = FrameCoords.X + 1 };
        }
        else
        {
            if (repeat)
            {
                FrameCoords = FrameCoords with { X = 0 };
                EmitSignallooped(this.Name);
            }
            else
            {
                playing = false;
                EmitSignalfinished(this.Name);
            }
        }
    }

    public void setTimeScale(float scale)
    {
        _time_scale = Mathf.Clamp(scale, 0.0f, 2.5f);
    }
    public void setTime(float value)
    {
        _time = Mathf.Max(0.0f, value);
    }

    public void setFps(float value)
    {
        _fps = value;
        _time = 1f / value;
    }

    public void set_play(bool toggle)
    {
        _playing = toggle;

        if (toggle)
        {
            play();
        }
        else
        {
            stop();
        }
    }

    public void set_id(long id) => this.id = id;
    public long get_id() => id;

    public void play()
    {
        if (!paused)
        {
            FrameCoords = FrameCoords with { X = 0 };
            EmitSignalstart(this.Name);
        }
    }

    public void stop()
    {
        EmitSignalstopped(this.Name);
    }

}
