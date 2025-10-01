using Godot;
using Godot.Collections;
using System;


[Tool]
[GlobalClass]
public partial class Stage : Node2D
{

    [Export] private int _index { get => index; set => set_index(value); }
    public int index = -1;

    [Export] public Dictionary<String, Region> regions = new Dictionary<String, Region>();
    [Export] public Dictionary<int, Region> idx_dict = new Dictionary<int, Region>();

    [Export] private int _current_index { get => current_index; set => set_current_region(value); }
    public int current_index = -1;

    public Region current_region = null;
    [Export] public StageRect stage_rect { get; set; }

    [ExportToolButton("Update")] private Callable update => Callable.From(_update);

    public override void _Ready()
    {
        base._Ready();

        _update();
    }

    private void _update()
    {
        index = -1;
        idx_dict.Clear();

        var children = this.GetChildren();

        var next_pivot_point = Vector2.Zero;

        foreach (Node node in children)
        {
            if (node is Region)
            {
                index += 1;
                Region region = node as Region;
                region.pivot_point = next_pivot_point;
                next_pivot_point = next_pivot_point with { X = next_pivot_point.X + region.size.X, Y = 0f };
                idx_dict.Add(index, region);
            }
        }
    }

    private void set_index(int idx)
    {
        index = idx;
    }

    public void change_region(Region region)
    {
        current_region = region;
    }

    public void set_current_region(int idx)
    {
        current_index = idx;

        if (idx_dict.ContainsKey(idx))
        {
            foreach (int i in idx_dict.Keys)
            {
                if (i == idx)
                {
                    current_region = idx_dict[i];

                    if (stage_rect != null)
                    {
                        Vector2[] poly = new Vector2[4];

                        Vector2 start_point = current_region.GlobalPosition + current_region.pivot_point;
                        Vector2 end_point = current_region.GlobalPosition + current_region.pivot_point + current_region.size;

                        poly[0] = start_point;
                        poly[1] = new Vector2(end_point.X, start_point.Y);
                        poly[2] = end_point;
                        poly[3] = new Vector2(start_point.X, end_point.Y);

                        stage_rect.set_collision(poly);
                    }

                    idx_dict[i].Visible = true;
                }
                else
                {
                    idx_dict[i].Visible = false;
                }
            }
        }

    }

    private Array<Region> get_regions() => idx_dict.Values as Array<Region>;

}
