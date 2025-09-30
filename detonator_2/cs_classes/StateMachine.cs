using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class StateMachine : LimboHsm
{

    [Export] public Dictionary<String, UnitState> states = new Dictionary<string, UnitState>();
    [Export] public UnitState start_state = null;

    public override void _EnterTree()
    {
        base._EnterTree();


        Entity parent = GetParentOrNull<Entity>();

        if (!Engine.IsEditorHint())
        {
            if (parent != null)
            {
                Initialize(parent);
                SetActive(true);
            }
        }
    }


}
