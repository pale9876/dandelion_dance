using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class PointComponent : Node2D
{
    [Export] public Array<Point> points = new Array<Point>();

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
        if (points.Count > 0)
        {
            foreach (Point point in points)
            {
                point.Visible = this.Visible;
            }
        }
    }


}
