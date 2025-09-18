use godot::prelude::*;
use godot::classes::{IRigidBody2D, RigidBody2D};

#[derive(GodotClass)]
#[class(base=RigidBody2D)]
pub struct BodyPart
{
    base: Base<RigidBody2D>
}

#[godot_api]
impl IRigidBody2D for BodyPart
{
    fn init(base: Base<RigidBody2D>) -> Self
    {
        Self {
            base
        }
    }

}