using Godot;
using System;

[Tool]
[GlobalClass]
public partial class UnitInfo : Resource
{

    [Export] public String name = "Unknown";
    [Export] public double speed = 300.0;
    [Export] public int max_health_point = 10000;


}
