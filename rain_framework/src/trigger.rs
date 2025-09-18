use godot::prelude::*;
use godot::classes::{Resource, IResource};

#[derive(GodotClass)]
#[class(base=Resource)]
pub struct Trigger
{
    #[export]
    line: i64,
    #[export]
    trigger_name: GString,

    base: Base<Resource>
}

#[godot_api]
impl IResource for Trigger
{
    fn init(base: Base<Resource>) -> Self
    {
        Self {
            line: 0,
            trigger_name: GString::from(""),
            base
        }
    }
}