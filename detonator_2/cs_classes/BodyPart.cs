using Godot;
using System;

public partial class BodyPart : RigidBody2D
{
    public Vector2 init_force = Vector2.Zero;

    public override void _Ready()
    {
        ApplyImpulse(init_force);
    }


}
