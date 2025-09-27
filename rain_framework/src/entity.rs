use godot::obj::WithBaseField;
use godot::prelude::*;
use godot::classes::{CharacterBody2D, ICharacterBody2D};
use godot::classes::Engine;

use crate::nemesis_system::NemesisSystem;
use crate::input_monitor::InputMonitor;

#[derive(GodotClass)]
#[class(tool, base=CharacterBody2D)]
pub struct Entity
{
    //props
    #[var(
        set=set_eid, get=get_eid,
        usage_flags=[EDITOR, READ_ONLY]
    )]
    eid: i64,
    #[var(usage_flags=[EDITOR])] unique: bool,
    // #[var()] first_name: GString,
    // #[var()] last_name: GString,
    #[var(usage_flags=[EDITOR])] e_name: GString,
    #[var] is_grabbed: bool,
    #[var] grabbed_by: Option<Gd<Entity>>,

    base: Base<CharacterBody2D>
}

#[derive(GodotClass)]
#[class(tool, init, base=CharacterBody2D)]
pub struct Trader
{
    base: Base<CharacterBody2D>
}

#[godot_api]
impl ICharacterBody2D for Entity
{
    fn init(base:Base<CharacterBody2D>) -> Self
    {
        Self{
            eid: 0,
            unique: false,
            // first_name: GString::default(),
            // last_name: GString::default(),
            e_name: GString::default(),
            is_grabbed: false,
            grabbed_by: Option::None,

            base,
        }
    }

    fn enter_tree(&mut self)
    {
        // set eid
        // let mut nemesis_system = Entity::get_nemesis();
        // nemesis_system.bind_mut().entity_entered(self_refer);
        // self.set_eid(nemesis_system.bind().e_index);

        // connect signals
        if !Engine::singleton().is_editor_hint()
        {
            self.signals().mouse_entered().connect_self(Self::on_mouse_entered);
            self.signals().mouse_exited().connect_self(Self::on_mouse_exited);
        }

    }

    fn physics_process(&mut self, delta: f64)
    {
        self.base_mut().move_and_slide();
    }

    fn exit_tree(&mut self)
    {
        let mut nemesis_system: Gd<NemesisSystem> = Entity::get_nemesis();
        nemesis_system.bind_mut().entity_exited(self.eid);
    }
}


#[godot_api]
impl Entity
{
    #[func]
    pub fn get_eid(&self) -> i64
    {
        self.eid
    }

    #[func]
    pub fn set_eid(&mut self, value: i64)
    {
        self.eid = value;
    }

    #[func]
    fn get_nemesis() -> Gd<NemesisSystem>
    {
        let engine = Engine::singleton();
        
        let nemesis_c_name = &NemesisSystem::class_name().to_string_name();
        
        let result = engine
            .get_singleton(nemesis_c_name)
            .unwrap()
            .cast::<NemesisSystem>();

        result
    }

    #[func]
    fn on_mouse_entered(&mut self)
    {
        let mut input_monitor = Engine::singleton().get_singleton("InputMonitor").unwrap().cast::<InputMonitor>();
        let casted = self.base_mut().clone().upcast::<Node2D>();
        input_monitor.bind_mut().mouse_in.push(&casted);

        godot_print!("Mouse Entered");
    }

    #[func]
    fn on_mouse_exited(&mut self)
    {
        let mut input_monitor = Engine::singleton().get_singleton("InputMonitor").unwrap().cast::<InputMonitor>();
        let casted = self.base_mut().clone().upcast::<Node2D>();
        if input_monitor.bind_mut().mouse_in.clone().contains(&casted.clone())
        {
            input_monitor.bind_mut().mouse_in.erase(&casted);
            godot_print!("Mouse Exited");
        }
    }
}