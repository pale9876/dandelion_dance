using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class FilterController : Control
{

	[Export] public Dictionary<String, Control> filters = new();

	public FilterController()
	{
		this.MouseFilter = MouseFilterEnum.Ignore;
	}

	public override void _EnterTree()
	{
		base._EnterTree();

		VisibilityChanged += on_visibility_changed;
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		VisibilityChanged -= on_visibility_changed;
	}

	public void active_filter(String filter_name, float time)
	{
		if (filters.ContainsKey(filter_name))
        {
			filters[filter_name].Set("time", time);
        }
	}

	public void active_filter(String filter_name, bool toggle)
    {
        if (filters.ContainsKey(filter_name))
        {
			filters[filter_name].Set("active", toggle);
        }
    }

	private void on_visibility_changed()
	{
		if (!Visible)
		{
			foreach (Control filter in filters.Values)
				filter.Visible = this.Visible;
		}
	}
}
