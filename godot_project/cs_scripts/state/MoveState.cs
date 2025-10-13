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

        if (get_direction().X == 0.0f)
        {
            (GetRoot() as StateMachine).Dispatch(StateMachine.TO_IDLE);
        }
    }
}
