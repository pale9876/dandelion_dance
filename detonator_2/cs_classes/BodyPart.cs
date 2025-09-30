using Godot;
using System;

[GlobalClass]
public partial class BodyPart : RigidBody2D
{
    public Vector2 init_force = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();
        ApplyImpulse(init_force);
    }


}
