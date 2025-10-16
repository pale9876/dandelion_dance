using Godot;
using System;

[Tool]
public partial class UnitState : LimboState
{

    public const String PURPOSE_DIR_VAR = "PurposeDirection";

    public bool cooldown { get => _cooltime > 0.0; }
    [Export] public float initial_cooltime = 0.0f;
    private float cooltime { get => _cooltime; set => setCoolTime(value); }
    private float _cooltime = 0.0f;

    public bool abort = false;

    public override void _EnterTree()
    {
        base._EnterTree();

        StateMachine parent = GetParent<StateMachine>();
        if (parent != null)
        {
            if (!parent.states.ContainsKey(this.Name))
            {
                parent.states.Add(this.Name, this);
                if (parent.states.Count == 1)
                {
                    parent.start_state = this;
                }
            }
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        StateMachine parent = GetParent<StateMachine>();
        if (parent != null)
        {
            if (parent.states.ContainsKey(this.Name))
            {
                parent.states.Remove(this.Name);
                if (parent.start_state == this)
                {
                    parent.start_state = null;
                }
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (_cooltime > 0.0)
        {
            _cooltime -= (float)delta;
        }
    }

    public override void _Enter()
    {
        base._Enter();

        if (initial_cooltime > 0.0f)
        {
            _cooltime = initial_cooltime;
        }
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        if (Engine.IsEditorHint()) return;

    }

    public override void _Exit()
    {
        base._Exit();

        if (Engine.IsEditorHint()) return;

        if (initial_cooltime > 0.0f)
        {
            cooltime = initial_cooltime;
        }
    }

    public void setCoolTime(float value)
    {
        _cooltime = (float)Mathf.Max(value, 0.0);
    }

    public Vector2 get_direction() => (get_unit().get_p_input().in_control == get_unit()) ?
        get_unit().get_p_input().get_current_direction() : get_state_machine().Blackboard.GetVar("PurposeDirection").As<Vector2>();

    public Unit get_unit() => Agent as Unit;

    public StateMachine get_state_machine() => GetRoot() as StateMachine;
}
