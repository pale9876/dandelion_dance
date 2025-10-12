using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Trigger : Resource
{
    [Export] public String trigger_name = "";
    [Export] public bool keep_activate = false;
    [Export] public bool keep_target = false;

    public virtual bool activate(Unit target)
    {
        if (target == null) return false;

        return true;
    }

}
