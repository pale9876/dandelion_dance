using Godot;
using System;

[Tool]
[GlobalClass]
public partial class UnitCollision : CollisionShape2D
{

    public int id = -1;

    public override void _EnterTree()
    {
        base._EnterTree();

        VisibilityChanged += on_visibility_changed;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        VisibilityChanged -= on_visibility_changed;
    }

    public void on_visibility_changed()
    {
        Disabled = (Visible) ? false : true;
    }

}
