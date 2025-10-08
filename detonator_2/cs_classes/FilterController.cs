using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class FilterController : Control
{

    [Export] private Dictionary<String, Control> filters = new();

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

    private void on_child_entered(Node node)
    {
        if (node is Control)
        {
            Control c = node as Control;
            c.MouseFilter = MouseFilterEnum.Ignore;
            c.SetAnchorsPreset(LayoutPreset.FullRect);
            c.Visible = false;
            filters.Add(c.Name, c);
        }
    }

    private void _update()
    {
        foreach (Node node in GetChildren())
        {
            if (node is Control)
            {
                Control c = node as Control;
                c.MouseFilter = MouseFilterEnum.Ignore;
                filters.Add(node.Name, node as Control);
            }
        }
    }

    public void active_filter(String filter_name, float time)
    {
        if (filters.ContainsKey(filter_name))
            filters[filter_name].Set("time", time);
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
