using Godot;
using System;

[Tool]
public partial class UnitBt : BTPlayer
{

    public override void _EnterTree()
    {
        base._EnterTree();

        Unit parent = GetParentOrNull<Unit>();
        if (parent != null)
        {
            parent.bt_player = this;
        }

    }

    public override void _ExitTree()
    {
        base._ExitTree();

        Unit parent = GetParentOrNull<Unit>();
        if (parent != null)
        {
            parent.bt_player = null;
        }
    }

}
