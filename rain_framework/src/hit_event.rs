use godot::global::{PropertyHint, PropertyUsageFlags};
use godot::meta::PropertyInfo;
use godot::prelude::*;
use godot::classes::{resource, IResource};


#[derive(GodotConvert, Var, Export)]
#[derive(PartialEq)]
#[godot(via=i64)]
pub enum EventType
{
    HIT = 0,
    PARRY = 1,
    SHEILD = 2,
    EVADE = 3,
    BUMP = 4,
}

#[derive(GodotClass)]
#[class(tool, base=Resource)]
pub struct HitEvent
{
    #[var(
        get, set=set_hit_event_type,
        usage_flags = [EDITOR]
    )] event_type: EventType,

    #[var(
    )] force: Vector2,

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
            base,
        }
    }

    fn validate_property(&self, property: &mut PropertyInfo)
    {
        if property.property_name == StringName::from("force")
        {
            let e_type = &self.event_type;
            property.usage
                = if EventType::EVADE == *e_type || EventType::HIT == *e_type || EventType::SHEILD == *e_type
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
}