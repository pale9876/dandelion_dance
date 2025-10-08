#if TOOLS
using Godot;
using System;

[Tool]
public partial class PosePanelPlugin : EditorPlugin
{

	private PosePanel panel = null;

	public override void _EnterTree()
	{
		base._EnterTree();
		// Initialization of the plugin goes here.
		panel = GD.Load<PackedScene>("res://addons/pose_panel/pose_panel.tscn").Instantiate<PosePanel>();
		AddControlToDock(DockSlot.LeftBr, panel);
		
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		// Clean-up of the plugin goes here.
		RemoveControlFromDocks(panel);
		panel.Free();
		
	}
}
#endif
