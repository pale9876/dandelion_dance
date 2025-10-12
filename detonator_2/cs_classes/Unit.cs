using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class Unit : Entity
{
    public const float DEFAULT_FRICTION = 2250.0f;
    public const float DEFAULT_ACCELERATION = 3350.0f;
    public const float DEFAULT_JUMP_FORCE = -620.0f;

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

    [Export] public bool hand_enable = false;

    [Export] public Dictionary<String, UnitCollision> collisions = new Dictionary<string, UnitCollision>();

    [Export] private Color debug_colour { get => _debug_color; set => change_debug_color(value); }
    private Color _debug_color = new Color();
    [Export] public PoseComponent pose_component = null;
    [Export] public BodyPartComponent body_part_component;
    [Export] public PsychoValuement psycho_valuement;
    [Export] public StateMachine state_machine = null;
    [Export] public BTPlayer bt_player = null;
    [Export] public TriggerMap trigger_map = null;

    [Export] public UnitInfo unit_info = null;
    [Export] public bool invincible = false;
    [Export] public bool throughable { get => _throughable; set => setThroughable(value); }
    private bool _throughable = false;

    private AirState airstate = AirState.NONE;

    private State state { get => _state; set => change_state(value); }
    private State _state = State.NORMAL;
    public Dictionary<String, Variant> abnormals = new();

    private bool pre_velocity_init = false;
    private Vector2 pre_velocity { get => _pre_velocity; set => setPreVelocity(value); }
    private Vector2 _pre_velocity = Vector2.Zero;

    // [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();

        ChildEnteredTree += on_child_entered;

        if (!Engine.IsEditorHint())
        {
            GD.Print("ADD Mouse Signals");
            this.MouseEntered += on_mouse_entered;
            this.MouseExited += on_mouse_exited;
            get_g_anim().time_scale_changed += on_global_time_scale_changed;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        collisions.Clear();

        ChildEnteredTree -= on_child_entered;

        if (!Engine.IsEditorHint())
        {
            this.MouseEntered -= on_mouse_entered;
            this.MouseExited -= on_mouse_exited;
            get_g_anim().time_scale_changed -= on_global_time_scale_changed;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        // _update();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Engine.IsEditorHint()) return;

    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Engine.IsEditorHint()) return;

        // Array<AutoSprite> current_sprites = pose_component.current_pose.get_current_sprites();

        // foreach (AutoSprite sprite in current_sprites)
        // {
        //     var frame_coord_x = sprite.FrameCoords.X;
        //     if (sprite.trigger_lines.ContainsKey(frame_coord_x))
        //     {
        //         trigger_map.activate_trigger(sprite.trigger_lines[frame_coord_x], this);
        //     }
        // }

        if (pre_velocity_init)
            Velocity = pre_velocity;


        if (!IsOnFloor())
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
        else if (IsOnFloor())
        {
            airstate = AirState.NONE;
        }

        switch (state)
        {
            case State.NORMAL:
                // mns_with_global();
                MoveAndSlide();
                break;
            case State.GRABBED:
                grabbed_event_handler();
                break;
        }

        _update_info();

    }

    public void move(double delta)
    {
        Velocity = Velocity with
        {
            X = (float)Mathf.MoveToward(
                Velocity.X,
                (unit_info != null) ? unit_info.speed : 300.0 * get_p_input().get_current_direction().X,
                delta * DEFAULT_ACCELERATION
            )
        };
    }

    public void idle(double delta)
    {
        if (Velocity.X != 0.0)
        {
            Velocity = Velocity with
            {
                X = (float)Mathf.MoveToward(
                    Velocity.X,
                    0.0,
                    delta * DEFAULT_FRICTION
                )
            };
        }
    }

    public void jump()
    {
        init_velocity(false, Velocity with { X = Velocity.X, Y = -DEFAULT_JUMP_FORCE });
    }

    public virtual void grabbed_event_handler()
    {

    }

    public virtual void on_global_time_scale_changed(float value)
    {

    }

    private void on_mouse_entered()
    {
        get_p_input().mouse_pointing.Add(this);
        GD.Print($"Add MousePointing => {this}");
    }

    private void on_mouse_exited()
    {
        get_p_input().mouse_pointing.Remove(this);
        GD.Print($"Exit MousePointing => {this}");
    }

    public void on_child_entered(Node node)
    {
        if (node is UnitCollision)
        {
            if (collisions.ContainsKey(node.Name)) return;

            collisions.Add(node.Name, node as UnitCollision);
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

    public void setPreVelocity(Vector2 value)
    {
        _pre_velocity = (pre_velocity_init) ? value : _pre_velocity + value;
        pre_velocity_init = true;
    }

    public void setThroughable(bool toggle)
    {
        _throughable = toggle;
        CollisionLayer = (uint)((toggle) ? 0 : 1);

    }

    public Vector2 init_velocity(bool keep, Vector2 value) => pre_velocity = (keep) ? Velocity + value : value;

    private Array<UnitCollision> get_collisions() => collisions.Values as Array<UnitCollision>;

    private float get_gravity(double delta) => Velocity.Y + ((float)delta * DEFAULT_GRAVITY);


}
