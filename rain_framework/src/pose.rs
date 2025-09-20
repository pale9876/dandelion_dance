use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct Pose
{
    // props
    

    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Pose
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self
        {
            base
        }
    }
}