using Godot;
using System;

[Tool]
[GlobalClass]
public partial class StageRect : StaticBody2D
{

    [Export] private CollisionPolygon2D collision { get; set; }
    [Export] private Color debug_colour { get; set; }

    public override void _Draw()
    {
        base._Draw();
        
        if (collision != null)
        {
            var start_point = collision.Polygon[0];
            var end_point = collision.Polygon[2];

            DrawRect(
                new Rect2(start_point, end_point),
                debug_colour,
                true
            );
        }

    }

    public void set_collision(Vector2[] poly)
    {
        if (collision != null) collision.Polygon = poly;
    }

}
