using Godot;
using System;

[Tool]
public partial class UnitSoundEffect : AudioStreamPlayer2D
{

    public UnitSoundEffect()
    {
        Bus = "UnitFX";
    }

    public override void _EnterTree()
    {
        base._EnterTree();

        var parent = GetParentOrNull<Pose>();
        if (parent != null)
        {
            parent.unit_sound_effect = this;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        var parent = GetParentOrNull<Pose>();
        if (parent != null)
        {
            parent.unit_sound_effect = null;
        }
    }

}
