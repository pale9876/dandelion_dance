use godot::meta::PropertyInfo;
use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

use crate::auto_sprite::{self, AutoSprite};


#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct AutoSpriteComponent
{
    #[export]
    sprites: Array<Option<Gd<AutoSprite>>>,
    #[export]
    dict: Dictionary,
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for AutoSpriteComponent
{
    fn init(base:Base<Node2D>) -> Self
    {
        Self {
            sprites : Array::new(),
            dict: Dictionary::new(),
            base
        }
    }

    fn ready(&mut self)
    {
        let childs: Array<Gd<Node>> = self.base().get_children();
        for node in childs.iter_shared()
        {
            let auto_sprite = node.try_cast::<AutoSprite>();
            match auto_sprite
            {
                Ok(auto_sprite) => {
                    self.dict.set(
                        auto_sprite.get_name(),
                        self.base().get_path_to(&auto_sprite)
                    );
                },
                Err(node) => {},
            }
        }
    }

}


#[godot_api]
impl AutoSpriteComponent
{

    #[func]
    fn move_next(&mut self)
    {

    }

}