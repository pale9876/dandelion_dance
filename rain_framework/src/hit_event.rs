use godot::prelude::*;
use godot::classes::{resource, IResource};


#[derive(GodotConvert, Var, Export)]
#[godot(via=i64)]
pub enum EventType
{
    HIT = 0,
    PARRY = 1,
    SHEILD = 2,
    EVADE = 3,

}

#[derive(GodotClass)]
#[class(base=Resource)]
pub struct HitEvent
{
    #[export]
    event_type: EventType,
    #[export]
    force: Vector2,

    base: Base<Resource>
}

#[godot_api]
impl IResource for HitEvent
{
    fn init(base: Base<Resource>) -> Self
    {
        Self {
            event_type : EventType::HIT,
            force: Vector2::ZERO,
            base,
        }
    }
}