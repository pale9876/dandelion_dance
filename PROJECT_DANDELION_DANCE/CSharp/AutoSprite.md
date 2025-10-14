###### Extends Sprite2D
---

해당 객체를 이해하기 위해서는 Sprite2D에 대한 이해가 필요합니다.

AutoSprite는 AnimatedSprite2D와 다르게 스프라이트 애니메이션을 재생시킬 수 있는 Sprite2D 객체입니다.

Sprite2D의 frame 인수의 증가가 아닌 FrameCoord.X 인수의 증가로 애니메이션이 진행됩니다. 재생에 있어서 좌표값의 증감으로 진행하게 한 이유는 Y축에 여러 스프라이트 바리에이션을 넣기 위함입니다.

``` CSharp

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
    [Signal] public delegate void reached_trigger_lineEventHandler(String trigger_name);

    [Export] public State state = State.PHYSICS;
    [Export] public bool auto_start = false;
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
            if (!parent.Visible) { Visible = false; }
            parent.add_sprite(this);
            Renamed += parent.on_child_renamed;

            if (!Engine.IsEditorHint())
            {
                start += parent.on_played;
                stopped += parent.on_stopped;
                looped += parent.on_looped;
                finished += parent.on_finished;

                if (parent.root_pose != null)
                {
                    reached_trigger_line -= parent.root_pose.on_reached_trigger_line;
                }
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

            if (!Engine.IsEditorHint())
            {
                start -= parent.on_played;
                stopped -= parent.on_stopped;
                looped -= parent.on_looped;
                finished -= parent.on_finished;

                if (parent.root_pose != null)
                {
                    reached_trigger_line -= parent.root_pose.on_reached_trigger_line;
                }
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

  

        playing = (Visible && auto_start) ? true : false;

    }

  

    public void next_frame()

    {

        int current_frame_x = FrameCoords.X;

  

        if (current_frame_x < Hframes - 1)

        {

            FrameCoords = FrameCoords with { X = current_frame_x + 1 };

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

  

        if (!Engine.IsEditorHint())

        {

            if (trigger_lines.ContainsKey(FrameCoords.X))

            {

                EmitSignalreached_trigger_line(trigger_lines[FrameCoords.X]);

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

```