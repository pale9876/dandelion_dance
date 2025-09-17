use godot::prelude::*;

mod sample_node;
mod hit_event;
mod auto_sprite;
mod auto_sprite_component;
mod entity;

struct RainFramworkExtension;

#[gdextension]
unsafe impl ExtensionLibrary for RainFramworkExtension {
    
}