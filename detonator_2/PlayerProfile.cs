using Godot;
using System;

[Tool]
public partial class PlayerProfile : Control
{

    public ExtraProgressUI health_trauma = null;
    public ExtraProgressUI health_progress = null;
    public ExtraProgressUI stress_trauma = null;
    public ExtraProgressUI stress_progress = null;
    public TextureRect profile_over = null;
    public TextureRect profile_under = null;

    public override void _Ready()
    {
        base._Ready();

        health_trauma = GetNodeOrNull<ExtraProgressUI>("HealthTrauma");
        health_progress = GetNodeOrNull<ExtraProgressUI>("HealthProgress");
        stress_trauma = GetNodeOrNull<ExtraProgressUI>("StressTrauma");
        stress_progress = GetNodeOrNull<ExtraProgressUI>("StressProgress");

    }

}
