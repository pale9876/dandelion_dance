using Godot;
using System;


[GlobalClass]
public partial class KoreanLabel : Label
{

    private KoreanTypingEffecter _effect;

    [Export] public bool playing = false;

    public KoreanLabel()
    {
        this._effect = new KoreanTypingEffecter("");
    }

    public override void _EnterTree()
    {

        String text = this.Text;
        String oro = text;

    }



}
