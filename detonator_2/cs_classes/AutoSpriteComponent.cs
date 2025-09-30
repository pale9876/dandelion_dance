using Godot;
using Godot.Collections;
using System;


[Tool]
[GlobalClass]
public partial class AutoSpriteComponent : Node2D
{
    [Export] public Dictionary<String, AutoSprite> auto_sprites = new Dictionary<string, AutoSprite>();

    [Export] public AutoSprite current_sprite { get => _current_sprite; set => change_current_sprite(value); }
    private AutoSprite _current_sprite = null;

    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _EnterTree()
    {
        base._EnterTree();
        
        VisibilityChanged += on_visibility_changed;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        VisibilityChanged -= on_visibility_changed;
    }

    public override void _Ready()
    {
        base._Ready();

        _update();
    }

    private void _update()
    {
        auto_sprites.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is AutoSprite)
            {
                if (!auto_sprites.ContainsKey(node.Name))
                {
                    auto_sprites.Add(node.Name, node as AutoSprite);
                }
            }
        }
    }


    private void on_visibility_changed()
    {
        foreach (AutoSprite sprite in get_sprites())
        {
            sprite.Visible = this.Visible;
        }
    }

    private void on_child_entered(Node node)
    {

    }

    private void on_child_exited(Node node)
    {

    }

    public void change_current_sprite(AutoSprite auto_sprite)
    {
        _current_sprite = auto_sprite;

        foreach (AutoSprite sprite in auto_sprites.Values)
        {
            sprite.Visible = (sprite == auto_sprite) ? true : false;
        }

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

    public Array<AutoSprite> get_sprites() => auto_sprites.Values as Array<AutoSprite>;

}
