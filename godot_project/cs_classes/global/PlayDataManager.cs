using Godot;
using System;
using System.Collections.Generic;

public partial class PlayDataManager : Node
{
    const String DEFAULT_PATH = "res://data/";

    static int index = -1;
    public String path = DEFAULT_PATH;
    private String _path { set => set_path(value); get => path; }
    private Resource current_data;

    public void save(int idx)
    {
        Error r_saver = ResourceSaver.Save(current_data, path + index.ToString(), ResourceSaver.SaverFlags.Compress);
        if (r_saver != Error.Ok)
        {
            GD.PrintErr($"Save Error :: {r_saver}");
        }
    }

    public void load_data(int idx)
    {
        current_data = ResourceLoader.Load(path + idx.ToString());
    }

    public string[] data_list()
    {
        Dictionary<String, Resource> _dict = new Dictionary<string, Resource>();

        DirAccess dir = DirAccess.Open(DEFAULT_PATH);
        if (dir != null)
        {
            string[] files = dir.GetFiles();
            return files;
        }

        return new string[0];

    }
    public void set_path(String value) { path = value; }

    public partial class PlayerData : Resource
    {

        

    }

    public partial class MapData : Resource
    {

    }

}
