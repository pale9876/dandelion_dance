using Godot;
using System;

[Tool]
[GlobalClass]
public partial class PsychoValuement : Node
{

    [Export] public bool eval_player_mode = false;

    [Export(PropertyHint.Range, "0.015, 0.850, 0.001")] public float aggressive = 0.500f;
    [Export(PropertyHint.Range, "0.015, 0.850, 0.001")] public float defensive = 0.500f;

    public float get_aggressive()
    {
        return aggressive / aggressive + defensive;
    }

    public float get_defensive()
    {
        return defensive / defensive + aggressive;
    }

}
