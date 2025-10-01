using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class Entity : CharacterBody2D
{
    private long eid = -1;

    [Export] private bool _ghost { set => set_ghost(value); get => ghost; }
    public bool ghost;
    [Export] public bool unique { get; set; }
    [Export] public String first_name { get; set; }
    [Export] public String last_name { get; set; }


    public override void _EnterTree()
    {
        base._EnterTree();

        if (!Engine.IsEditorHint())
        {
            ESystem sys = get_sys();
            sys.add_entity(this);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        if (!Engine.IsEditorHint())
        {
            ESystem sys = get_sys();
            sys.entities.Remove(eid);
        }

        eid = -1;
    }

    public bool mns_with_global()
    {
        Velocity *= get_g_anim().global_scale;

        return MoveAndSlide();
    }

    public GlobalAnimation get_g_anim()
    {
        GlobalAnimation g_anim = GetNode<GlobalAnimation>("/root/GlobalAnimation");
        return g_anim;
    }

    public ESystem get_sys()
    {
        var e_system = GetNode<ESystem>("/root/ESystem");
        return e_system;
    }

    public void set_ghost(bool toggle)
    {
        ghost = toggle;
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
        var system = get_sys();

        if (system.entities.ContainsKey(eid))
        {
            if (system.entities[eid].ContainsKey("pos"))
            {
                system.entities[this.eid]["pos"] = this.GlobalPosition;
                return true;
            }
            else
                GD.PrintErr($"{this} => ");
                return false;
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

    public void set_eid(long eid)
    {
        this.eid = eid;
    }

}
