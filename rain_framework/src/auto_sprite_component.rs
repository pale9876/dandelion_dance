use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

use crate::auto_sprite::{AutoSprite};


#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct AutoSpriteComponent
{
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
            dict: Dictionary::new(),
            base
        }
    }

    fn ready(&mut self)
    {
        self.update();
    }

}


#[godot_api]
impl AutoSpriteComponent
{

    #[func]
    fn move_next(&mut self)
    {

    }

    #[func]
    fn update(&mut self)
    {
        self.dict.clear();
        
        for sprite in self.get_sprites().iter_shared()
        {
            let sp_unwraped = sprite.unwrap();
            let sp_name = sp_unwraped.get_name();
            let path = self.base().get_path_to(&sp_unwraped);
            self.dict.set(
                sp_name, path
            );
        }
    }

    #[func]
    fn get_sprites(&mut self) -> Array<Option<Gd<AutoSprite>>>
    {
        let mut result: Array<Option<Gd<AutoSprite>>> = Array::new();
        let childs: Array<Gd<Node>> = self.base().get_children();

        for node in childs.iter_shared()
        {
            let auto_sprite = node.try_cast::<AutoSprite>();
            match auto_sprite
            {
                Ok(auto_sprite) => {
                    let cast_success = Some(auto_sprite);
                    result.push(&cast_success);
                },
                Err(node) => {}
            }
        }

        result
    }

}
