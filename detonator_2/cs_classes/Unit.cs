using Godot;
using Godot.Collections;
using System;

[Tool]
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

    enum State
    {
        NORMAL,
        GRABBED,
    }


    [Signal] public delegate void health_changedEventHandler(int value);
    [Signal] public delegate void max_health_changedEventHandler(int value);
    [Signal] public delegate void collision_changedEventHandler();

    [Export] public Dictionary<String, UnitCollision> collisions = new Dictionary<string, UnitCollision>();

    [Export] private Color debug_colour { get => _debug_color; set => change_debug_color(value); }
    private Color _debug_color = new Color();
    [Export] public PoseComponent pose_component;
    [Export] public BodyPartComponent body_part_component;
    [Export] public PsychoValuement psycho_valuement;
    [Export] public StateMachine state_machine = null;
    [Export] public bool invincible = false;

    private State state { get => _state; set => change_state(value); }
    private State _state = State.NORMAL;
    public Dictionary abnormals;

    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();

        ChildEnteredTree += on_child_entered;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        ChildEnteredTree -= on_child_entered;
    }

    public override void _Ready()
    {
        base._Ready();

        _update();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Engine.IsEditorHint()) return;

        switch (state)
        {
            case State.NORMAL:
                mns_with_global();
                break;
            case State.GRABBED:
                grabbed_event_handler();
                break;
        }
    }

    public void grabbed_event_handler()
    {

    }

    public void on_child_entered(Node node)
    {
        if (node is UnitCollision)
        {
            if (collisions.ContainsKey(node.Name)) return;
            collisions.Add(node.Name, node as UnitCollision);
        }
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

    private void change_state(State st)
    {
        if (_state != st)
        {
            switch (st)
            {
                case State.GRABBED:

                    break;
                case State.NORMAL:
                    break;
            }
        }

        _state = st;
    }

    public void grabbed(Node node)
    {
        state = State.GRABBED;
        abnormals.Add("grabbed_by", node);
    }

    public void change_debug_color(Color color)
    {
        _debug_color = color;

        foreach (UnitCollision collision in get_collisions())
            collision.DebugColor = color;
    }

    public Array<UnitCollision> get_collisions() => collisions.Values as Array<UnitCollision>;

}
