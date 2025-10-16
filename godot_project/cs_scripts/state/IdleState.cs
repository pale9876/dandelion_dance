using Godot;
using System;

[Tool]
public partial class IdleState : UnitState
{

    public override void _Enter()
    {
        base._Enter();

        get_unit().pose_component.change_to("Idle");
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        var dir = get_direction();
        var sm = get_state_machine();

        if (dir.X != 0.0f)
        {
            sm.ChangeActiveState(sm.states["Move"]);
        }
        else
        {
            if (dir.Y < 0.0f)
            {
                get_unit().pose_component.change_to("Jump");
            }
            
            if (get_unit().aerial_state == Unit.AerialState.JUMPUP)
            {
                sm.ChangeActiveState(sm.states["Jump"]);
            }
        }
    }
}
