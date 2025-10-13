using Godot;
using Godot.Collections;
using System;

public partial class HitEvent : RefCounted
{

    const String event_force = "force";
    const String event_dir = "dir";

    public enum EventType
    {
        NONE = -1,
        HIT = 0,
        SHIELD = 1,
        PARRY = 2,
        EVADE = 3,
        GRABBED = 4,
    }

    public EventType event_type = EventType.NONE;
    public long from;
    public long to;
    public Dictionary effects;

    public HitEvent(EventType type, long from, long to, Dictionary values)
    {
        this.event_type = type;
        this.from = from;
        this.to = to;

        if (values.Count != 0)
        {
            effects = values;
        }
    }


}
