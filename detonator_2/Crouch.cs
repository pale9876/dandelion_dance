using Godot;
using System;


[Tool]
public partial class Crouch : Pose
{

    public override void _pose_entered()
    {
        base._pose_entered();

        if (Engine.IsEditorHint()) return;
        
    }


}
