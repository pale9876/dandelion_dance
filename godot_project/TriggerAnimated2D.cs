using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class TriggerAnimated2D : AnimatedSprite2D
{

    [Export] public Dictionary<String, Dictionary<int, String>> trigger_line = new();

    public override void _EnterTree()
    {
        base._EnterTree();

    }


}
