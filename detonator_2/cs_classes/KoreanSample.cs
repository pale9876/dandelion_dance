using Godot;
using SmartFormat;
using SmartFormat.Extensions;
using System;

public partial class KoreanSample : Node
{

    public override void _Ready()
    {
        Smart.Default.AddExtensions(new KoreanFormatter(Smart.Default));
        String example = Smart.Format("{0:은} {1:을} 먹었다", "나오", "부엉이");
        GD.Print(example);
        GD.Print(Smart.Format("{0:은} {1:이다}", "안녕", "유예"));
        GD.Print(KoreanTypingEffecter.get_typing(example, 5));
    }

}
