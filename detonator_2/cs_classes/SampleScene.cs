using Godot;
using System;

public partial class SampleScene : CharacterBody2D
{
    public override void _EnterTree()
    {
        base._EnterTree();

        MouseEntered += on_mouse_entered;
    }

    private void on_mouse_entered()
    {
        GD.Print("Hello");
    }

}
