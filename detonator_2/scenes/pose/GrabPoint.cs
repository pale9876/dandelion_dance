using Godot;
using Godot.Collections;
using System;

[Tool]
[GlobalClass]
public partial class GrabPoint : Marker2D
{
	private Node2D grabbed { get => _grabbed; set => setGrabbed(value); }
	private Node2D _grabbed = null;

	public override void _EnterTree()
	{
		base._EnterTree();
		Pose parent = GetParentOrNull<Pose>();
		if (parent != null)
		{
			Renamed += parent.grab_point_renamed;
			if (!parent.grab_points.ContainsKey(this.Name)) parent.grab_points.Add(this.Name, this);
        }
	}

    public override void _ExitTree()
    {
		base._ExitTree();
		Pose parent = GetParentOrNull<Pose>();
		if (parent != null)
		{
			Renamed -= parent.grab_point_renamed;
			if (parent.grab_points.ContainsKey(this.Name)) parent.grab_points.Remove(this.Name);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
		base._PhysicsProcess(delta);
		if (grabbed != null)
        {
			grabbed.GlobalPosition = this.GlobalPosition;
        }
    }

	public virtual void unit_grabbed_event_handler(Unit unit)
    {
        
    }

	private void setGrabbed(Node2D node2d)
	{
		_grabbed = node2d;

		if (node2d is Unit)
		{
			unit_grabbed_event_handler(node2d as Unit);
		}
	}


}
