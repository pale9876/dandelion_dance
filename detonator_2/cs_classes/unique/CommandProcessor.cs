using System;
using Godot;
using Godot.Collections;

public partial class CommandProcessor : Node
{
    public enum Command
    {
        WEAKPUNCH,
        STRONGKICK,
    }

    [Export] public Resource command_information { get => _command_information; set => setCommandInformation(value); }
    private Resource _command_information = null;
    private PlayerInput input_singleton = null;

    private Unit unit = null;

    private Dictionary<String, Array<Command>> command = new Dictionary<String, Array<Command>>();

    public override void _EnterTree()
    {
        base._EnterTree();
        input_singleton = get_input_singleton();
        unit = GetParent<Unit>();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 dir = input_singleton.get_current_direction();

        if (dir.X != 0.0f)
        {
            var move_state = unit.state_machine.states["Move"];
            if (move_state != unit.state_machine.GetActiveState())
            {
                unit.state_machine.ChangeActiveState(move_state);
            }
        }
        else
        {
            unit.state_machine.Dispatch(StateMachine.TO_IDLE);
        }
    }

    public void setCommandInformation(Resource res)
    {
        _command_information = res;
    }

    private PlayerInput get_input_singleton() => GetNode<PlayerInput>("/root/PlayerInput");

}
