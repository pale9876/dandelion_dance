using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class Unit : Entity
{
    public const String TO_MOVE = "to_move";
    public const String TO_IDLE = "to_idle";
    public const String JUMP_UP = "jump_up";
    public const String FALL_DOWN = "fall_down";

    public const String ALWAYS_IDLE = "always_idle";
    public const String ALWAYS_MOVE = "always_move";
    public const String ALWAYS_SHIFT = "always_shif";


    public const float DEFAULT_FRICTION = 2250.0f;
    public const float DEFAULT_ACCELERATION = 3350.0f;

    const float MAX_GRAVITY = 3550.0f;

    public enum AirState
    {
        NONE,
        JUMPUP,
        AIRBORN,
        FALLDOWN,
    }

    [Signal] public delegate void health_changedEventHandler(int value);
    [Signal] public delegate void max_health_changedEventHandler(int value);
    [Signal] public delegate void collision_changedEventHandler();

    [Export] public PoseComponent pose_component = null;
    [Export] public Dictionary<String, UnitCollision> collisions;

    public override void _Ready()
    {
        _update();
    }

    public override void _PhysicsProcess(double delta)
    {

        mns_with_global();
        
    }


    public void _update()
    {
        collisions.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is UnitCollision)
            {
                collisions.Add(node.Name, node as UnitCollision);
            }
        }
    }


}
