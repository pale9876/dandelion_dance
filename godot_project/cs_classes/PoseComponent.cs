using System;
using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class PoseComponent : CanvasGroup
{

    private int id = -1;
    [Export] public Unit root = null;
    [Export] public bool flip { get => _flip; set => setFlip(value); }
    private bool _flip = false;
    [Export] public TriggerMap trigger_map { get => _trigger_map; set => setTriggerMap(value); }
    private TriggerMap _trigger_map = null;
    [Export] public Dictionary<String, Pose> poses = new Dictionary<String, Pose>();
    [Export] public Array<Pose> index_list = new Array<Pose>();
    [Export] public Pose current_pose { get => _current_pose; set => change_pose(value); }
    private Pose _current_pose = null;
    [Export] public Pose init_pose;
    [Export] public Array<Sprite2D> effect_filters = new();
    
    private int current_index { get => _current_index; set => change_index(value); }
    private int _current_index = -1;

    // [ExportToolButton("Update")] private Callable update => Callable.From(_update);
    [ExportToolButton("Go to Prev Pose")] private Callable p_idx => Callable.From(prev_pose);
    [ExportToolButton("Go to Next Pose")] private Callable n_idx => Callable.From(next_pose);

    public PoseComponent()
    {
        Shader sd = GD.Load<Shader>("res://shaders/group_outline.gdshader");
        ShaderMaterial shader_material = new ShaderMaterial();
        shader_material.Shader = sd;
        Material = shader_material;
        ClipChildren = ClipChildrenMode.AndDraw;
    }

    public override void _EnterTree()
    {
        base._EnterTree();

        Unit parent = GetParentOrNull<Unit>();
        if (parent != null)
        {
            Owner = parent;
            root = parent;
            parent.pose_component = this;
        }

        VisibilityChanged += on_visibility_changed;
        ChildOrderChanged += on_child_order_changed;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        id = -1;

        Unit parent = GetParentOrNull<Unit>();
        if (parent != null)
        {
            parent.pose_component = null;
        }

        poses.Clear();

        VisibilityChanged -= on_visibility_changed;
        ChildOrderChanged -= on_child_order_changed;
    }

    public override void _Ready()
    {
        base._Ready();

        Unit parent = GetParentOrNull<Unit>();

        if (Engine.IsEditorHint())
        {
            if (index_list.Count > 0)
            {
                current_index = 0;
            }
        }
        else
        {
            change_pose(init_pose);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Engine.IsEditorHint()) return;

        if (current_pose != null)
        {
            current_pose._pose_update(delta);
        }
    }

    public void child_renamed()
    {
        poses.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is Pose)
            {
                poses.Add(node.Name, node as Pose);
            }
        }
    }

    public void on_child_order_changed()
    {
        index_list.Clear();

        foreach (Node node in GetChildren())
        {
            if (node is Pose)
            {
                index_list.Add(node as Pose);
            }
        }
    }

    public void on_visibility_changed()
    {
        if (Visible)
        {
            foreach (Pose pose in get_poses())
                pose.Visible = (current_pose == pose) ? true : false;
        }
        else
        {
            foreach (Node node in GetChildren())
            {
                if (node is Node2D)
                {
                    Node2D node_2d = node as Node2D;
                    node_2d.Visible = this.Visible;
                }
            }
        }
    }

    public virtual void trigger_map_changed_event_handler() { }

    private void next_pose()
    {
        current_index += 1;
        GD.Print($"{this.Name} :: current_index => {current_index}");
    }

    private void prev_pose()
    {
        current_index -= 1;
        GD.Print($"{this.Name} :: current_index => {current_index}");
    }

    private void change_index(int idx)
    {
        _current_index = Math.Clamp(idx, (index_list.Count > 0) ? 0 : -1, index_list.Count - 1);

        if (_current_index > -1)
        {
            current_pose = index_list[_current_index];
        }
    }

    public void change_pose(Pose pose)
    {
        Pose old_pose = (_current_pose == null) ? null : _current_pose;

        if (pose == current_pose) return;
        
        _current_pose = pose;

        foreach (Pose p in index_list)
        {
            p.Visible = (p == pose) ? true : false;
        }

        if (Engine.IsEditorHint()) return;

        if (pose != null)
        {
            pose._pose_entered();
            // EmitSignalpose_changed();
        }

        if (old_pose != null)
        {
            old_pose._pose_exited();
        }
    }

    public bool change_to(String pose_name)
    {
        if (poses.ContainsKey(pose_name))
        {
            change_pose(poses[pose_name]);
            return true;
        }
        
        return false;

    }

    public void insert_pose(Pose pose)
    {
        if (!poses.ContainsKey(pose.Name)) poses.Add(pose.Name, pose);
        if (!index_list.Contains(pose)) index_list.Add(pose);
        id += 1;
        pose.set_id(id);
    }

    public void delete_pose(Pose pose)
    {
        if (poses.ContainsKey(pose.Name)) poses.Remove(pose.Name);
        if (index_list.Contains(pose)) index_list.Remove(pose);
    }

    public Array<Pose> get_poses() => poses.Values as Array<Pose>;
    public void setFlip(bool toggle)
    {
        _flip = toggle;

        foreach (Pose pose in poses.Values)
        {
            pose.flip(toggle);
        }
    }

    private void setTriggerMap(TriggerMap value)
    {
        _trigger_map = value;
        if (value != null)
        {
            trigger_map_changed_event_handler();
        }
    }

}