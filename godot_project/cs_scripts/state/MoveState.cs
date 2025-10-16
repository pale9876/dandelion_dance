using Godot;
using System;

[Tool]
public partial class MoveState : UnitState
{
    public override void _Enter()
    {
        base._Enter();

        get_unit().pose_component.change_to("Move");
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        var dir = get_direction();
        var sm = get_state_machine();

        if (dir.X == 0.0f)
        {
            sm.ChangeActiveState(sm.states["Idle"]);
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
