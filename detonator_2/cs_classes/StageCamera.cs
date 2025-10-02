using Godot;
using System;

[Tool]
[GlobalClass]
public partial class StageCamera : Camera2D
{

    public enum FollowMode
    {
        IMMEDIATE,
        SMOOTH,
    }

    [Export] public Node2D target = null;

    private Stage stage = null;


    public override void _EnterTree()
    {
        base._EnterTree();

        stage = GetParentOrNull<Stage>();
        if (stage == null)
        {
            GD.PrintErr($"{this} => 이 객체는 반드시 Stage의 하위에 존재해야합니다.");
            return;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        
    }

    public void shake()
    {

    }

}
