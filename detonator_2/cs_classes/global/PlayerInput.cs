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

    public Dictionary<int, Entity> index = new Dictionary<int, Entity>();
    private State state = State.NONE;
    private Entity inControl = null;
    public bool shift = false;
    public Vector2 old_input_direction = Vector2.Zero;
    public Vector2 old_abs_input_direction = Vector2.Zero;

    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
    {
        old_input_direction = get_current_direction();
        old_abs_input_direction = get_current_abs_direction();
    }

    public override void _Input(InputEvent @event)
    {
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
    }

    public void index_handler(int idx)
    {
        if (inControl != null)
            index.Add(idx, inControl);
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

}
