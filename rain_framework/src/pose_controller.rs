use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct PoseController
{
    poses: Dictionary,
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for PoseController
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            poses: Dictionary::new(),
            base            
        }
    }

    fn ready(&mut self)
    {
        self._update();
    }

}

#[godot_api]
impl PoseController
{

    #[func]
    fn _update(&mut self)
    {

    }

}