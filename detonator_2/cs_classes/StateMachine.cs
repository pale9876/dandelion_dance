using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class StateMachine : LimboHsm
{

    [Export] public Dictionary<String, UnitState> states = new Dictionary<string, UnitState>();
    [Export] public UnitState start_state = null;

    public override void _Ready()
    {
        base._Ready();

        Entity parent = GetParentOrNull<Entity>();

        if (!Engine.IsEditorHint())
        {
            if (parent != null)
            {
                InitialState = start_state;
                Initialize(parent);
                SetActive(true);
            }
            else
            {
                GD.PrintErr($"{this} => Has no parent in this tree.");    
            }
        }
    }
}
