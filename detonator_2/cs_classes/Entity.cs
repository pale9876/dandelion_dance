using Godot;
using System;

public partial class Entity : CharacterBody2D
{
    [Export] public bool ghost;
    public bool _ghost { set => set_ghost(value); get => ghost; }

    public override void _EnterTree()
    {
        ESystem sys = get_sys();
        sys.add_entity(this);
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

}
