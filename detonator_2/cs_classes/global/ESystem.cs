using Godot;
using Godot.Collections;
using System;

public partial class ESystem : Node
{
    private int index = -1;
    private static Dictionary<long, Dictionary<String, Variant>> entities = new Dictionary<long, Dictionary<String, Variant>>();

    public override void _ExitTree()
    {
        base._ExitTree();

        index = -1;
    }

    public void add_entity(Entity ett)
    {
        index += 1;
        entities[index] = new Dictionary<String, Variant> {
            { "node", ett },
            { "name", ett.Name },
            { "pos", ett.GlobalPosition },
        };
    }

    public static Array<Entity> get_entities()
    {
        var result = new Array<Entity>();

        foreach (Dictionary<String, Variant> dict in entities.Values)
        {
            result.Add((Entity)dict["node"]);
        }

        return result;
    }
}
