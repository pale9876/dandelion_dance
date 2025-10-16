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

    [Export] public Array<UnitCollision> collisions = new Array<UnitCollision>();
    [Export] private Color debug_colour { get => _debug_color; set => set_debug_color(value); }
    private Color _debug_color = new Color();

    [Export] public State state = State.NORMAL;

    public override void _EnterTree()
    {
        base._EnterTree();

        Pose parent = GetParentOrNull<Pose>();
        if (parent != null)
            parent.hurtbox = this;

        VisibilityChanged += on_visibility_changed;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        index = -1;

        Pose parent = GetParentOrNull<Pose>();
        if (parent != null)
            parent.hurtbox = null;
        
        VisibilityChanged -= on_visibility_changed;
        collisions.Clear();
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
