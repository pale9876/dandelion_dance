use godot::{classes::Engine, prelude::*};

use crate::executioner::Executioner;
use crate::input_monitor::InputMonitor;
use crate::nemesis_system::NemesisSystem;

// test
mod sample_node;

// auto sprite
mod auto_sprite;
mod auto_sprite_component;

// hitbox, hurtbox
mod hitbox;
mod hurtbox;

// pose & pose controller;
mod pose;
mod pose_controller;

// custom resource
mod hit_event;
mod trigger;

// entity
mod entity;
mod squad;
// mod trader;

// body part
mod body_part;
mod body_part_component;

// screen
mod screen_shutter;

// spot & map
mod spot_map;
mod spot;
mod stage;
mod sector;
mod sector_area;
mod stage_collision;
mod sector_collision;

// singleton
mod executioner;
mod nemesis_system;
mod input_monitor;

// ecs
// mod ecs;

struct RainFramworkExtension;

#[gdextension]
unsafe impl ExtensionLibrary for RainFramworkExtension {
    fn on_level_init(level: InitLevel)
    {
        if level == InitLevel::Scene
        {
            let mut engine = Engine::singleton();

            // nemesis_system
            engine.register_singleton(
                &NemesisSystem::class_name().to_string_name(),
                &NemesisSystem::new_alloc()
            );

            // executioner
            engine.register_singleton(
                &Executioner::class_name().to_string_name(),
                &Executioner::new_alloc()
            );

            engine.register_singleton(
                &InputMonitor::class_name().to_string(),
                &InputMonitor::new_alloc()
            );

        }
    }

    fn on_level_deinit(level: InitLevel) {
        if level == InitLevel::Scene
        {
            let mut engine = Engine::singleton();

            let nemsys_class_name = &NemesisSystem::class_name().to_string_name();
            let executioner_class_name = &Executioner::class_name().to_string_name();
            let input_monitor_class_name= &InputMonitor::class_name().to_string_name();

            // queue free nemesis system
            if let Some(nemesys) = engine.get_singleton(nemsys_class_name)
            {
                let mut casted = nemesys.cast::<NemesisSystem>();
                engine.unregister_singleton(nemsys_class_name);
                casted.queue_free();
            }

            // queue free executioner
            if let Some(executioner) = engine.get_singleton(executioner_class_name)
            {
                let mut casted = executioner.cast::<Executioner>();
                engine.unregister_singleton(executioner_class_name);
                casted.queue_free();
            }

            if let Some(input_moni) = engine.get_singleton(input_monitor_class_name)
            {
                let mut casted = input_moni.cast::<InputMonitor>();
                engine.unregister_singleton(input_monitor_class_name);
                casted.queue_free();
            }

        }
    }
}