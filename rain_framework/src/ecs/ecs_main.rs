use godot::prelude::*;
use std::{sync::mpsc::*, thread::JoinHandle};
use bevy::prelude::*;

use crate::ecs::{resource::prelude::*, scheduler::prelude::ApplicationScheduler};

#[derive(Debug)]
pub enum EcsRequest
{
    ApplicationWillInitialize,
}

#[derive(Debug)]
pub enum EcsResponse
{
    ApplicationDidInitialize,
}

#[derive(Debug)]
pub enum EcsEvents
{
    REQUEST(EcsRequest),
    RESPONSE(EcsResponse)
}

pub type EcsSender = Sender<EcsEvents>;

pub struct EcsMain
{
    application_scheduler: ApplicationScheduler,
    node_sender: EcsSender,
    world: World

}

impl EcsMain
{
    pub fn launch_ecs_thread(node_sender: EcsSender) -> (JoinHandle<()>, EcsSender)
    {
        let (ecs_sender, ecs_receiver) = channel();
        
        let scoped_ecs_sender = ecs_sender.clone();
        let task: JoinHandle<()> = std::thread::spawn(move || {
            let mut ecs = EcsMain::new(scoped_ecs_sender, node_sender);
            loop
            {
                let next = match ecs_receiver.try_recv()
                {
                    Err(_) => continue,
                    Ok(e) => e,
                };

                match next
                {
                    EcsEvents::REQUEST(req) => ecs.handle_request(req),
                    EcsEvents::RESPONSE(resp) => ecs.handle_response(resp),
                }
            }

        });

        return (task, ecs_sender)
    }

    fn new(ecs_sender: EcsSender, node_sender: EcsSender) -> Self
    {
        let mut world = World::new();

        world.insert_resource(DispatcherResource::new(ecs_sender.clone()));

        let application_scheduler = ApplicationScheduler::new();

        Self {
            world,
            application_scheduler,
            node_sender,
        }
    }

    pub fn handle_request(&mut self, req: EcsRequest)
    {
        match req
        {
            EcsRequest::ApplicationWillInitialize => {
                self.application_scheduler.run(&mut self.world)
            }
        }
    }

    pub fn handle_response(&mut self, resp: EcsResponse)
    {
        match resp
        {
            EcsResponse::ApplicationDidInitialize => {
                _ = self.node_sender.send(EcsEvents::RESPONSE(resp));
            }
        }
    }

}