using Godot;
using System;

[Tool]
public partial class MovePose : Pose
{
    private float direction = 0.0f;

    public override void _pose_entered()
    {
        base._pose_entered();
    }

    public override void _pose_update(double delta)
    {
        base._pose_update(delta);

        float root_dir = get_root().get_p_input().get_current_direction().X;
        if (root_dir > 0.0f) // right
        {
            _component.flip = false;
        }
        else if (root_dir < 0.0f) // left
        {
            _component.flip = true;
        }
        
        get_root().move(delta);
    }

    public override void _pose_exited()
    {
        base._pose_exited();
        direction = 0.0f;
    }


}
