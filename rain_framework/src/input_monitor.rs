use godot::classes::{Input, InputEventMouseButton};
use godot::global::{maxf, MouseButton};
use godot::prelude::*;
use godot::classes::{INode, InputEvent, Node, InputEventKey};

#[derive(PartialEq)]
#[derive(GodotConvert, Var, Export)]
#[godot(via=i64)]
pub enum State
{
    NONE = 0,
    INDEX = 1,
    EDIT = 2,
}

#[derive(GodotClass)]
#[class(base=Node)]
pub struct InputMonitor
{
    // enum
    state: State,

    #[var] sandbox: bool,

    // data
    #[var] in_control: Option<Gd<Node2D>>,
    #[var] grabbed: Option<Gd<Node2D>>,
    #[var] input_direction: Vector2,
    #[var] direction_record: Array<Vector2>,
    #[var] abs_dir_record: Array<Vector2>,
    mouse_in: Array<Gd<Node2D>>,

    old_dir: Vector2,
    shift: bool,
    
    // input_margin
    #[var] input_margin: f64,
    _margin: f64,

    // index
    #[var] index_dict: Dictionary,

    base: Base<Node>
}

#[godot_api]
impl INode for InputMonitor {

    fn init(base: Base<Node>) -> Self
    {
        Self {
            state: State::NONE,
            sandbox: false,
            in_control: Option::None,
            grabbed: Option::None,
            input_direction: Vector2::ZERO,
            index_dict: Dictionary::new(),
            direction_record: Array::new(),
            abs_dir_record: Array::new(),
            old_dir: Vector2::ZERO,
            shift: false,
            mouse_in: Array::new(),
            
            _margin: InputMonitor::get_default_margin(),
            input_margin: 0.32,

            base
        }
    }

    fn process(&mut self, delta: f64)
    {
        if self._margin > 0.{ self.set_margin(self._margin - delta) }
    }

    fn physics_process(&mut self, _delta: f64)
    {
        let current_input_dir = self.get_current_input_direction();

        if current_input_dir != Vector2::ZERO
        {
            self.old_dir = current_input_dir;
            self._margin = self.input_margin;                    
        }

        if self.sandbox
        {
            if self.grabbed != Option::None
            {
                let grabbed_obj = self.grabbed.clone();
                let mouse_pos = grabbed_obj.unwrap().get_global_mouse_position();
                self.grabbed.as_mut()
                    .unwrap()
                    .set_global_position(mouse_pos.clone());
            }
        }
    }

    fn input(&mut self, ev: Gd<InputEvent>)
    {
        match ev.clone().try_cast::<InputEventKey>()
        {
            Ok(key_ev) => {
                if !key_ev.is_echo()
                {
                    if key_ev.is_pressed()
                    {
                        if key_ev.is_action_pressed(&StringName::from("LShif")) { self.shift = true; }
                        else if key_ev.is_action_pressed(&StringName::from("LControl")) { self.state = State::INDEX; }
                        else if key_ev.is_action_pressed(&StringName::from("LAlt")) { self.state = State::EDIT; }
                        
                        match self.state
                        {
                            State::NONE => {},
                            State::INDEX => self.index_handler(key_ev.clone()),
                            State::EDIT => self.edit_handler(key_ev.clone())
                        }
                    }
                    else if key_ev.is_released()
                    {
                        if key_ev.is_action_released(&StringName::from("LControl")) || key_ev.is_action_released(&StringName::from("LAlt")) { self.state = State::NONE; }
                        else if key_ev.is_action_pressed(&StringName::from("LShif")) { self.shift = false; }
                    }
                }
            },
            Err(_) => {}
        }

        match ev.clone().try_cast::<InputEventMouseButton>()
        {
            Ok(mouse_ev) => {
                if !mouse_ev.is_echo()
                {
                    if mouse_ev.is_pressed()
                    {
                        if mouse_ev.get_button_index() == MouseButton::LEFT
                        {
                            if self.sandbox {self.grabbed = self.mouse_in.front()};
                        }
                    }
                    else if mouse_ev.is_released()
                    {
                        self.grabbed = Option::None;
                    }
                }
            },
            Err(_) => {}
        }

    }

}

#[godot_api]
impl InputMonitor
{
    #[signal] fn control_changed(unit: Option<Gd<Node2D>>);

    fn get_default_margin() -> f64 { 0.32 }

    #[func]
    fn get_current_input_direction(&self) -> Vector2
    {
        let input = Input::singleton();

        let right = StringName::from("right");
        let left = StringName::from("left");

        let up = StringName::from("up");
        let down= StringName::from("down");

        let dir_x = input.get_action_strength(&right) - input.get_action_strength(&left);
        let dir_y = input.get_action_strength(&down) - input.get_action_strength(&up);

        Vector2 { x: dir_x, y: dir_y }
    }

    #[func(virtual)]
    fn index_handler(&mut self, key_ev: Gd<InputEventKey>)
    {
        let num_txt = ["1".into(), "2".into(), "3".into(), "4".into(), "5".into()];
        let key_txt = key_ev.as_text();
        let index = key_txt.to_int();
                            
        if num_txt.contains(&key_txt)
        {
            let controling_entity = self.in_control.clone();
            self.index_dict.set(index, controling_entity);
        }
    }
    
    #[func]
    fn set_margin(&mut self, value: f64)
    {
        self._margin = maxf(value, 0.);

        if self._margin == 0.
        {
            self.abs_dir_record = Array::new();
            self.direction_record = Array::new();
        }
    }

    #[func(virtual)]
    fn edit_handler(&mut self, key_ev: Gd<InputEventKey>)
    {
        let num_txt = ["1".into(), "2".into(), "3".into(), "4".into(), "5".into()];
        let key_txt = key_ev.as_text();
        let index = key_txt.to_int();

        if num_txt.contains(&key_txt) && self.index_dict.contains_key(index)
        {
            self.index_dict.remove(index);
        }
    }


}