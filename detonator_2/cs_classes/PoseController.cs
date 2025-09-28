using Godot;
using System;
using Godot.Collections;
using System.Security.Cryptography;

#if TOOLS
[Tool]
[GlobalClass]
public partial class PoseController : Node
{

    [Export] Dictionary<StringName, AutoSprite> auto_sprites;
    [Export] Area2D hitbox;
    [Export] Area2D hurtbox;

    public override void _Ready()
    {

        //auto sprites update
        _update();

        // connect signals
        this.ChildEnteredTree += node_entered_event_handler;
        this.ChildExitingTree += node_exited_event_handler;
    }

    public void _update()
    {
        Array<Node> children = GetChildren();
        foreach (Node node in children)
        {
            if (node is AutoSprite)
            {
                auto_sprites.Add(node.Name, node as AutoSprite);    
            }
        }
    }

    public void node_entered_event_handler(Node node)
    {
        if (node is AutoSprite)
        {
            var auto_sprite = node as AutoSprite;
            auto_sprites.Add(node.Name, auto_sprite);
        }
    }

    public void node_exited_event_handler(Node node)
    {
        if (node is AutoSprite)
        {
            var n = node.Name;

            if (auto_sprites.ContainsKey(n))
            {
                auto_sprites.Remove(n);
            }
        }
    }
}
#endif