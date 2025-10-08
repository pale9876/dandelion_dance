using Godot;
using System;

[Tool]
public partial class PlayerProfile : Control
{
    [Signal] public delegate void health_max_value_changedEventHandler(double value);
    [Signal] public delegate void stress_max_value_changedEventHandler(double value);
    [Signal] public delegate void stress_progress_changedEventHandler(double value);
    [Signal] public delegate void health_progress_changedEventHandler(double value);

    [Export] public double max_health_value { get => _max_health_value; set => max_health_value_changed(value); }
    [Export] public double current_health_value { get => _current_health_value; set => health_value_changed(value); }

    private double _max_health_value = 1.0;
    private double _current_health_value = 1.0;

    [Export] public double max_stress_value { get => _max_stress_value; set => max_stress_value_changed(value); }
    [Export] public double current_stress_value { get => _current_stress_value; set => stress_value_changed(value); }

    private double _max_stress_value = 1.0;
    private double _current_stress_value = 1.0;

    [Export] public ExtraProgressUI health_trauma = null;
    [Export] public ExtraProgressUI health_progress = null;
    [Export] public ExtraProgressUI stress_trauma = null;
    [Export] public ExtraProgressUI stress_progress = null;
    [Export] public TextureRect profile_over = null;
    [Export] public TextureRect profile_under = null;

    public override void _Ready()
    {
        base._Ready();

        health_trauma = GetNode<ExtraProgressUI>("HealthTrauma");
        health_progress = GetNode<ExtraProgressUI>("HealthProgress");
        stress_trauma = GetNode<ExtraProgressUI>("StressTrauma");
        stress_progress = GetNode<ExtraProgressUI>("StressProgress");
        profile_over = GetNode<TextureRect>("ProfileOver");
        profile_under = GetNode<TextureRect>("ProfileUnder");
    }

    private void max_health_value_changed(double value)
    {
        var fixed_value = Mathf.Max(value, 0.0);
        _max_health_value = fixed_value;

        if (health_progress == null && health_trauma == null) return;

        health_progress.real_max = fixed_value;
        health_trauma.real_max = fixed_value;
        current_health_value = Mathf.Min(current_health_value, health_progress.real_max);

        EmitSignalhealth_max_value_changed(fixed_value);

    }
    private void health_value_changed(double value)
    {
        var fixed_value = Mathf.Clamp(value, 0.0, max_health_value);
        _current_health_value = fixed_value;

        if (health_progress == null && health_trauma == null) return;

        health_progress.real_value = fixed_value;
        health_trauma.real_value = fixed_value;

        EmitSignalhealth_progress_changed(value);
    }

    private void max_stress_value_changed(double value)
    {
        var fixed_value = Mathf.Max(value, 0.0);
        _max_stress_value = fixed_value;

        if (stress_trauma == null && stress_progress == null) return;

        stress_progress.real_max = fixed_value;
        stress_trauma.real_max = fixed_value;

        current_stress_value = Mathf.Min(current_stress_value, stress_progress.real_max);

        EmitSignalstress_max_value_changed(fixed_value);
    }

    private void stress_value_changed(double value)
    {
        var fixed_value = Mathf.Clamp(value, 0.0, max_stress_value);
        _current_stress_value = fixed_value;

        if (stress_trauma == null && stress_progress == null) return;

        stress_trauma.real_value = fixed_value;
        stress_progress.real_value = fixed_value;

        EmitSignalstress_progress_changed(fixed_value);
    }

}
