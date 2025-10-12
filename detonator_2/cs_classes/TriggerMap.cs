using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class TriggerMap : Node
{
    [Signal] public delegate void trigger_activatedEventHandler(String trigger_name, bool result);

    [Export] public Dictionary<String, Trigger> triggers = new();
    [Export] public Trigger wheel = null;
    [Export] public Trigger targeting = null;

    public TriggerMap()
    {

    }

    public override void _EnterTree()
    {
        base._EnterTree();

        PoseComponent parent = GetParentOrNull<PoseComponent>();

        if (parent != null)
        {
            parent.trigger_map = this;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        PoseComponent parent = GetParentOrNull<PoseComponent>();

        if (parent != null)
        {
            parent.trigger_map = null;
        }

        wheel = null;
        triggers.Clear();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (!Engine.IsEditorHint())
        {
            if (wheel != null)
            {
                // activate_trigger
            }
        }
    }

    public void activate_trigger(Trigger trigger, Unit target)
    {
        if (trigger.keep_activate)
        {
            wheel = trigger;
                        
        }
        
        bool result = trigger.activate(target);
        EmitSignaltrigger_activated(trigger.trigger_name, result);
    }
}
