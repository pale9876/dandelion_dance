using Godot;
using System;

[Tool]
public partial class FallState : UnitState
{

    public override void _Enter()
    {
        base._Enter();
        // get_unit().pose_component.change_to("Jump");
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        if (get_unit().aerial_state == Unit.AerialState.JUMPUP)
        {
            get_state_machine().ChangeActiveState(
                get_state_machine().states["Jump"]
            );
        }

        else if (get_unit().aerial_state == Unit.AerialState.NONE)
        {
            get_state_machine().ChangeActiveState(
                get_state_machine().states["Idle"]
            );
        }
    }

}

