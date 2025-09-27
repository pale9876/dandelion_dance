use godot::classes::class_macros::registry::signal;
use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

use crate::auto_sprite::{AutoSprite};


#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct AutoSpriteComponent
{
    #[export]
    dict: Dictionary,

    #[var(
        set=set_idx
    )]
    index: i64,

    base: Base<Node2D>
}

#[godot_api]
impl INode2D for AutoSpriteComponent
{
    fn init(base:Base<Node2D>) -> Self
    {
        Self {
            dict: Dictionary::new(), // <StringName, NodePath>
            index: -1,

            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.signals()
            .child_entered_tree()
            .connect_self(Self::on_child_entered);
    }

    fn ready(&mut self)
    {

    }

    fn exit_tree(&mut self)
    {

    }

}

#[godot_api]
impl AutoSpriteComponent
{
    #[signal] fn sprite_changed(sprite_name: StringName);

    #[func()]
    fn set_idx(&mut self, idx: i64)
    {
        if self.index != idx
        {
            self.index = idx;
            self.idx_changed(idx);
        }
    }

    #[func]
    fn idx_changed(&mut self, idx: i64)
    {
        for node_path in self.dict.values_array().iter_shared()
        {
            let npath = node_path.to::<NodePath>();
            let mut auto_sprite = self.base_mut().get_node_as::<AutoSprite>(&npath);
            let index_num = auto_sprite.get_index() as i64;
            auto_sprite.set_visible(if index_num == idx {true} else {false});
            let c_name = auto_sprite.get_name();
            self.signals().sprite_changed().emit(&c_name);
        }
    }

    #[func]
    fn on_child_entered(&mut self, node: Gd<Node>)
    {
        let auto_sprite = node.try_cast::<AutoSprite>();
        match auto_sprite {
            Ok(auto_sprite) => {
                let path = self.base().get_path_to(&auto_sprite);
                self.dict.set(
                    auto_sprite.get_name(), path
                );
            },
            Err(_) => ()
        }

    }

    #[func]
    fn on_child_exited(&mut self, node: Gd<Node>)
    {
        match node.try_cast::<AutoSprite>()
        {
            Ok(auto_sprite) => {
                let c_name = auto_sprite.get_name();
                if self.dict.contains_key(c_name.clone())
                {
                    self.dict.remove(c_name);
                }
            },
            Err(_) => ()
        }
    }

    #[func]
    fn dict_update(&mut self)
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
                Err(node) => ()
            }
        }

        result
    }

}
