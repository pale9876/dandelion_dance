using Godot;
using System;

[Tool]
[GlobalClass]
public partial class StateLabel : Label
{
    [Export] public bool debug = true;
    [Export] public Unit root = null;

    public override void _EnterTree()
    {
        base._EnterTree();

        Unit parent = GetParentOrNull<Unit>();

        if (parent != null)
        {
            root = parent;
            if (!Engine.IsEditorHint())
            {
                if (parent.state_machine != null)
                {
                    parent.state_machine.ActiveStateChanged += on_active_state_changed;
                }
            }
        }

    }

    public override void _ExitTree()
    {
        base._ExitTree();

        Unit parent = GetParentOrNull<Unit>();
        
        if (parent != null)
        {
            root = null;
            if (!Engine.IsEditorHint())
            {
                if (parent.state_machine != null)
                {
                    parent.state_machine.ActiveStateChanged -= on_active_state_changed;
                }
            }
        }

    }

    private void on_active_state_changed(LimboState current, LimboState previous)
    {
        var parent = GetParentOrNull<Unit>();

        if (debug)
        {
            if (previous != null)
            {
                GD.Print($"{parent.Name} Change State ! {previous.Name} => {current.Name}");
            }
        }

        Text = current.Name;

    }


}
