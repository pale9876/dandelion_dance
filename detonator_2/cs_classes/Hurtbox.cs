using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Hurtbox : Area2D
{
    public enum State
    {
        NORMAL,
        UNGRABBED,
        DREADNOUGHT,
        INVINCIBLE,
    }

    private int index = -1;

    [Export] private Array<UnitCollision> collisions = new Array<UnitCollision>();
    [Export] private Color debug_colour { get => _debug_color; set => set_debug_color(value); }
    private Color _debug_color = new Color();

    public State state = State.NORMAL;

    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();

        _update();

        VisibilityChanged += on_visibility_changed;

    }

    public override void _ExitTree()
    {
        base._ExitTree();
        index = -1;

        VisibilityChanged -= on_visibility_changed;
    }

    private void _update()
    {
        collisions.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is UnitCollision)
            {
                collisions.Add(node as UnitCollision);
            }
        }
    }

    public void set_debug_color(Color color)
    {
        _debug_color = color;
        if (collisions.Count != 0)
        {
            foreach (UnitCollision collision in collisions)
            {
                collision.DebugColor = color;
            }
        }
    }

    private void on_visibility_changed()
    {
        foreach (UnitCollision collision in collisions)
        {
            collision.Visible = Visible;
        }
    }

}
