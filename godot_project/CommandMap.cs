using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class CommandMap : Resource
{

    [Export] public Dictionary<String, Array<String>> command = new();
    

}
