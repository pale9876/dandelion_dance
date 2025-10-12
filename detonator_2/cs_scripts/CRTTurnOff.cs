using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class CRTTurnOff : FilterRect
{

	[Export] public bool active = false;

	public CRTTurnOff()
	{
		Shader shader = GD.Load<Shader>("res://shaders/crt_turn_off.gdshader");
		var sm = new ShaderMaterial();
		sm.Shader = shader;
		Material = sm;
		MouseFilter = MouseFilterEnum.Ignore;
	}



	public override void _Process(double delta)
	{
		base._Process(delta);


	}
}
