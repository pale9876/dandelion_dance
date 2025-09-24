use godot::prelude::*;
use godot::classes::{Node2D, INode2D};


#[derive(GodotClass)]
#[class(base=Node2D)]
pub struct Spot
{
    debug_colour: Color,
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Spot
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            debug_colour: Color::RED,
            base
        }
    }

    fn draw(&mut self)
    {

    }

}