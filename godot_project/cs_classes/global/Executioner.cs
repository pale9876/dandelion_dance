using Godot;
using Godot.Collections;
using System;

public partial class Executioner : Node
{
    private Array<HitEvent> events = new Array<HitEvent>();

    public override void _PhysicsProcess(double _delta)
    {
        base._PhysicsProcess(_delta);

        foreach (HitEvent ev in events)
        {
            hit_event_handler(ev);
        }
    }

    private HitEvent create_event(HitEvent.EventType type, long from, long to, Dictionary values = null)
    {
        return new HitEvent(type, from, to, values);
    }

    public void add_event(HitEvent.EventType type, long from, long to, Dictionary values = null)
    {
        var ev = create_event(type, from, to, values);
        events.Add(ev);
    }

    public void hit_event_handler(HitEvent ev)
    {
        switch (ev.event_type)
        {
            case HitEvent.EventType.HIT:
                hit(ev.from, ev.to, ev.effects);
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

    private void hit(long from, long to, Dictionary values)
    {
        
    }

}
