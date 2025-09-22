use godot::{classes::Engine, prelude::*};

use crate::executioner::Executioner;
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

// body part
mod body_part;
mod body_part_component;

// screen
mod effect_screen;


// singleton
mod executioner;
mod nemesis_system;

struct RainFramworkExtension;

#[gdextension]
unsafe impl ExtensionLibrary for RainFramworkExtension {
    fn on_level_init(level: InitLevel)
    {
        if level == InitLevel::Scene
        {
            // nemesis_system
            Engine::singleton().register_singleton(
                &NemesisSystem::class_name().to_string_name(),
                &NemesisSystem::new_alloc()
            );

            // executioner
            Engine::singleton().register_singleton(
                &Executioner::class_name().to_string_name(),
                &Executioner::new_alloc()
            );
        }
    }

    fn on_level_deinit(level: InitLevel) {
        if level == InitLevel::Scene
        {
            let mut engine = Engine::singleton();
            let nemsys_class_name = &NemesisSystem::class_name().to_string_name();
            let executioner_class_name = &Executioner::class_name().to_string_name();

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

        }
    }
}