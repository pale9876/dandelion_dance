use godot::prelude::*;
use godot::classes::{IRigidBody2D, RigidBody2D};
use crate::auto_sprite::AutoSprite;

#[derive(GodotClass)]
#[class(init, base=RigidBody2D)]
pub struct BodyPart
{
    init_force: Vector2,

    #[export]
    sprite: Option<Gd<AutoSprite>>,
    
    base: Base<RigidBody2D>
}

#[godot_api]
impl IRigidBody2D for BodyPart
{
    fn ready(&mut self)
    {
        let force = self.init_force;
        self.base_mut().apply_impulse(force);
    }
}

#[godot_api]
impl BodyPart
{
    #[func]
    fn from_force(force: Vector2) -> Gd<Self>
    {
        Gd::from_init_fn(|base|{
            Self {
                init_force: force,
                sprite: Option::None,
                base
            }
        })
    }
}