using Godot;
using System;

[Tool]
[GlobalClass]
public partial class UnitCollision : CollisionShape2D
{

    public override void _EnterTree()
    {
        VisibilityChanged += on_visibility_changed;
    }

    public override void _ExitTree()
    {
        VisibilityChanged -= on_visibility_changed;
    }

    public void on_visibility_changed()
    {
        
        Disabled = (Visible) ? false : true;
    }

}
