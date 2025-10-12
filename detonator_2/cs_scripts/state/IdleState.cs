using Godot;
using System;

[Tool]
public partial class IdleState : UnitState
{
    public override void _Update(double delta)
    {
        base._Update(delta);

        if (Engine.IsEditorHint()) return;

        (Agent as Unit).idle(delta);
    }
}
