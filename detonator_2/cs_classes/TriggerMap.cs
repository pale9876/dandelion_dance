using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class TriggerMap : Node
{
    [Signal] public delegate void trigger_activatedEventHandler(String trigger_name, bool result);

    [Export] public Dictionary<String, Trigger> triggers = new();

    public TriggerMap()
    {

    }

    public override void _EnterTree()
    {
        base._EnterTree();

        Unit parent = GetParentOrNull<Unit>();

        if (parent != null)
        {
            parent.trigger_map = this;   
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        Unit parent = GetParentOrNull<Unit>();

        if (parent != null)
        {
            parent.trigger_map = null;
        }
    }

    public void activate_trigger(String trigger_name, Unit target)
    {
        if (triggers.ContainsKey(trigger_name))
        {
            bool result = triggers[trigger_name].activate(target);
            EmitSignaltrigger_activated(trigger_name, result);
        }
    }

}
