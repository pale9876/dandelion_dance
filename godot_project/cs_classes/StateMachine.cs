using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class StateMachine : LimboHsm
{
    // public const String TO_MOVE = "to_move";
    // public const String TO_IDLE = "to_idle";
    // public const String JUMP_UP = "jump_up";
    // public const String FALL_DOWN = "fall_down";

    // public const String ALWAYS_IDLE = "always_idle";
    // public const String ALWAYS_SHIFT = "always_shif";


    [Export] public Dictionary<String, UnitState> states = new Dictionary<string, UnitState>();
    [Export] public UnitState start_state = null;

    public override void _EnterTree()
    {
        base._EnterTree();

        var unit = GetParentOrNull<Unit>();
        if (unit != null)
        {
            unit.state_machine = this;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        var unit = GetParentOrNull<Unit>();
        if (unit != null)
        {
            unit.state_machine = null;
        }
        
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

            // AddTransition(ANYSTATE, states["Idle"], ALWAYS_IDLE);
            // AddTransition(states["Idle"], states["Move"], TO_MOVE);
            // AddTransition(states["Idle"], states["Jump"], JUMP_UP);
            // AddTransition(states["Idle"], states["Fall"], FALL_DOWN);

            // AddTransition(states["Move"], states["Idle"], TO_IDLE);
            // AddTransition(states["Move"], states["Jump"], JUMP_UP);
            // AddTransition(states["Move"], states["Fall"], FALL_DOWN);

            // // AddTransition(states[""], states[""],);
            // AddTransition(states["Jump"], states["Fall"], FALL_DOWN);
            // AddTransition(states["Jump"], states["Fall"], TO_IDLE);

            // AddTransition(states["Fall"], states["Idle"], TO_IDLE);
            // AddTransition(states["Fall"], states["Move"], TO_MOVE);

            InitialState = start_state;
            Initialize(parent);
            SetActive(true);
        }
    }

}
