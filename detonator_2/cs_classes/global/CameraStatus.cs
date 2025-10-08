using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class CameraStatus : Node
{

    private uint index = 0;
    public Dictionary<uint, StageCamera> cameras = new Dictionary<uint, StageCamera>();
    public StageCamera current_camera = null;

    public override void _Ready()
    {
        base._Ready();

    }



}
