use godot::prelude::*;
use std::{sync::mpsc::*, thread::JoinHandle};
use bevy::prelude::*;

pub enum EcsRequest
{
    ApplicationWillInitialize,
}

pub enum EcsResponse
{
    ApplicationDidInitialize,
}


pub enum EcsEvents
{
    REQUEST(EcsRequest),
    RESPONSE(EcsResponse)
}

pub type EcsSender = Sender<EcsEvents>;

pub struct EcsMain
{
    world: World
}

impl EcsMain
{
    pub fn launch_ecs_thread(node_sender: EcsSender) -> (JoinHandle<()>, EcsSender)
    {
        let (ecs_sender, ecs_receiver) = channel();
        let task: JoinHandle<()> = std::thread::spawn(move || {
            let mut ecs = EcsMain::new();
            loop
            {
                let next = match ecs_receiver.recv()
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

    fn new() -> Self
    {
        let world = World::new();

        Self {
            world
        }
    }

    pub fn handle_request(&mut self, event: EcsRequest)
    {
        match event
        {
            EcsRequest::ApplicationWillInitialize => {}
        }
    }

    pub fn handle_response(&mut self, event: EcsResponse)
    {
        match event
        {
            EcsResponse::ApplicationDidInitialize => {}
        }
    }

}