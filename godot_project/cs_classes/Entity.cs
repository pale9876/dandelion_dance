using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class Entity : CharacterBody2D
{
    private long eid = -1;
    private ESystem esystem = null;

    [Export] public bool eyes_only = false;
    [Export] public bool ghost { set => setGhost(value); get => _ghost; }
    private bool _ghost;
    [Export] public bool unique { get; set; }
    [Export] public String first_name { get; set; }
    [Export] public String last_name { get; set; }

    public override void _EnterTree()
    {
        base._EnterTree();

        if (!Engine.IsEditorHint())
        {
            esystem = get_sys();
            esystem.add_entity(this);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        if (!Engine.IsEditorHint())
        {
            esystem.entities.Remove(eid);
            get_p_input().entity_exit(this);
        }

        eid = -1;
    }

    public void setGhost(bool toggle)
    {
        _ghost = toggle;
        this.Visible = (toggle) ? false : true;
    }

    public String get_full_name()
    {
        return last_name + " " + first_name;
    }

    public bool _update_info()
    {
        /*entitiy_info
        // {
            //node
            //name
            //pos
            }*/
        if (esystem.entities.ContainsKey(eid))
        {
            if (esystem.entities[eid].ContainsKey("pos"))
            {
                esystem.entities[this.eid]["pos"] = this.GlobalPosition;
                // GC.Collect(0, GCCollectionMode.Optimized);
                return true;
            }
            else
            {
                GD.PrintErr($"{this} => ");
                return false;
            }
        }

        GD.PrintErr($"{this} => ");
        return false;
    }

    public Dictionary<String, Variant> get_info()
    {
        var sys = get_sys();
        if (sys.entities.ContainsKey(this.eid))
        {
            return sys.entities[this.eid];
        }

        return new Dictionary<string, Variant>(); // Cannot get info
    }

    public Node get_effect_bus()
    {
        EffectBus2D e2b = GetNode<EffectBus2D>("/root/EffectBus2D");
        return e2b;
    }

    public GlobalAnimation get_g_anim() => GetNode<GlobalAnimation>("/root/GlobalAnimation");
    public ESystem get_sys() => GetNode<ESystem>("/root/ESystem");
    public PlayerInput get_p_input() => GetNode<PlayerInput>("/root/PlayerInput");
    public void set_eid(long eid) => this.eid = eid;

}
