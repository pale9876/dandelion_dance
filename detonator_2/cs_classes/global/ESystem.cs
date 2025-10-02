using Godot;
using Godot.Collections;
using System;

public partial class ESystem : Node
{
    private int index = -1;
    public Dictionary<long, Dictionary<String, Variant>> entities = new();


    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        entities.Clear();
        index = -1;
    }

    public void add_entity(Entity ett)
    {
        index += 1;
        ett.set_eid(index);

        entities.Add(
            index, new Dictionary<String, Variant>
            {
                { "node", ett },
                { "name", ett.Name },
                { "pos", ett.GlobalPosition },
            }
        );
    }

    public Dictionary<String, Variant> get_entity_info(long idx) => (entities.ContainsKey(idx)) ? entities[idx] : new Dictionary<String, Variant>();

    public Array<Entity> get_entities()
    {
        var result = new Array<Entity>();

        foreach (Dictionary<String, Variant> dict in entities.Values)
        {
            result.Add((Entity)dict["node"]);
        }

        return result;
    }

}
