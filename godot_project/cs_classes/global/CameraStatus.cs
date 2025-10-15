using Godot;
using Godot.Collections;

[Tool]
public partial class CameraStatus : Node
{

    public enum PosMode
    {
        FREECAM,
        PLAYER,
        OBSERVE,
    }

    private Array<StageCamera> cameras = new();
    public StageCamera current_camera = null;

    public PosMode pos_mode { get => _pos_mode; set => pos_mode_changed(value); }
    private PosMode _pos_mode = PosMode.FREECAM;
    public float zoom_step = 0.1f;

    public override void _Ready()
    {
        base._Ready();

        if (current_camera != null)
        {
            if (current_camera.target == null)
            {
                pos_mode = PosMode.FREECAM;
            }
        }
    }

    public void change_current_camera_target(Node2D node2d)
    {
        current_camera.target = node2d;
        
        if (node2d is Unit)
        {
            pos_mode = (node2d as Unit).hand_enable ? PosMode.PLAYER : PosMode.OBSERVE;
        }
    }

    public void insert_camera(StageCamera camera)
    {
        if (!cameras.Contains(camera))
        {
            cameras.Add(camera);
            if (current_camera == null)
            {
                current_camera = camera;
                current_camera.MakeCurrent();
            }
        }
    }

    public void delete_camera(StageCamera camera)
    {
        if (cameras.Contains(camera))
        {
            cameras.Remove(camera);
            if (current_camera == camera)
            {
                current_camera = null;
                if (cameras.Count > 0)
                {
                    cameras[0].MakeCurrent();
                }
            }
        }
    }

    public void next_camera()
    {
        if (cameras.Count > 0)
        {
            current_camera = (cameras.IndexOf(current_camera) == cameras.Count - 1) ?
                cameras[0] : cameras[cameras.IndexOf(current_camera) + 1];
        }
    }

    public void pos_mode_changed(PosMode mode)
    {
        _pos_mode = mode;
        if (mode == PosMode.PLAYER || mode == PosMode.OBSERVE)
        {
            if (current_camera != null)
            {
                current_camera.Zoom = Vector2.One;
            }
        }
    }

}
