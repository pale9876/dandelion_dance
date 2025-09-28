using Godot;
using System;


[Tool]
[GlobalClass]
public partial class AutoSprite : Sprite2D
{
    [Signal] public delegate void startEventHandler();
    [Signal] public delegate void stoppedEventHandler();

    public enum State{
        IDLE,
        PHYSICS,
    }

    [Export] public State state = State.PHYSICS;
    [Export] public bool playing = false;
    [Export] public bool repeat = false;
    private bool _play { set => set_play(value); get => playing; }

    public override void _Process(double delta)
    {
        if (state == State.IDLE)
        {
            if (playing)
                next_frame();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (state == State.PHYSICS)
        {
            if (playing)
                next_frame();
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
