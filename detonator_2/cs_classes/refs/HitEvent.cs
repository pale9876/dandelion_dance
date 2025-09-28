using Godot;
using Godot.Collections;
using System;

public partial class HitEvent : RefCounted
{

    const String event_force = "force";
    const String event_dir = "dir";

    public enum EventType
    {
        HIT = 0,
        SHIELD = 1,
        PARRY = 2,
        EVADE = 3,
        GRABBED = 4,
    }

    public EventType event_type;
    public Node from;
    public Node to;
    public float dir;
    public float force;
    

    public HitEvent(EventType type, Node from, Node to, Dictionary values)
    {
        this.event_type = type;
        this.from = from;
        this.to = to;

        if (values.Count != 0)
        {
            if (values.ContainsKey(event_force)) { this.force = (float)values[event_force].AsDouble(); }
            if (values.ContainsKey(event_dir)) { this.dir = (float)values[event_dir].AsDouble(); }
        }
    }


}
