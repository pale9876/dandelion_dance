using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Trigger : Resource
{
    public virtual bool activate(Unit target)
    {
        if (target == null) return false;
        
        return true;
    }

}
