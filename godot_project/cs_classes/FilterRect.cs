using Godot;
using System;

[Tool]
[GlobalClass]
public partial class FilterRect : ColorRect
{

	public FilterRect()
    {
		Color transparent = Color.FromHtml("ffffff00");
		Color = transparent;
    }

	public override void _EnterTree()
	{
		base._EnterTree();

		FilterController parent = GetParentOrNull<FilterController>();
		if (parent != null)
		{
			if (parent.filters.ContainsKey(this.Name))
			{
				parent.filters.Add(this.Name, this);
			}
		}
	}

	public override void _ExitTree()
	{
		base._ExitTree();

		FilterController parent = GetParentOrNull<FilterController>();
		if (parent != null)
		{
			if (parent.filters.ContainsKey(this.Name))
			{
				parent.filters.Remove(this.Name);
			}
		}
	}

}
