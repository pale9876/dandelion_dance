use std::ffi::c_void;

use godot::prelude::*;
use godot::classes::{INode, Node};
use godot::classes::Engine;

use crate::entity::Entity;
use crate::hit_event::*;

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
        if Engine::singleton().is_editor_hint()
        {
            return
        }

        if !self.events.is_empty()
        {
            self.ev_handler();
        }
    }
}

#[godot_api]
impl Executioner
{
    fn ev_handler(&mut self)
    {
        for event in self.events.iter_shared()
        {
            let ev = event.at(0);
            let from = event.at(1);
            let to = event.at(2);
            
            let from_entity = from.to::<Gd<Entity>>();
            let to_entity = to.to::<Gd<Entity>>();
            
            match ev.to::<EventType>()
            {
                EventType::HIT => {
                    let velocity = to_entity.get_velocity();
                    // to_entity.set_velocity();
                },
                EventType::PARRY => {},
                EventType::SHEILD => {},
                EventType::EVADE => {},
                EventType::BUMP => {},
                EventType::GRABBED => {},
            }
        }
    }

}