using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Layer : Node2D
{
    private enum LayerType
    {
        NORMAL,
        BACKGROUND,
        FOREGROUND,
    }

    [Export] private LayerType init_layer_type = LayerType.NORMAL;

    private LayerType layer_type { get => _layer_type; set => setLayerType(value); }
    private LayerType _layer_type = LayerType.NORMAL;

    public override void _Ready()
    {
        base._Ready();

        layer_type = init_layer_type;
    }

    private void setLayerType(LayerType type)
    {
        _layer_type = type;
        EffectBus2D e2b = get_e2b();

        switch (type)
        {
            case LayerType.BACKGROUND:
                if (e2b.background_effect_layer != null)
                    GD.PushError($"Effect2dBug => 이미 background_effect_layer가 존재하지만 다음으로 교체합니다 -> {this}");
                e2b.background_effect_layer = this;
                break;
            case LayerType.FOREGROUND:
                if (e2b.foreground_effect_layer != null)
                    GD.PushError($"Effect2dBug => 이미 background_effect_layer가 존재하지만 다음으로 교체합니다 -> {this}");
                e2b.foreground_effect_layer = this;
                break;
            case LayerType.NORMAL:
                e2b.clear_role(this);
                break;
        }
    }
    
    private EffectBus2D get_e2b() => GetNode<EffectBus2D>("/root/EffectBus2D");

}
