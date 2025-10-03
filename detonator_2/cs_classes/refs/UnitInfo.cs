using Godot;
using System;


[GlobalClass]
public partial class UnitInfo : Resource
{

    [Export] public String name = "Unknown";
    [Export] public double speed = 100.0;
    [Export] public int max_health_point = 10000;


}
