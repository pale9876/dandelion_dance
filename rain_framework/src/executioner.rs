use godot::prelude::*;
use godot::classes::{INode, Node};
use godot::classes::Engine;

use crate::entity::Entity;
use crate::hit_event::*;

#[derive(GodotClass)]
#[class(tool, base=Node)]
pub struct Executioner
{
    #[var]
    verbose: bool,
    events: Array<Option<Gd<HitEvent>>>,
    base: Base<Node>
}

#[godot_api]
impl INode for Executioner
{
    fn init(base:Base<Node>) -> Self
    {
        Self {
            verbose: false,
            events: Array::new(),
            base
        }
    }

    fn enter_tree(&mut self)
    {
        
    }

    fn physics_process(&mut self, _delta: f64)
    {
        if Engine::singleton().is_editor_hint() { return }
        
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
            if let mut ev = event.unwrap()
            {
                let ev_type = ev.bind().event_type;
                let force = ev.bind().get_force();
                let mut from = ev.bind_mut().get_from().unwrap();
                let mut to = ev.bind_mut().get_to().unwrap();

                match ev_type
                {
                    // Hit
                    EventType::HIT => {
                        let velocity = to.get_velocity();
                        to.set_velocity(velocity + force);
                    },
                    // Parry
                    EventType::PARRY => {
                        
                    },
                    // Shield
                    EventType::SHIELD => {

                    },
                    // Evade
                    EventType::EVADE => {

                    },
                    // Bump
                    EventType::BUMP => {

                    },
                    // Grabbed
                    EventType::GRABBED => {

                    }
                }
            }
        }

    }
}