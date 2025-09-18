use godot::prelude::*;
use godot::classes::{Node, INode};

#[derive(GodotClass)]
#[class(tool, base=Node)]
pub struct Executioner
{
    events: Array<VariantArray>, // [0]: event, [1]: from, [2]: to
    base: Base<Node>
}

#[godot_api]
impl INode for Executioner
{
    fn init(base:Base<Node>) -> Self
    {
        Self {
            events: Array::new(),
            base
        }
    }

    fn enter_tree(&mut self)
    {
        
    }

    fn physics_process(&mut self, delta: f64)
    {
        
    }

}

#[godot_api]
impl Executioner
{

}