using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Pose : Node2D
{
    private int id = -1;
    [Signal] public delegate void reached_triggerEventHandler(String trigger_name);

    [Export] public bool filp_lock = false;
    [Export] public Dictionary<String, AutoSpriteComponent> auto_sprite_components = new();
    [Export] public Dictionary<String, Hitbox> hitboxes = new();
    [Export] public Hurtbox hurtbox = null;

    public override void _EnterTree()
    {
        base._EnterTree();

        PoseComponent parent = GetParentOrNull<PoseComponent>();
        if (parent != null)
        {
            Renamed += parent.child_renamed;
            parent.insert_pose(this);
            Visible = (this != parent.current_pose) ? false : true;
        }

        if (!Engine.IsEditorHint())
        {
            _pose_init();
        }

        VisibilityChanged += on_visibility_changed;
    }
    public override void _ExitTree()
    {
        base._ExitTree();

        auto_sprite_components.Clear();
        hitboxes.Clear();
        hurtbox = null;

        PoseComponent parent = GetParentOrNull<PoseComponent>();
        if (parent != null)
        {
            Renamed -= parent.child_renamed;
            parent.delete_pose(this);
        }

        VisibilityChanged -= on_visibility_changed;
    }

    public void child_updated()
    {
        auto_sprite_components.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is AutoSpriteComponent)
            {
                auto_sprite_components.Add(node.Name, node as AutoSpriteComponent);
            }
        }
    }

    public virtual void _pose_init() {}
    public virtual void _pose_entered() {}
    public virtual void _pose_update(double delta) {}
    public virtual void _pose_exited() {}

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

    public Array<AutoSprite> get_current_sprites()
    {
        Array<AutoSprite> result = new();

        foreach (var component in auto_sprite_components.Values)
        {
            result.Add(component.current_sprite);
        }

        return result;
    }

}
