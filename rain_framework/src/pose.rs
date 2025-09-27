use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

use crate::hitbox::Hitbox;
use crate::hurtbox::Hurtbox;

#[derive(GodotClass)]
#[class(tool, init, base=Node2D)]
pub struct Pose
{
    #[export] hitbox: Option<Gd<Hitbox>>,
    #[export] hurtbox: Option<Gd<Hurtbox>>,
    // props
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Pose
{

    

}