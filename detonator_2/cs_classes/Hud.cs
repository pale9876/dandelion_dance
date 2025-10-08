using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Hud : Control
{

    public Hud()
    {
        MouseFilter = MouseFilterEnum.Ignore;
    }

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


    private void on_visibility_changed()
    {
        foreach (Node node in GetChildren())
        {
            if (node is Control)
                (node as Control).Visible = this.Visible;
        }
    }

}
