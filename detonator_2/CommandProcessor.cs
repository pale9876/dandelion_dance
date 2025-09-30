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

    [Export] public Resource command_information { get; set; }

    private Dictionary<String, Array<Command>> command = new Dictionary<String, Array<Command>>();

    public override void _PhysicsProcess(double delta)
    {
        
    }


}
