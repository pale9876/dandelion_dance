using Godot;
using System;

[Tool]
[GlobalClass]
public partial class ExtraProgressUI : TextureProgressBar
{
    private const float INIT_VALUE = 0.0f;

    public enum State
    {
        IDLE,
        PHYSICS,
    }

    public enum ApplyMode
    {
        IMMEDIATE = 0,
        LERP = 1,
    }

    [Export] public ApplyMode apply_mode = ApplyMode.IMMEDIATE;
    [Export] public State state = State.IDLE;

    [Export] public double real_value { get => _real_value; set => setRealValue(value); }
    [Export] public double real_max { get => _real_max; set => setRealMax(value); }

    private double _real_value = .5;
    private double _real_max = 1;

    private double purpose_value = INIT_VALUE;
    [Export(PropertyHint.Range, "1.0, 3.0, 0.001")] public double purpose_scale = 1.0;

    public ExtraProgressUI()
    {
        this.Step = 0.001;
        this.Value = .5;
        this.MaxValue = 1.0;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (state == State.IDLE && apply_mode == ApplyMode.LERP)
        {
            if (purpose_value != Value)// if (!Mathf.IsEqualApprox(purpose_value, Value))
            {
                value_lerp();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (state == State.PHYSICS && apply_mode == ApplyMode.LERP)
        {
            if (purpose_value != Value)// if (!Mathf.IsEqualApprox(purpose_value, Value))
            {
                value_lerp();
            }
        }
    }

    public virtual void value_lerp()
    {
        Value = Mathf.MoveToward(Value, purpose_value, this.Step * purpose_scale);
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
        else if (apply_mode == ApplyMode.LERP)
            purpose_value = get_progress_value();
    }

    public void set_max(double value) => real_max = value;
    public void set_value(double value) => real_value = value;
    private double get_progress_value() => _real_value / _real_max;

}