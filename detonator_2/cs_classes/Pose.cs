using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Pose : Node2D
{

    private int id = -1;
    [Signal] public delegate void ActiveTriggerEventHandler();

    [Export] public bool filp_lock = false;

    [Export] public Dictionary<String, AutoSpriteComponent> auto_sprite_components = new Dictionary<String, AutoSpriteComponent>();
    [Export] public Dictionary<String, Hitbox> hitboxes = new Dictionary<String, Hitbox>();
    [Export] public Hurtbox hurtbox = null;

    // [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();

        VisibilityChanged += on_visibility_changed;

        PoseComponent parent = GetParent<PoseComponent>();
        if (parent != null)
        {
            parent.insert_pose(this);
        }

        if (!Engine.IsEditorHint())
            _pose_init();
    }
    public override void _ExitTree()
    {
        base._ExitTree();

        auto_sprite_components.Clear();
        hitboxes.Clear();
        hurtbox = null;

        PoseComponent parent = GetParent<PoseComponent>();
        if (parent != null)
            parent.delete_pose(this);

        VisibilityChanged -= on_visibility_changed;
    }

    public override void _Ready()
    {
        base._Ready();

        // _update();
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

    // private void _update()
    // {
    //     auto_sprite_components.Clear();
    //     hitboxes.Clear();

    //     foreach (Node node in GetChildren())
    //     {
    //         if (node is AutoSpriteComponent)
    //             auto_sprite_components.Add(node.Name, node as AutoSpriteComponent);

    //         else if (node is Hurtbox)
    //             hurtbox = node as Hurtbox;

    //         else if (node is Hitbox)
    //             hitboxes.Add(node.Name, node as Hitbox);
    //     }
    // }

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

    public void flip(bool toggle)
    {
        foreach (Hitbox hitbox in hitboxes.Values)
        {
            hitbox.Scale = hitbox.Scale with { X = (toggle) ? -1.0f : 1.0f };
        }

        hurtbox.Scale = hurtbox.Scale with { X = (toggle) ? -1.0f : 1.0f };

        foreach (AutoSpriteComponent component in auto_sprite_components.Values)
        {
            foreach (AutoSprite sprite in component.get_sprites())
            {
                sprite.FlipH = (!filp_lock) ? toggle : false;
            }
        }
    }

    public int get_id() => id;
    public void set_id(int id) => this.id = id;
}
