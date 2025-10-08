using Godot;
using System;

[Tool]
public partial class Jump : UnitState
{

    public override void _Enter()
    {
        base._Enter();

        (Agent as Unit).jump();

    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        (Agent as Unit).move(delta);
    }

}
