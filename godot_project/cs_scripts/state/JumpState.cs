using Godot;
using System;

[Tool]
public partial class JumpState : UnitState
{

    public override void _Enter()
    {
        base._Enter();

        (Agent as Unit).pose_component.change_to("Jump");
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        if (get_unit().Velocity.Y > 0.0)
        {
            get_state_machine().Dispatch(StateMachine.FALL_DOWN);
        }

    }

}
