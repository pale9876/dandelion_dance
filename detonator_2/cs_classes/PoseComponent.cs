using System;
using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class PoseComponent : CanvasGroup
{

    [Signal] public delegate void pose_changedEventHandler(String pose_name);

    private int id = -1;
    [Export] public bool flip { get => _flip; set => setFlip(value); }
    private bool _flip = false;
    [Export] public Dictionary<StringName, Pose> poses = new Dictionary<StringName, Pose>();
    [Export] public Dictionary<int, Pose> index_list = new Dictionary<int, Pose>();
    [Export] public Pose current_pose { get => _current_pose; set => change_pose(value); }
    private Pose _current_pose = null;
    [Export] public Pose init_pose;
    
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
    }

    public override void _EnterTree()
    {
        base._EnterTree();

        //auto sprites update
        // _update();

        Unit parent = GetParentOrNull<Unit>();
        if (parent != null)
            parent.pose_component = this;

        // connect signals
        // this.ChildEnteredTree += node_entered_event_handler;
        this.VisibilityChanged += on_visibility_changed;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        id = -1;
        current_index = -1;

        poses.Clear();
        index_list.Clear();
        current_pose = null;

        // disconnect
        // this.ChildEnteredTree -= node_entered_event_handler;
        this.VisibilityChanged -= on_visibility_changed;
    }

    public override void _Ready()
    {
        base._Ready();

        Unit parent = GetParentOrNull<Unit>();
        if (parent != null)
            parent.pose_component = null;

        if (Engine.IsEditorHint())
        {
            if (index_list.Count > 0)
                current_index = 0;
        }
        else
        {
            current_index = init_pose.get_id();
        }
    }

    // public void _update()
    // {
    //     id = -1;
    //     current_index = -1;

    //     poses.Clear();
    //     index_list.Clear();

    //     Array<Node> children = GetChildren();
    //     foreach (Node node in children)
    //     {
    //         if (node is Pose)
    //         {
    //             id += 1;
    //             add_pose(id, node as Pose);
    //         }
    //     }

    //     current_index = (index_list.Count > 0) ? 0 : -1;

    // }

    // public void node_entered_event_handler(Node node)
    // {
    //     if (node is Pose)
    //         _update();
    // }

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

    // public void add_pose(int idx, Pose pose)
    // {
    //     String pose_name = pose.Name;
    //     if (!poses.ContainsKey(pose_name)) poses.Add(pose.Name, pose);
    //     if (!index_list.ContainsKey(idx)) index_list.Add(idx, pose);
    //     pose.set_id(idx);
    // }

    // public void remove_pose(Pose pose)
    // {
    //     if (poses.ContainsKey(pose.Name)) poses.Remove(pose.Name);
    //     if (index_list.ContainsKey(pose.get_id())) index_list.Remove(pose.get_id());
    // }

    private void next_pose()
    {
        current_index += 1;
    }

    private void prev_pose()
    {
        current_index -= 1;
    }

    private void change_index(int idx)
    {
        _current_index = Math.Clamp(idx, (index_list.Count > 0) ? 0 : -1, index_list.Count - 1);

        if (idx >= 0 && index_list.ContainsKey(idx))
        {
            current_pose = index_list[idx];
        }
    }

    public void change_pose(Pose pose)
    {
        Pose old_pose = (_current_pose == null) ? null : _current_pose;

        _current_pose = pose;

        foreach (Pose p in index_list.Values)
            p.Visible = (p == pose) ? true : false;

        if (Engine.IsEditorHint()) return;

        if (pose != null)
        {
            pose._pose_entered();
            EmitSignalpose_changed(pose.Name);
        }

        if (old_pose != null)
        {
            old_pose._pose_exited();
        }
    }
    public void insert_pose(Pose pose)
    {
        if (!poses.ContainsKey(pose.Name)) poses.Add(pose.Name, pose);
        id += 1;
        pose.set_id(id);
        if (!index_list.ContainsKey(id)) index_list.Add(pose.get_id(), pose);
    }

    public void delete_pose(Pose pose)
    {
        if (poses.ContainsKey(pose.Name)) poses.Remove(pose.Name);
        if (index_list.ContainsKey(pose.get_id())) index_list.Remove(pose.get_id());
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
}