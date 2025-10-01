using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Pose : Node2D
{

    public int id = -1;

    [Export] public Dictionary<StringName, AutoSpriteComponent> auto_sprite_components = new Dictionary<StringName, AutoSpriteComponent>();
    [Export] public Dictionary<String, Hitbox> hitboxes = new Dictionary<String, Hitbox>();
    [Export] public Hurtbox hurtbox;

    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();

        VisibilityChanged += on_visibility_changed;

        if (!Engine.IsEditorHint())
            _pose_init();
    }

    public override void _Ready()
    {
        base._Ready();

        _update();
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

    private void _update()
    {
        auto_sprite_components.Clear();
        hitboxes.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is AutoSpriteComponent)
                auto_sprite_components.Add(node.Name, node as AutoSpriteComponent);

            else if (node is Hurtbox)
                hurtbox = node as Hurtbox;

            else if (node is Hitbox)
                hitboxes.Add(node.Name, node as Hitbox);
        }
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
