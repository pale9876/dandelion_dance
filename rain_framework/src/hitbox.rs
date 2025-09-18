use godot::prelude::*;
use godot::classes::{Area2D, IArea2D};

pub enum Grade
{

}

#[derive(GodotClass)]
#[class(tool, base=Area2D)]
pub struct Hitbox
{
    base: Base<Area2D>
}

#[godot_api]
impl IArea2D for Hitbox
{
    fn init(base: Base<Area2D>) -> Self
    {
        Self {
            base
        }
    }
}