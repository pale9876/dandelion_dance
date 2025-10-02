using Godot;
using System;

[Tool]
public partial class BulletTimeManager : Node
{

    const double DEFAULT_TIME_SCALE = 1.0f;

    private double bullet_time { get => _bullet_time; set => setBulletTime(value); }
    private double _bullet_time = DEFAULT_TIME_SCALE;
    public double propose_time_scale { get => _propose_time_scale; set => setProposeTimeScale(value); }
    private double _propose_time_scale = 1.0f;
    public double linear_scale = 3.0;

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_propose_time_scale != bullet_time)
        {
            bullet_time = Mathf.MoveToward(
                bullet_time, _propose_time_scale, delta * linear_scale
            );
        }
    }

    public void start_bullet_time(double time)
    {
        
    }

    public void setBulletTime(double value)
    {
        _bullet_time = value;
        if (!Engine.IsEditorHint())
        {
            Engine.TimeScale = value;
            get_g_anim().emit_time_scale_changed((float)value);
        }
    }

    public void setProposeTimeScale(double value) => _propose_time_scale = Mathf.Clamp(value, 0.001, 2.0);
    private GlobalAnimation get_g_anim() => GetNode<GlobalAnimation>("/root/GlobalAnimation");

}
