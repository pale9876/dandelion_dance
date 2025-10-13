using Godot;
using System;

[Tool]
public partial class IdlePose : Pose
{

    public override void _pose_entered()
    {
        base._pose_entered();
    }

    public override void _pose_update(double delta)
    {
        base._pose_update(delta);

        get_root().apply_friction(delta);

    }

}
