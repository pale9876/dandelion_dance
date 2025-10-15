using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class UnitAnimation : AnimationPlayer
{
	public UnitAnimation()
	{

	}

	public override void _EnterTree()
	{
		base._EnterTree();

		Pose pose = GetParentOrNull<Pose>();
		if (pose != null)
		{
			pose.animation_player = this;
			AnimationFinished += pose.on_animation_finished;
			RootNode = this.GetPathTo(pose);
		}
		
		if (!HasAnimation("Default"))
        {
			AnimationLibrary default_library = new AnimationLibrary();
			Animation default_anim = new Animation();
			default_library.AddAnimation("Default", default_anim);
			AddAnimationLibrary("", default_library);
        }
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		Pose pose = GetParentOrNull<Pose>();
		if (pose != null)
		{
			pose.animation_player = null;
			AnimationFinished -= pose.on_animation_finished;
		}

		RemoveAnimationLibrary("");
	}
}
