using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class GlobalAnimation : Node
{
    public float global_scale = 1.0f;

    long index = -1;

    private Dictionary<long, AutoSprite> auto_sprites = new Dictionary<long, AutoSprite>();

    public void add_sprite(AutoSprite auto_sprite)
    {
        index += 1;
        auto_sprite.set_id(index);

        auto_sprites.Add(index, auto_sprite);
    }

}
