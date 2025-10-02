using Godot;
using System;
using System.Dynamic;

[Tool]
public partial class PlayerProfile : Control
{

    [Export] public double max_health_value { get => _max_health_value; set => max_health_value_changed(value); }
    [Export] public double current_health_value { get => _current_health_value; set => health_value_changed(value); }

    private double _max_health_value = 1.0;
    private double _current_health_value = 1.0;

    public ExtraProgressUI health_trauma = null;
    public ExtraProgressUI health_progress = null;
    public ExtraProgressUI stress_trauma = null;
    public ExtraProgressUI stress_progress = null;
    public TextureRect profile_over = null;
    public TextureRect profile_under = null;

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
        _max_health_value = value;

        if (health_progress == null && health_trauma == null) return;

        health_progress.real_max = value;
        health_trauma.real_max = value;

    }

    private void health_value_changed(double value)
    {
        _current_health_value = value;

        if (health_progress == null && health_trauma == null) return;

        health_progress.real_value = value;
        health_trauma.real_value = value;
    }

    public void value_change_event_handler()
    {
        
    }

}
