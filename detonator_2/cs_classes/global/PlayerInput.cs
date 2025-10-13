using Godot;
using System;
using Godot.Collections;
using System.Linq;

public partial class PlayerInput : Node
{
    public enum State
    {
        NONE,
        INDEX,
        EDIT,
    }

    const float MARGIN_TIME = 0.35f;

    public Dictionary<int, Entity> index = new();
    private State state = State.NONE;
    public Unit in_control { get => _in_control; set => setInControl(value); }
    private Unit _in_control = null;
    public Unit observe { get => _observe; set => setObserved(value); }
    private Unit _observe = null;
    public bool shift = false;
    public Vector2 old_input_direction = Vector2.Zero;
    public Vector2 old_abs_input_direction = Vector2.Zero;

    public Array<Entity> mouse_pointing = new Array<Entity>();

    private CommandProcessor command_processor = new CommandProcessor();

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        old_input_direction = get_current_direction();
        old_abs_input_direction = get_current_abs_direction();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event is InputEventKey)
        {
            InputEventKey key_ev = (@event as InputEventKey);
            if (key_ev.IsEcho())
            {
                if (key_ev.IsPressed())
                {
                    String key_text = key_ev.AsText();
                    String[] arr = ["1", "2", "3", "4", "5", "6"];

                    if (arr.Contains(key_text))
                    {
                        int key_int = key_text.ToInt();
                        if (state == State.INDEX)
                        {
                            index_handler(key_int);
                        }
                        else if (state == State.EDIT)
                        {
                            edit_handler(key_int);
                        }
                    }

                    if (key_ev.IsActionPressed("LControl"))
                    {
                        state = State.INDEX;
                    }
                    else if (key_ev.IsActionPressed("LAlt"))
                    {
                        state = State.EDIT;
                    }
                    else if (key_ev.IsActionPressed("LShift"))
                    {
                        shift = true;
                    }

                }
                else if (key_ev.IsReleased())
                {
                    if (key_ev.IsActionReleased("LControl") || key_ev.IsActionReleased("LAlt"))
                    {
                        state = State.NONE;
                    }
                    else if (key_ev.IsActionReleased("LShift"))
                    {
                        shift = false;
                    }
                }
            }
        }
        else if (@event is InputEventMouseButton)
        {
            if (mouse_pointing.Count > 0)
            {
                if (mouse_pointing[0] is Unit)
                {
                    Unit selected = mouse_pointing[0] as Unit;
                    if (selected.hand_enable)
                    {
                        in_control = selected;
                        get_cam_status().change_current_camera_target(selected);
                    }
                    else
                    {
                        observe = selected;
                    }
                }
                else
                {
                    // TODO ??
                }
            }
        }
    }

    public void index_handler(int idx)
    {
        if (in_control != null)
            index.Add(idx, in_control);
    }

    public void edit_handler(int idx)
    {
        if (index.ContainsKey(idx))
        {
            index.Remove(idx);
        }
    }

    public Vector2 get_current_direction()
    {
        float input_x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        float input_y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
        return new Vector2(input_x, input_y).Normalized();
    }

    public Vector2 get_current_abs_direction() // 우측기준
    {
        float input_x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        float input_y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
        return new Vector2(Mathf.Abs(input_x), input_y);
    }

    public void entity_exit(Entity entity)
    {
        if (in_control == entity) in_control = null;
        if (mouse_pointing.Contains(entity)) mouse_pointing.Remove(entity);
    }

    public void setInControl(Unit unit)
    {
        Unit previous_unit = _in_control;

        if (previous_unit != unit)
        {
            if (previous_unit != null)
            {
                previous_unit.RemoveChild(command_processor);
            }
        }
        else return;

        _in_control = unit;

        unit.AddChild(command_processor);
        GD.Print($"SetControl => {unit}");
    }

    public void setObserved(Unit unit) => _observe = unit;
    private CameraStatus get_cam_status() => GetNode<CameraStatus>("/root/CameraStatus");
}
