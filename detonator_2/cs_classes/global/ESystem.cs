using Godot;
using Godot.Collections;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class ESystem : Node
{
    private int index = -1;
    private Dictionary<long, Dictionary> entities = new Dictionary<long, Dictionary>();

    public void add_entity(Entity ett)
    {
        index += 1;
        entities[index] = new Dictionary {
            { "node", ett },
            { "name", ett.Name },
            { "pos", ett.GlobalPosition },
        };
    }
}
