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

        Node parent = GetParent();

        if (parent != null)
        {
            if (parent is Hitbox)
            {
                if (!(parent as Hitbox).collisions.Contains(this)) (parent as Hitbox).collisions.Add(this);
            }

            else if (parent is Hurtbox)
            {
                if (!(parent as Hurtbox).collisions.Contains(this))
                {
                    (parent as Hurtbox).collisions.Add(this);
                }
            }
            else if (parent is Unit)
            {
                if (!(parent as Unit).collisions.ContainsKey(this.Name))
                {
                    (parent as Unit).collisions.Add(this.Name, this);
                }
            }
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        VisibilityChanged -= on_visibility_changed;

        Node parent = GetParent();

        if (parent != null)
        {
            if (parent is Hitbox)
            {
                if ((parent as Hitbox).collisions.Contains(this)) (parent as Hitbox).collisions.Remove(this);
            }
            else if (parent is Hurtbox)
            {
                if ((parent as Hurtbox).collisions.Contains(this)) (parent as Hurtbox).collisions.Remove(this);
            }
            else if (parent is Unit)
            {
                if ((parent as Unit).collisions.ContainsKey(this.Name)) (parent as Unit).collisions.Remove(this.Name);
            }
        }
    }

    public void on_visibility_changed()
    {
        Disabled = (Visible) ? false : true;
    }

}
