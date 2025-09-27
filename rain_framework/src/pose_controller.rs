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

    fn enter_tree(&mut self)
    {
        self.signals().child_entered_tree().connect_self(Self::on_child_entered);
        self.signals().child_exiting_tree().connect_self(Self::on_child_exited);
    }

    fn ready(&mut self)
    {

    }

}

#[godot_api]
impl PoseController
{
    fn on_child_entered(&mut self, node: Gd<Node>)
    {

    }

    fn on_child_exited(&mut self, node: Gd<Node>)
    {

    }

}