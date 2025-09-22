use godot::prelude::*;
use godot::classes::{IRigidBody2D, RigidBody2D};
use crate::auto_sprite::AutoSprite;

#[derive(GodotClass)]
#[class(init, base=RigidBody2D)]
pub struct BodyPart
{
    init_force: Vector2,

    #[init(node="AutoSprite")]
    sprite: OnReady<Gd<Node>>,
    
    base: Base<RigidBody2D>
}

#[godot_api]
impl IRigidBody2D for BodyPart
{
    fn ready(&mut self)
    {
        // self.base_mut().apply_impulse(impulse);
    }

    

}

#[godot_api]
impl BodyPart
{

}