using Godot;
using System;

[Tool]
public partial class PosePanel : Control
{

    public enum EditMode
    {
        NONE = 0,
        POSE = 1,
        COMPONENT = 2,
    }

    // Panel
    private Panel pose_panel = null;
    private Panel component_panel = null;

    // Attributes
    private Label pose_name = null;
    private AutoSprite sprite = null;

    public EditMode edit_mode { get => _edit_mode; set => setEditMode(value); }
    public EditMode _edit_mode = EditMode.NONE;

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

    public override void _Notification(int what)
    {
        base._Notification(what);

        switch (what)
        {
            case (int)NotificationExitTree:
                edit_mode = EditMode.NONE; 
                pose_panel = null;
                component_panel = null;
                sprite = null;
                pose_name = null;
                break;
        }
    }

    public override void _Ready()
    {
        base._Ready();

        // Get Panel
        pose_panel = GetNode<Panel>("PosePanel");
        component_panel = GetNode<Panel>("ComponentPanel");

        // Get Attribute
        pose_name = GetNode<Label>("%PoseName");
        sprite = GetNode<AutoSprite>("%Sprite");

        // Set Visible
        pose_panel.Visible = false;
        component_panel.Visible = false;

    }

    private void on_edited_object_changed()
    {
        GodotObject obj = get_object();

        if (obj is PoseComponent)
        {
            EditMode old_mode = edit_mode;
            edit_mode = EditMode.COMPONENT;
            if (old_mode != edit_mode)
            {
                GD.Print($"Pose Panel::EditMode => {edit_mode}");
                component_selected_event_handler(obj as PoseComponent);
            }
        }
        else if (obj is Pose)
        {
            EditMode old_mode = edit_mode;
            edit_mode = EditMode.POSE;
            if (old_mode != edit_mode)
            {
                GD.Print($"Pose Panel::EditMode => {edit_mode}");
                pose_selected_event_handler(obj as Pose);
            }
        }
    }

    private void pose_selected_event_handler(Pose pose)
    {
        pose_name.Text = pose.Name;
    }

    private void component_selected_event_handler(PoseComponent pose_component)
    {

    }

    private void setEditMode(EditMode t)
    {
        _edit_mode = t;

        if (t == EditMode.POSE)
        {
            pose_edit_entered();
        }
        else if (t == EditMode.COMPONENT)
        {
            component_edit_entered();
        }
    }

    private void pose_edit_entered()
    {
        if (pose_panel != null) pose_panel.Visible = true;
        if (component_panel != null) component_panel.Visible = false;
    }

    private void component_edit_entered()
    {
        if (pose_panel != null) pose_panel.Visible = false;
        if (component_panel != null) component_panel.Visible = true;
    }

    private EditorInspector get_inspector() => EditorInterface.Singleton.GetInspector();
    private GodotObject get_object() => get_inspector().GetEditedObject();
}
