using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class MarginArea : Area2D
{

    [Export] private CollisionPolygon2D collision;

    public override void _EnterTree()
    {
        this.ChildEnteredTree += on_child_entered;
    }

    public override void _ExitTree()
    {
        this.ChildEnteredTree -= on_child_entered;
    }

    public void on_child_entered(Node node)
    {
        if (node is CollisionPolygon2D)
            (node as CollisionPolygon2D).Visible = false;
    }
    
    public void set_collision(Vector2 margin, Vector2 pivot, Vector2 region)
    {
        if (collision != null)
        {
            Vector2 start_point = pivot - margin;
            Vector2 end_point = pivot + region + margin;

            var poly = new Vector2[4];
            
            poly[0] = start_point;
            poly[1] = new Vector2(end_point.X, start_point.Y);
            poly[2] = end_point;
            poly[3] = new Vector2(start_point.X, end_point.Y);

            collision.Polygon = poly;
        }
    }
}
