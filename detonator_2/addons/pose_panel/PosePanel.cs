using Godot;
using System;
using System.Runtime.InteropServices;

[Tool]
public partial class PosePanel : Control
{

    public enum EditMode
    {
        POSE = 0,
        COMPONENT = 1,
    }

    private Panel pose_panel = null;
    private Panel component_panel = null;

    public EditMode edit_mode { get; set; }
    public EditMode _edit_mode = EditMode.POSE;
    public override void _EnterTree()
    {
        base._EnterTree();

        if (Engine.IsEditorHint())
        {
            var inspector = get_inspector();
            inspector.EditedObjectChanged += on_edited_object_changed;
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        if (Engine.IsEditorHint())
        {
            var inspector = get_inspector();
            inspector.EditedObjectChanged -= on_edited_object_changed;
        }
    }

    public override void _Ready()
    {
        base._Ready();

        pose_panel = GetNode<Panel>("PosePanel");
        component_panel = GetNode<Panel>("ComponentPanel");

        edit_mode = EditMode.POSE;

    }

    private void on_edited_object_changed()
    {
        GodotObject obj = get_object();

        if (obj is PoseComponent)
        {
            edit_mode = EditMode.COMPONENT;
        }
        else if (obj is Pose)
        {
            edit_mode = EditMode.POSE;
        }
    }

    private void setEditMode(EditMode t)
    {
        _edit_mode = t;

        if (t == EditMode.POSE)
        {
            pose_panel.Visible = true;
            component_panel.Visible = false;
        }
        else if (t == EditMode.COMPONENT)
        {
            pose_panel.Visible = false;
            component_panel.Visible = true;
        }
    }

    private EditorInspector get_inspector() => EditorInterface.Singleton.GetInspector();
    private GodotObject get_object() => get_inspector().GetEditedObject();
}
