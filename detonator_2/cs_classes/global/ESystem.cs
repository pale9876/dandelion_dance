using Godot;
using Godot.Collections;
using System;

public partial class ESystem : Node
{
    private int index = -1;
    public Dictionary<long, Dictionary<String, Variant>> entities = new Dictionary<long, Dictionary<String, Variant>>();

    public Entity player { get => getPlayer(); set => setPlayer(value); }
    private Entity _player = null;

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

    public Array<Entity> get_entities()
    {
        var result = new Array<Entity>();

        foreach (Dictionary<String, Variant> dict in entities.Values)
        {
            result.Add((Entity)dict["node"]);
        }

        return result;
    }

    public Entity getPlayer() => this._player;
    public void setPlayer(Entity entity)
    {
        _player = entity;
    }

}
