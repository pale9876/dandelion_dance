using Godot;
using System;

[GlobalClass]
public partial class Entity : CharacterBody2D
{
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

    public bool mns_with_global()
    {
        Velocity *= GlobalAnimation.global_scale;
        
        return MoveAndSlide();
    }

    public ESystem get_sys()
    {
        var e_system = GetNode<ESystem>("root/ESystem");
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

}
