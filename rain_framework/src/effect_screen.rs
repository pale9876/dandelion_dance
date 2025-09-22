use godot::prelude::*;
use godot::classes::{ColorRect, IColorRect};

#[derive(GodotClass)]
#[class(tool, base=ColorRect)]
pub struct EffectScreen
{
    blur : bool,

    base : Base<ColorRect>
}


#[godot_api]
impl IColorRect for EffectScreen
{
    fn init(base: Base<ColorRect>) -> Self
    {
        Self {
            blur: false,
            
            base
        }
    }
}
