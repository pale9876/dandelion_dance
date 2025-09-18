use godot::prelude::*;
use godot::classes::{Node2D, INode2D};
use crate::entity::Entity;

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct Squad
{
    entities: Array<Gd<Entity>>,
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Squad
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            //props
            entities: Array::new(),
            //
            base
        }
    }

    fn enter_tree(&mut self)
    {

    }
}