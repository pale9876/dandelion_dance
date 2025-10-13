using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class GlobalAnimation : Node
{
    [Signal] public delegate void time_scale_changedEventHandler(float value);

    long index = -1;

    private Dictionary<long, AutoSpriteComponent> components = new Dictionary<long, AutoSpriteComponent>();

    public void add_component(AutoSpriteComponent component)
    {
        index += 1;
        component.set_id(index);

        components.Add(index, component);
    }

    public bool remove_component(long index)
    {
        if (components.ContainsKey(index))
        {
            components.Remove(index);
            return true;
        }
        GD.PrintErr($"GlobalAnimation => {index}는 존재하지 않습니다");
        return false;
    }

    public void emit_time_scale_changed(float value) => EmitSignaltime_scale_changed(value);

}
