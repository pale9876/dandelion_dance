use bevy::prelude::*;

use crate::ecs::system::prelude::ApplicationSystem;
#[derive(Clone, SystemSet, PartialEq, Eq, Debug, Hash)]
pub enum ApplicationSystemSet
{
    Init,
    Compete,
}

pub struct ApplicationScheduler
{
    inner: Schedule,
}

impl ApplicationScheduler
{
    pub fn new() -> Self
    {
        let mut inner = Schedule::default();

        inner.add_systems(
            (ApplicationSystem::application_will_initialize.in_set(ApplicationSystemSet::Init),
            ApplicationSystem::applictaion_did_initialize
                .in_set(ApplicationSystemSet::Compete)
                .after(ApplicationSystemSet::Init))
        );

        Self {
            inner,
        }

    }


    pub fn run(&mut self, world: &mut World)
    {
        self.inner.run(world);
    }
}