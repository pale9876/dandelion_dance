using Godot;
using System;

[Tool]
public partial class JumpPose : Pose
{
    public override void _pose_entered()
    {
        base._pose_entered();
        get_root().jump();
    }

    public override void _pose_update(double delta)
    {
        base._pose_update(delta);
        get_root().move(delta);

        if (get_root().aerial_state == Unit.AerialState.JUMPUP)
        {
            auto_sprite_components["JumpSprites"].change_sprite("JumpUp");
        }
        else if(get_root().aerial_state == Unit.AerialState.FALLDOWN)
        {
            auto_sprite_components["JumpSprites"].change_sprite("FallDown");
        }
    }
}
