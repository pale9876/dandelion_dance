### [Tool]

---

포즈는 유닛이 취할 행동 묘사하는 객체입니다.

Trigger, AutoSpriteComponent, Hitbox, GrabPoint, Hurtbox, UnitAnimation 와 같이 프레임이 진행될 때마다 위치와 프로퍼티 값이 유동적으로 변하는 객체들을 관리하기 위해 제작하였으며, 상태머신과 비슷하게 작동되지만 좀 더 심플한 알고리즘을 통하여 구동됩니다.


``` C#
using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Pose : Node2D
{
    private int id = -1;

    [Export] public bool filp_lock = false;
    [Export] public Dictionary<String, Trigger> triggers = new();
    [Export] public Dictionary<String, AutoSpriteComponent> auto_sprite_components = new();

    [Export] public Dictionary<String, Hitbox> hitboxes = new();
    [Export] public Dictionary<String, GrabPoint> grab_points = new();
    [Export] public Hurtbox hurtbox = null;
    [Export] public UnitAnimation animation_player = null;

    public PoseComponent _component = null;

    public override void _EnterTree()
    {
        base._EnterTree();

        PoseComponent parent = GetParentOrNull<PoseComponent>();
        if (parent != null)
        {
            Renamed += parent.child_renamed;
            parent.insert_pose(this);
            _component = parent;
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
            _component = null;
        }

        VisibilityChanged -= on_visibility_changed;
    }

    public virtual void _pose_init() {}
    public virtual void _pose_entered() {}
    public virtual void _pose_update(double delta) {}
    public virtual void _pose_exited() {}

    public void auto_sprite_component_renamed()
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

    public void grab_point_renamed()
    {
        grab_points.Clear();
        foreach (Node node in GetChildren())
        {
            if (node is GrabPoint)
            {
                grab_points.Add(node.Name, node as GrabPoint);
            }
        }
    }

    public virtual void on_animation_finished(StringName anim_name) {}

    public virtual void on_reached_trigger_line(String trigger_name)
    {
        if (triggers.ContainsKey(trigger_name))
        {
            Trigger trigger = triggers[trigger_name];
            PoseComponent component = GetParent<PoseComponent>();
            if (component.root != null)
            {
                component.trigger_map.activate_trigger(trigger, component.root);
            }
        }
    }

    public void on_visibility_changed()

    {

        if (Visible)

        {

            PoseComponent parent = GetParentOrNull<PoseComponent>();

            parent.change_pose(this);

            if (animation_player != null)

            {

                animation_player.Play("Default");

            }

  

            foreach (Node node in GetChildren())

            {

                if (node is Node2D)

                {

                    (node as Node2D).Visible = this.Visible;

                }

            }

        }
        else
        {
            if (animation_player != null)
            {
                animation_player.Play("RESET");
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

    public Array<AutoSprite> get_current_sprites()
    {
        Array<AutoSprite> result = new();
        foreach (var component in auto_sprite_components.Values)
        {
            result.Add(component.current_sprite);
        }
        return result;
    }

    public int get_id() => id;
    public void set_id(int id) => this.id = id;
    public Unit get_root() => (_component != null) ? _component.root : null;

}

```