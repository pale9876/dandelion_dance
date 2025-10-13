using Godot;
using Godot.Collections;

[Tool]
public partial class CameraStatus : Node
{

    private Array<StageCamera> cameras = new();
    public StageCamera current_camera = null;

    public override void _Ready()
    {
        base._Ready();
    }

    public void change_current_camera_target(Node2D node2d)
    {
        current_camera.target = node2d;
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

}
