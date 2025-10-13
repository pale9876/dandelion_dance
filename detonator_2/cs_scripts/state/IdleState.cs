using Godot;
using System;

[Tool]
public partial class IdleState : UnitState
{

    public override void _Enter()
    {
        base._Enter();

        (Agent as Unit).pose_component.change_to("Idle");
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        if (get_direction().X != 0.0f)
        {
            GetRoot().Dispatch(StateMachine.TO_MOVE);
        }
    }

}
