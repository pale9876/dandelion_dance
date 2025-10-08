using Godot;
using System;

public partial class TestKoreanString : Node
{
    [Export] public String text = "나는 타인의 그림자이자 심연이다.";

    public override void _EnterTree()
    {
        KoreanTypingEffecter kte = new KoreanTypingEffecter(text);
        GD.Print(kte.get_chars());
    }


}
