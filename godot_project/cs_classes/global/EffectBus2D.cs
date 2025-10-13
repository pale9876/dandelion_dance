using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class EffectBus2D : Node
{
    private ulong index = 0;

    public Node2D background_effect_layer = null;
    public Node2D foreground_effect_layer = null;

    public void _spawn_effect(Node2D at)
    {

        index += 1;
    }

    public bool clear_role(Layer layer)
    {
        if (background_effect_layer == layer)
        {
            background_effect_layer = null;
            return true;
        }
        else if (foreground_effect_layer == layer)
        {
            foreground_effect_layer = null;
            return true;
        }

        return false;
    }

}