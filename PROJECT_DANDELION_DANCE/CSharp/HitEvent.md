
---

HitBox객체가 HurtBox를 감지할 때 생성하는 이벤트입니다.

``` C#
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
        COUNTER = 1,
        SHIELD = 2,
        PARRY = 3,
        EVADE = 4,
        GRABBED = 5,
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

```