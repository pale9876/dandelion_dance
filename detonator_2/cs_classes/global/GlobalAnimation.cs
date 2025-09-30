using Godot;
using System;
using System.Collections.Generic;

public partial class GlobalAnimation : Node
{
    public static float global_scale = 1.0f;

    long index = -1;
    Dictionary<long, AutoSprite> auto_sprites;

    public void add_sprite(AutoSprite auto_sprite)
    {
        index += 1;
        auto_sprites.Add(index, auto_sprite);
    }

}
