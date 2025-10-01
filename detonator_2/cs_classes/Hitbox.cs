using Godot;
using System;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class Hitbox : Area2D
{

    public bool execute = false;
    private int id = -1;
    
    private Array<Hurtbox> entered_boxes = [];
    [Export] private Array<UnitCollision> collisions = new Array<UnitCollision>();
    [Export] private Color debug_colour { get => _debug_color; set => set_debug_color(value); }
    private Color _debug_color = new Color();

    public override void _EnterTree()
    {
        base._EnterTree();

        VisibilityChanged += on_visible_changed;
        ChildEnteredTree += on_child_entered;

        if (!Engine.IsEditorHint())
        {
            AreaEntered += on_area_entered;
            AreaExited += on_area_exited;
        }
    }

    public override void _ExitTree()
    {

        base._ExitTree();

        VisibilityChanged -= on_visible_changed;
        ChildEnteredTree -= on_child_entered;

        id = -1;
        if (!Engine.IsEditorHint())
        {
            entered_boxes.Clear();
            AreaEntered -= on_area_entered;
            AreaExited -= on_area_exited;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (execute)
        {

        }
        foreach (Hurtbox hurt_box in entered_boxes)
        {
            var query = PhysicsRayQueryParameters2D.Create(this.GlobalPosition, hurt_box.GlobalPosition, 0, [this.GetRid()]);
            query.CollideWithAreas = true;
            var result = GetWorld2D().DirectSpaceState.IntersectRay(query);
            if (result.Count > 0)
            {
                if (result.ContainsKey("collider"))
                {
                    GD.Print($"{result["collider"]}");
                    Node p_obj = result["collider"].As<Node>();
                    if (p_obj is Hurtbox && p_obj == hurt_box)
                    {
                        GD.Print($"{Name} Hit to {hurt_box.Name}");
                        // Executioner executioner = GetNode<Executioner>("root/Executioner");
                        // executioner.add_event(
                        //     HitEvent.EventType.HIT, this, hurt_box.Owner,
                        //     new Dictionary { }
                        // );
                    }
                }
            }
        }
    }

    public void _update()
    {
        collisions.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is UnitCollision)
            {
                collisions.Add(node as UnitCollision);
            }
        }
    }

    public void set_debug_color(Color color)
    {
        _debug_color = color;
        if (collisions.Count != 0)
        {
            foreach (CollisionShape2D collision in collisions)
            {
                collision.DebugColor = color;
            }
        }
    }

    public void on_child_entered(Node node)
    {
        if (node is UnitCollision)
        {
            _update();
        }
    }

    public void on_child_exited(Node node)
    {
        if (node is UnitCollision)
        {
            UnitCollision collision = node as UnitCollision;
        }
    }

    public void on_visible_changed()
    {
        foreach (UnitCollision collision in collisions)
        {
            collision.Visible = this.Visible;
        }
        // GD.Print($"{Name} => Visibility Changed");
    }

    public void on_area_entered(Area2D area)
    {
        if (area is Hurtbox)
        {
            entered_boxes.Add(area as Hurtbox);
        }
    }

    public void on_area_exited(Area2D area)
    {
        if (area is Hurtbox)
        {
            entered_boxes.Add(area as Hurtbox);
        }
    }

}
