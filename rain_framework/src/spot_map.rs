use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

use crate::spot::Spot;


#[derive(GodotClass)]
#[class(base=Node2D)]
pub struct SpotMap
{
    // props
    spots: Dictionary,
    stages: Dictionary,

    base: Base<Node2D>
}

#[godot_api]
impl INode2D for SpotMap
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            spots: Dictionary::new(),
            stages: Dictionary::new(),

            base
        }
    }
}

#[godot_api]
impl SpotMap
{
    #[func]
    fn create_spot()
    {
        
    }

}