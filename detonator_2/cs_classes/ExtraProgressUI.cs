using Godot;
using System;

[Tool]
[GlobalClass]
public partial class ExtraProgressUI : TextureProgressBar
{
    private const float INIT_VALUE = 0.0f;

    public enum ApplyMode
    {
        IMMEDIATE = 0,
        LERP = 1,
    }

    public ApplyMode apply_mode = ApplyMode.IMMEDIATE;

    public double real_value { get => _real_value; set => setRealValue(value); }
    public double real_max { get => _real_max; set => setRealMax(value); }

    private double _real_value = .5;
    private double _real_max = 1;

    private double purpose_value = INIT_VALUE;

    public override void _EnterTree()
    {
        base._EnterTree();

        this.Value = .5;
        this.MaxValue = 1.0;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);



    }


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);


    }

    private void setRealValue(double value)
    {
        _real_value = value;
        apply_value();
    }

    private void setRealMax(double value)
    {
        _real_max = value;
        apply_value();
    }

    private void apply_value()
    {
        if (apply_mode == ApplyMode.IMMEDIATE)
            Value = get_progress_value();
        else
            purpose_value = get_progress_value();
    }

    public void set_max(double value) => real_max = value;
    public void set_value(double value) => real_value = value;

    private double get_progress_value() => _real_value / _real_max;

}