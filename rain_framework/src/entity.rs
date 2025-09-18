use godot::prelude::*;
use godot::classes::{CharacterBody2D, ICharacterBody2D};
use godot::classes::Engine;
use godot::global::godot_print;

use crate::nemesis_system::NemesisSystem;

#[derive(GodotClass)]
#[class(tool, base=CharacterBody2D)]

pub struct Entity
{
    
    //props
    #[var]
    pub eid: i64,
    #[var]
    e_name: GString,

    base: Base<CharacterBody2D>
}

#[godot_api]
impl ICharacterBody2D for Entity
{
    fn init(base:Base<CharacterBody2D>) -> Self
    {
        Self{
            eid: 0,
            e_name: GString::from(""),
            base,
        }
    }

    fn enter_tree(&mut self)
    {
        let engine = Engine::singleton();
        let nemesis_class_name = &NemesisSystem::class_name().to_string_name();

        if let Some(singleton) = engine.get_singleton(nemesis_class_name)
        {
            let mut nemesis_system = singleton.cast::<NemesisSystem>();
            nemesis_system.bind_mut().e_index += 1;
            self.set_eid(nemesis_system.bind().e_index);
        }
    }
}

#[godot_api]
impl Entity
{


}