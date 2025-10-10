using Godot;
using System;

[Tool]
[GlobalClass]
public partial class PsychoValuement : Node
{

    [Export(PropertyHint.Range, "0.015, 0.850, 0.001")] public float aggressive = 0.500f;
    [Export(PropertyHint.Range, "0.015, 0.850, 0.001")] public float defensive = 0.500f;

    public override void _EnterTree()
    {
        base._EnterTree();

        var unit = GetParentOrNull<Unit>();
        if (unit != null)
            unit.psycho_valuement = this;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        var unit = GetParentOrNull<Unit>();
        if (unit != null)
            unit.psycho_valuement = null;
    }

    public float get_aggressive()
    {
        return aggressive / aggressive + defensive;
    }

    public float get_defensive()
    {
        return defensive / defensive + aggressive;
    }

}
