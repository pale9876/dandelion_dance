use godot::global::{PropertyHint, PropertyUsageFlags};
use godot::meta::PropertyInfo;
use godot::prelude::*;
use godot::classes::{resource, IResource};

use crate::entity::Entity;


#[derive(GodotConvert, Var, Export)]
#[derive(PartialEq)]
#[derive(Debug, Clone, Copy)]
#[godot(via=i64)]
pub enum EventType
{
    HIT = 0,
    PARRY = 1,
    SHIELD = 2,
    EVADE = 3,
    BUMP = 4,
    GRABBED = 5,
}

#[derive(GodotClass)]
#[class(tool, base=Resource)]
pub struct HitEvent
{
    #[var(
        get, set=set_hit_event_type
    )] event_type: EventType,
    #[var] from: Option<Gd<Entity>>,
    #[var] to: Option<Gd<Entity>>,
    #[var] force: Vector2,

    base: Base<Resource>
}

#[godot_api]
impl IResource for HitEvent
{
    fn init(base: Base<Resource>) -> Self
    {
        Self {
            event_type: EventType::HIT,
            force: Vector2::ZERO,
            from: Option::<Gd<Entity>>::None,
            to: Option::<Gd<Entity>>::None,
            base,
        }
    }

    fn validate_property(&self, property: &mut PropertyInfo)
    {
        if property.property_name == StringName::from("force")
        {
            let e_type = &self.event_type;
            property.usage
                = if EventType::EVADE == *e_type || EventType::HIT == *e_type || EventType::SHIELD == *e_type
                    {PropertyUsageFlags::DEFAULT}
                else
                    {PropertyUsageFlags::NO_EDITOR};
        }
    }
}

#[godot_api]
impl HitEvent
{
    #[func]
    pub fn set_hit_event_type(&mut self, t: i64)
    {
        self.event_type = EventType::from_godot(t);
        self.base_mut().notify_property_list_changed();
    }

    pub fn get_ev_type(&self) -> EventType
    {
        self.event_type
    }


}