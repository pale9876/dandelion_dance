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

    [Export] public State state = State.PHYSICS;
    [Export] public bool playing { set => set_play(value); get => _playing; }
    [Export] public bool repeat = false;
    [Export] public float fps { set => setFps(value); get => _fps; }
    [Export] public float time_scale { set => setTimeScale(value); get => _time_scale; }
    private float time { set => setTime(value); get => _time; }


    private bool _playing = false;
    private float _fps = 10.0f;
    private float _time = 0f;
    private float _time_scale = 1.0f;


    public override void _EnterTree()
    {
        if (!Engine.IsEditorHint())
        {
            var g_animation = GetNode<GlobalAnimation>("root/GlobalAnimation");
            g_animation.add_sprite(this);
        }
    }

    public override void _Process(double delta)
    {
        if (state == State.IDLE)
        {
            if (playing)
                time -= (float)delta * time_scale * GlobalAnimation.global_scale;
                next_frame();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (state == State.PHYSICS)
        {
            if (playing)
            {
                time -= (float)delta * time_scale * GlobalAnimation.global_scale;
                next_frame();
            }
        }
    }

    public bool next_frame()
    {
        if (FrameCoords.X < Hframes)
        {
            FrameCoords = FrameCoords with { X = FrameCoords.X + 1 };
            return true;
        }
        else
        {
            if (repeat)
            {
                FrameCoords = FrameCoords with { X = 0 };
                return true;
            }
        }
        return false;
    }

    public void setTimeScale(float scale)
    {
        time_scale = Mathf.Clamp(scale, 0.0f, 2.5f);
    }
    public void setTime(float value)
    {
        _time = Mathf.Max(0.0f, value);
    }

    public void setFps(float value)
    {
        fps = value;
        _time = 1f / value;
    }

    public void set_play(bool toggle)
    {
        playing = toggle;

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
        EmitSignalstart();
    }

    public void stop()
    {
        FrameCoords = FrameCoords with { X = 0 };
        EmitSignalstopped();
    }

}
