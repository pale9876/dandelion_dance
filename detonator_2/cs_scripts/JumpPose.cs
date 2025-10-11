using Godot;
using System;

[Tool]
public partial class JumpPose : Pose
{

    [Export] private bool jumpup = false;
    [Export] private bool jumpdown = false;

    public override void _pose_entered()
    {
        base._pose_entered();
    }

    


}
