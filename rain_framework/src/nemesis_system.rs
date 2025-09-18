use godot::prelude::*;
use godot::classes::{Node, INode};


#[derive(GodotClass)]
#[class(tool, base=Node)]
pub struct NemesisSystem
{
    #[var(
        get = get_idx, set = set_idx
    )]
    pub e_index: i64,
    base: Base<Node>
}

#[godot_api]
pub impl INode for NemesisSystem
{
    fn init(base: Base<Node>) -> Self
    {
        Self {
            e_index: -1,
            base
        }
    }
}

#[godot_api]
impl NemesisSystem
{
    #[func]
    fn get_idx(&self) -> i64
    {
        self.e_index
    }

    #[func]
    fn set_idx(&mut self, value: i64)
    {
        self.e_index = value;
    }
}