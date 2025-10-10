using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class StateMachine : LimboHsm
{
    public const String TO_MOVE = "to_move";
    public const String TO_IDLE = "to_idle";
    public const String JUMP_UP = "jump_up";
    public const String FALL_DOWN = "fall_down";

    public const String ALWAYS_IDLE = "always_idle";
    public const String ALWAYS_MOVE = "always_move";
    public const String ALWAYS_SHIFT = "always_shif";

    [Export] public Dictionary<String, UnitState> states = new Dictionary<string, UnitState>();
    [Export] public UnitState start_state = null;

    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();

        var unit = GetParentOrNull<Unit>();
        if (unit != null)
            unit.state_machine = this;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        var unit = GetParentOrNull<Unit>();
        if (unit != null)
            unit.state_machine = null;
        
        states.Clear();

    }

    public override void _Ready()
    {
        base._Ready();

        Unit parent = GetParentOrNull<Unit>();


        if (!Engine.IsEditorHint())
        {
            if (parent == null)
            {
                GD.PrintErr($"{this} => Has no parent in this tree.");
                return;
            }

            if (states.ContainsKey("IdleState"))
                this.AddTransition(ANYSTATE, states["Idle"], TO_IDLE);

            InitialState = start_state;
            Initialize(parent);
            SetActive(true);
        }
    }

    public void _update()
    {
        if (states.Count > 0) states.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is UnitState)
                states.Add(node.Name, node as UnitState);
        }
    }
}
