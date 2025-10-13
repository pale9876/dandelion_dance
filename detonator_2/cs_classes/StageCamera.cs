using Godot;
using System;

[Tool]
[GlobalClass]
public partial class StageCamera : Camera2D
{
    public enum ShakeMode
    {
        HORZ = 0, // 가로
        Vert = 1, // 세로
        PLUS_ONE = 2, // Vector2(1,1)
        MINUS_ONE = 3, // Vector2(-1, 1)
        CHAOS = 2,
    }

    public enum FollowMode
    {
        IMMEDIATE,
        SMOOTH,
    }

    private int id = -1;

    [Export] public FollowMode follow_mode = FollowMode.IMMEDIATE;
    [Export] public Node2D target = null;
    public Node2D old_target = null;
    private Vector2 old_pos = Vector2.Zero;

    private double time { get => _time; set => _shake_start(value); }
    private double _time = 0.0;

    public override void _EnterTree()
    {
        base._EnterTree();
        if (!Engine.IsEditorHint())
        {
            get_status().insert_camera(this);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        if (!Engine.IsEditorHint())
        {
            // get_status().insert_camera(this);
        }

    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (target != null)
        {
            if (follow_mode == FollowMode.IMMEDIATE)
            {
                this.GlobalPosition = target.GlobalPosition;
            }
        }

        if (time > 0.0)
        {
            time -= delta;
        }
    }

    private void _shake_start(double t)
    {
        _time = Mathf.Max(t, 0.0);
        old_pos = Position;
    }

    public CameraStatus get_status() => GetNode<CameraStatus>("/root/CameraStatus");

}
