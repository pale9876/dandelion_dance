using System.Security.Cryptography.X509Certificates;
using Godot;

[Tool]
[GlobalClass]
public partial class AutoSprite : Sprite2D
{
    public enum State
    {
        IDLE,
        PHYSICS,
    }
    
    [Signal] public delegate void startEventHandler();
    [Signal] public delegate void stoppedEventHandler();
    [Signal] public delegate void finishedEventHandler();
    [Signal] public delegate void roopedEventHandler();

    [Export] public State state = State.PHYSICS;
    [Export] public bool playing { set => set_play(value); get => _playing; }
    [Export] public bool repeat = false;
    [Export] public float fps { set => setFps(value); get => _fps; }
    [Export] public float time_scale { set => setTimeScale(value); get => _time_scale; }
    [Export] public bool paused { set; get; }

    private float time { set => setTime(value); get => _time; }

    private bool _paused = false;
    private bool _playing = false;
    private float _fps = 10.0f;
    private float _time = 0f;
    private float _time_scale = 1.0f;


    public override void _EnterTree()
    {
        base._EnterTree();

        if (!Engine.IsEditorHint())
        {
            var g_animation = GetNode<GlobalAnimation>("root/GlobalAnimation");
            g_animation.add_sprite(this);
        }

        VisibilityChanged += on_visible_changed;

    }

    public override void _ExitTree()
    {
        base._ExitTree();

        VisibilityChanged -= on_visible_changed;
    }

    public override void _Process(double delta)
    {
        if (state == State.IDLE)
        {
            if (playing) time -= (float)delta * time_scale * GlobalAnimation.global_scale;
            if (time == 0.0f)
            {
                time = 1.0f / fps;
                next_frame();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (state == State.PHYSICS)
        {
            if (playing)
            {
                time -= (float)delta * time_scale * GlobalAnimation.global_scale;
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
        var parent = GetParentOrNull<AutoSpriteComponent>();
        if (parent != null)
        {
            if (parent.Visible != this.Visible)
            {
                GD.PrintErr($"{this} => 이 노드는 상위 노드와 Visible이 상이할 수 없음.");
                this.Visible = parent.Visible;
            }
        }
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
                EmitSignalrooped();
            }
            else
            {
                playing = false;
                EmitSignalfinished();
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

    public void play()
    {
        if (!paused)
        {
            FrameCoords = FrameCoords with { X = 0 };
            EmitSignalstart();
        }
    }

    public void stop()
    {
        EmitSignalstopped();
    }

}
