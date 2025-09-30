using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Pose : Node2D
{

    public int id = -1;

    [Export] public AutoSpriteComponent auto_sprite_component;
    [Export] public Hitbox hitbox;
    [Export] public Hurtbox hurtbox;

    public override void _EnterTree()
    {
        base._EnterTree();

        VisibilityChanged += on_visibility_changed;

        if (!Engine.IsEditorHint())
            _pose_init();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        VisibilityChanged -= on_visibility_changed;
    }

    public virtual void _pose_init()
    {

    }

    public virtual void _pose_entered()
    {

    }

    public virtual void _pose_exited()
    {

    }

    public void on_visibility_changed()
    {
        foreach (Node node in GetChildren())
        {
            if (node is Node2D)
            {
                (node as Node2D).Visible = this.Visible;
            }
        }
    }
}
