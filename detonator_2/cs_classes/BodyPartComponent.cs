using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class BodyPartComponent : Node2D
{
    [Export] private Array<PackedScene> part_scenes = new Array<PackedScene>();

    public void spawn_body_parts(Vector2 min_force, Vector2 max_force)
    {
        foreach (PackedScene scene in part_scenes)
        {
            RandomNumberGenerator rand = new RandomNumberGenerator();
            BodyPart body_part = scene.Instantiate<BodyPart>();
            body_part.init_force = new Vector2(
                    rand.RandfRange(min_force.X, max_force.X),
                    rand.RandfRange(min_force.Y, max_force.Y)
                );
            CallDeferred("AddChild", body_part);
        }
    }

}
