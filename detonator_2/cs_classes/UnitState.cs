using Godot;
using System;

[Tool]
[GlobalClass]
public partial class UnitState : LimboState
{

    public bool cooldown { get => _cooltime > 0.0; }
    [Export] public float initial_cooltime = 0.0f;
    private float cooltime { get => _cooltime; set => setCoolTime(value); }
    private float _cooltime = 0.0f;

    public override void _Enter()
    {
        base._Enter();

        if (initial_cooltime > 0.0f)
        {
            _cooltime = initial_cooltime;
        }
    }

    public override void _Update(double delta)
    {
        base._Update(delta);

        if (Engine.IsEditorHint()) return;

        if (_cooltime > 0.0)
        {
            _cooltime -= (float)delta;
        }
    }

    public void setCoolTime(float value)
    {
        _cooltime = (float)Mathf.Max(value, 0.0);
    }
}
