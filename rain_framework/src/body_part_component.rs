use godot::prelude::*;
use godot::classes::{INode2D, Node2D, RigidBody2D};

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct BodyPartComponent
{
    #[export] body_parts: Array<Gd<PackedScene>>,
    
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for BodyPartComponent
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            body_parts: Array::new(),

            base
        }
    }
}

#[godot_api]
impl BodyPartComponent
{
    #[func]
    fn create_parts(&mut self)
    {
        for body_part_scene in self.body_parts.iter_shared()
        {
            if body_part_scene.can_instantiate()
            {
                
            }
        }
    }
}