using Godot;
using System;

[Tool]
public partial class JumpState : UnitState
{

    public override void _Enter()
    {
        base._Enter();
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        var sm = get_state_machine();

        if (get_unit().aerial_state == Unit.AerialState.NONE)
        {
            sm.ChangeActiveState(
                sm.states["Idle"]
            );
        }
        else if (get_unit().aerial_state == Unit.AerialState.FALLDOWN)
        {
            sm.ChangeActiveState(
                sm.states["Fall"]
            );
        }
    }

}
