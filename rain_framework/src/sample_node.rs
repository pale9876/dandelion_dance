use godot::prelude::*;
use godot::classes::{Node, INode};

#[derive(GodotClass)]
#[class(tool, base=Node)]
pub struct SampleNode
{
    #[var]
    trans_name: GString,
    #[export]
    index: i64,

    base: Base<Node>,
}

#[godot_api]
impl INode for SampleNode
{
    fn init(base:Base<Node>) -> Self
    {
        Self
        {
            trans_name: GString::from("Fire"),
            index: 10,
            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.signals()
            .child_entered_tree()
            .connect_self(Self::_child_entered);
    }
}

#[godot_api]
impl SampleNode
{
    #[func]
    fn _child_entered(&mut self, node: Gd<Node>)
    {
        let n_name = String::from(node.to_string());
        godot_print!("{}", &n_name);
    }
}