using System;
using Godot;
using Godot.Collections;

[Tool]
[GlobalClass]
public partial class PoseComponent : Node2D
{

    [Signal] public delegate void pose_changedEventHandler();

    private int id = -1;

    [Export] Dictionary<StringName, Pose> poses = new Dictionary<StringName, Pose>();
    [Export] Dictionary<int, Pose> index = new Dictionary<int, Pose>();
    [Export] public Pose current_pose { get => _current_pose; set => change_pose(value); }
    private Pose _current_pose = null;

    private int current_index { get => _current_index; set => change_index(value); }
    private int _current_index = -1;


    [ExportToolButton("Update")] private Callable update => Callable.From(_update);
    [ExportToolButton("Go to Prev Pose")] private Callable p_idx => Callable.From(prev_pose);
    [ExportToolButton("Go to Next Pose")] private Callable n_idx => Callable.From(next_pose);


    public override void _EnterTree()
    {
        base._EnterTree();

        //auto sprites update
        _update();

        // connect signals
        this.ChildEnteredTree += node_entered_event_handler;
        this.VisibilityChanged += on_visibility_changed;
    }

    public override void _Ready()
    {
        base._Ready();

        if (index.Count > 0)
            current_index = 0;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        id = -1;
        current_index = -1;

        // disconnect
        this.ChildEnteredTree -= node_entered_event_handler;
        this.VisibilityChanged -= on_visibility_changed;
    }

    public void _update()
    {
        id = -1;
        current_index = -1;

        poses.Clear();
        index.Clear();

        Array<Node> children = GetChildren();
        foreach (Node node in children)
        {
            if (node is Pose)
            {
                id += 1;
                add_pose(id, node as Pose);
            }
        }

        current_index = (index.Count > 0) ? 0 : -1;

    }

    public void node_entered_event_handler(Node node)
    {
        if (node is Pose)
            _update();
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

    public void add_pose(int idx, Pose pose)
    {
        String pose_name = pose.Name;
        if (!poses.ContainsKey(pose_name)) poses.Add(pose.Name, pose);
        if (!index.ContainsKey(idx)) index.Add(idx, pose);
        pose.id = idx;
    }

    public void remove_pose(Pose pose)
    {
        if (poses.ContainsKey(pose.Name)) poses.Remove(pose.Name);
        if (index.ContainsKey(pose.id)) index.Remove(pose.id);
    }

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
        _current_index = Math.Clamp(idx, -1, index.Count);

        if (idx >= 0 && index.ContainsKey(idx))
        {
            current_pose = index[idx];
        }
    }

    public void change_pose(Pose pose)
    {
        Pose old_pose = _current_pose;
        _current_pose = pose;

        foreach (Pose p in poses.Values)
            p.Visible = (p == pose) ? true : false;

        if (Engine.IsEditorHint()) return;

        if (pose != null && IsInstanceValid(pose))
        {
            old_pose._pose_exited();
            pose._pose_entered();
            EmitSignalpose_changed();
        }
    }

    public Array<Pose> get_poses() => poses.Values as Array<Pose>;

}