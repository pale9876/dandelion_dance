using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class AutoSpriteComponent : Node2D
{
    private long id = -1;
    
    [Export] public Pose root_pose = null;
    [Export] public Dictionary<String, AutoSprite> auto_sprites = new();
    [Export] public Array<AutoSprite> index_list = new();
    [Export] public AutoSprite current_sprite { get => _current_sprite; set => change_current_sprite(value); }
    private AutoSprite _current_sprite = null;
    [Export] public int index { get => _index; set => index_changed(value); }
    private int _index = -1;

    public AutoSpriteComponent()
    {

    }

    public override void _EnterTree()
    {
        base._EnterTree();

        Pose parent = GetParentOrNull<Pose>();

        if (parent != null)
        {
            root_pose = parent;
            Renamed += parent.auto_sprite_component_renamed;
            ChildOrderChanged += on_child_order_changed;
            if (!parent.auto_sprite_components.ContainsKey(this.Name))
            {
                parent.auto_sprite_components.Add(this.Name, this);
            }
        }

        if (!Engine.IsEditorHint())
        {
            GlobalAnimation g_anim = get_g_anim();
            g_anim.add_component(this);
        }

        VisibilityChanged += on_visibility_changed;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        var parent = GetParentOrNull<Pose>();
        if (parent != null)
        {
            Renamed -= parent.auto_sprite_component_renamed;
            ChildOrderChanged -= on_child_order_changed;
            if (parent.auto_sprite_components.ContainsKey(this.Name))
            {
                parent.auto_sprite_components.Remove(this.Name);
            }
        }

        if (!Engine.IsEditorHint())
        {
            GlobalAnimation g_anim = get_g_anim();
            g_anim.remove_component(this.id);
        }

        auto_sprites.Clear();
        id = -1;

        VisibilityChanged -= on_visibility_changed;
    }

    private void on_visibility_changed()
    {
        current_sprite.Visible = true;
    }

    public void on_child_renamed()
    {
        auto_sprites.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is AutoSprite)
            {
                auto_sprites.Add(node.Name, node as AutoSprite);
            }
        }
    }

    public void on_child_order_changed()
    {
        index_list.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is AutoSprite)
            {
                index_list.Add(node as AutoSprite);
                if (index_list.Count > 0)
                {
                    index = 0;
                }
            }
        }
    }

    public void change_current_sprite(AutoSprite auto_sprite)
    {
        _current_sprite = auto_sprite;
        if (auto_sprite != null)
        {
            foreach (AutoSprite sprite in index_list)
            {
                sprite.Visible = (auto_sprite == sprite) ? true : false;
            }
        }
    }

    public void add_sprite(AutoSprite sprite)
    {
        if (!auto_sprites.ContainsKey(sprite.Name))
        {
            auto_sprites.Add(sprite.Name, sprite);
        }
        id += 1;
        sprite.set_id(id);
    }

    public void remove_sprite(AutoSprite sprite)
    {
        if (auto_sprites.ContainsKey(sprite.Name)) auto_sprites.Remove(sprite.Name);
    }

    public bool change_sprite(String sprite_name)
    {
        if (auto_sprites.ContainsKey(sprite_name))
        {
            AutoSprite next_sprite = auto_sprites[sprite_name];
            current_sprite = next_sprite;
            return true;
        }

        return false;
    }

    public void index_changed(int value)
    {
        _index = Mathf.Clamp(
            value,
            (index_list.Count == 0) ? -1 : 0,
            index_list.Count - 1
        );

        if (_index != -1)
        {
            current_sprite = index_list[_index];
        }
    }
    
    public virtual void on_stopped(String sprite_name) { }
    public virtual void on_played(String sprite_name){}
    public virtual void on_looped(String sprite_name){}
    public virtual void on_finished(String sprite_name){}

    public void set_id(long value) => this.id = value;
    public Array<AutoSprite> get_sprites() => auto_sprites.Values as Array<AutoSprite>;
    private GlobalAnimation get_g_anim() => GetNode<GlobalAnimation>("/root/GlobalAnimation");
}
