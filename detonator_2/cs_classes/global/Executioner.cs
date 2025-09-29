using Godot;
using Godot.Collections;
using System;

public partial class Executioner : Node
{
    private long index = -1;
    [Export] private Dictionary<long, HitEvent> events = new Dictionary<long, HitEvent>();

    public override void _PhysicsProcess(double _delta)
    {
        foreach (HitEvent ev in events.Values)
            hit_event_handler(ev);

        index = -1;
    }

    private HitEvent create_event(HitEvent.EventType type, Node from, Node to, Dictionary values)
    {
        return new HitEvent(type, from, to, values);
    }

    public void add_event(HitEvent.EventType type, Node from, Node to, Dictionary values)
    {
        index += 1;
        var ev = create_event(type, from, to, values);
        events.Add(index, ev);
    }

    public void hit_event_handler(HitEvent ev)
    {
        switch (ev.event_type)
        {
            case HitEvent.EventType.HIT:
                break;
            case HitEvent.EventType.SHIELD:
                break;
            case HitEvent.EventType.EVADE:
                break;
            case HitEvent.EventType.GRABBED:
                break;
            case HitEvent.EventType.PARRY:
                break;
        }
    }

    private void hit(Node from, Node to, float force, Vector2 dir)
    {
        
    }

}
