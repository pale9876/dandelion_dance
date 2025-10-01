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

    private const float DEFAULT_GRAVITY = 970.0f;
    private const float MAX_GRAVITY = 3550.0f;

    public enum AirState
    {
        NONE = 0,
        JUMPUP = 1, // 상승
        DEFER = 2,// JUMPUP과 FALLDOWN의 중간값. 상태를 정의할 수 없으니 보류함.
        FALLDOWN = 3, // 하강
        AIRBORN = 4, // 공중에 떠있는 상태로 정지함.
    }

    public enum State
    {
        SPAWN = 0, // 스폰중
        NORMAL = 1, // 통상
        GRABBED = 2, // 잡기당함
        DEAD = 3, // 죽음
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
    [Export] public bool throughable { get => _throughable; set => setThroughable(value); }
    private bool _throughable = false;


    private AirState airstate = AirState.NONE;

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

        if (!this.IsOnFloor())
        {
            airstate = (airstate == AirState.NONE) ? AirState.JUMPUP : AirState.DEFER;

            if (!(airstate == AirState.AIRBORN))
            {
                Velocity = Velocity with
                {
                    X = Velocity.X,
                    Y = Mathf.Min(
                            get_gravity(delta), MAX_GRAVITY
                        )
                };
            }

            airstate = (Velocity.Y > 0.0f) ? // Y축 운동량이 아래로
                AirState.FALLDOWN : (Velocity.Y < 0.0f) ? // Y축 운동량이 위로
                AirState.JUMPUP : AirState.DEFER;
        }
        else
        {
            airstate = AirState.NONE;
        }

        switch (state)
        {
            case State.NORMAL:
                mns_with_global();
                break;
            case State.GRABBED:
                grabbed_event_handler();
                break;
        }

        _update_info();

    }

    public void jump()
    {
        
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

    public void setThroughable(bool toggle)
    {
        _throughable = toggle;
        if (toggle)
        {

        }
    }

    private Array<UnitCollision> get_collisions() => collisions.Values as Array<UnitCollision>;

    private float get_gravity(double delta) => Velocity.Y + ((float)delta * DEFAULT_GRAVITY);

}
