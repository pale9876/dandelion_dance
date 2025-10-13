using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class Lazor2D : Line2D
{
    [Export] public bool active { get => _active; set => setActive(value); }
    private bool _active = false;

    [Export] public Vector2 start_point { get => _start_point; set => setStartPoint(value); }
    private Vector2 _start_point = Vector2.Zero;
    [Export] public Vector2 end_point { get => _end_point; set => setEndPoint(value); }
    private Vector2 _end_point = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Engine.IsEditorHint()) return;

        if (active)
        {
            cast();
        }

    }

    private bool cast()
    {
        PhysicsRayQueryParameters2D query = PhysicsRayQueryParameters2D.Create(
            start_point,
            end_point,
            0,
            [GetParent<Unit>().GetRid()]
        );

        Dictionary collision = GetWorld2D().DirectSpaceState.IntersectRay(query);
        if (collision.Count > 0)
        {

            return true;
        }

        return false;
    }

    private void setActive(bool toggle)
    {
        _active = toggle;
    }

    private void setStartPoint(Vector2 value)
    {
        _start_point = value;
    }

    private void setEndPoint(Vector2 value)
    {
        _end_point = value;
    }

}
