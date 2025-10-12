using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Trigger : Resource
{
    public enum ErrReason
    {
        WORK = 0,
        TARGET_NULL = 1,
        OUT_OF_RANGE = 2,
        NOT_CONTAINS = 3,
    }

    [Export] public String trigger_name = "";
    [Export] public bool keep_activate = false;
    [Export] public bool keep_target = false;
    public ErrReason err_reason = ErrReason.WORK;

    public virtual bool activate(Node2D target)
    {
        if (target == null)
        {
            err_reason = ErrReason.TARGET_NULL;
            return false;
        }

        return true;
    }

}
