using Godot;
using Godot.Collections;
using System;

public partial class AutoSpriteComponent : Node2D
{
    [Export] public Dictionary<String, AutoSprite> auto_sprites = new Dictionary<string, AutoSprite>();

    public override void _Ready()
    {
        _update();
    }

    public void _update()
    {
        auto_sprites.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is AutoSprite)
                auto_sprites.Add(node.Name, node as AutoSprite);
        }
    }

    public Array<AutoSprite> get_sprites()
    {
        return auto_sprites.Values as Array<AutoSprite>;
    }

}
