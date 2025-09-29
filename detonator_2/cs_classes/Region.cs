using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Region : Node2D
{
    
    // SETGET Props
    [Export] private Vector2 _pivot_point { get => pivot_point; set => pivot_changed(value); }
    [Export] private Vector2 _margin { get => margin; set => set_margin(value); }
    [Export] private Vector2 _size { set => region_changed(value); get => size; }
    [Export] private Color _debug_color { set => set_debug_color(value); get => debug_colour; }
    [Export] public Color alert_colour { get; set; }
    
    // export props
    public Vector2 pivot_point { get; set; }
    public Vector2 margin = Vector2.Zero;
    public Vector2 size { get; set; }
    public Color debug_colour { get; set; }

    // Export node
    [Export] public MarginArea margin_area { get; set; }


    //Tool Button
    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            margin_area.BodyEntered += on_body_entered;
            margin_area.BodyExited += on_body_exited;
        }
    }

    public override void _Draw()
    {

        if (Engine.IsEditorHint())
        {
            DrawRect(
                new Rect2(pivot_point, size),
                debug_colour,
                true
                );

            DrawRect(
                new Rect2(pivot_point - margin, size + (margin * 2)),
                alert_colour,
                true
                );
        }
    }

    private void _update()
    {
        if (margin_area != null)
        {
            margin_area.set_collision(margin, this.GlobalPosition + pivot_point, size);
        }

        QueueRedraw();
        GD.Print($"{this.Name} has Update");
    }

    public void on_body_entered(Node2D node)
    {
        if (node is Entity)
        {
            var entity = (Entity)node;
            entity.ghost = false;
        }
    }

    public void on_body_exited(Node2D node)
    {
        if (node is Entity)
        {
            var entity = (Entity)node;
            entity.ghost = true;
        }
    }

    // SETGET
    public void set_margin(Vector2 value)
    {
        margin = value;
    }

    public void pivot_changed(Vector2 point)
    {
        pivot_point = point;
        QueueRedraw();
    }

    public void set_debug_color(Color color)
    {
        debug_colour = color;
        QueueRedraw();
    }

    public void region_changed(Vector2 value)
    {
        size = value;
        QueueRedraw();
    }

}
