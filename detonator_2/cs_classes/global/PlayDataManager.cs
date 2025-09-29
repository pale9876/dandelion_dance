using Godot;
using System;


public partial class PlayDataManager : Node
{
    public String path = "";
    private String _path { set => set_path(value); get => path; }
    private Resource current_data;


    public void set_path(String value) { path = value; }
    public void save()
    {
        Error r_saver = ResourceSaver.Save(current_data, path, ResourceSaver.SaverFlags.Compress);
        if (r_saver != Error.Ok)
        {
            GD.PrintErr($"Save Error :: {r_saver}");
        }
    }

    public void load_data(int idx)
    {

    }

    public partial class PlayerData : Resource
    {

    }

    public partial class MapData : Resource
    {

    }

}
