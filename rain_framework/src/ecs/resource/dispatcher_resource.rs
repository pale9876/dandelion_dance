use bevy::prelude::*;
use crate::ecs::prelude::*;

#[derive(Resource)]
pub struct DispatcherResource
{
    inner: EcsSender,
}

impl DispatcherResource
{
    pub fn new(ecs_sender: EcsSender) -> Self
    {
        Self {
            inner: ecs_sender,

        }
    }

    pub fn dispatch(&self, ev: EcsEvents)
    {
        self.inner.send(ev);
    }
}