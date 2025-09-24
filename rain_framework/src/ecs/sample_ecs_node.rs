use std::{sync::mpsc::*, thread::JoinHandle};
use godot::prelude::*;
use godot::classes::{Node2D, INode2D};

use crate::ecs::ecs_main::EcsMain;
use crate::ecs::prelude::*;

#[derive(GodotClass)]
#[class(base=Node2D)]
pub struct SampleEcsNode
{
    ecs_task: JoinHandle<()>,
    ecs_sender: EcsSender,
    node_sender: EcsSender, 
    node_receiver: Receiver<EcsEvents>,

    base: Base<Node2D>,
}


#[godot_api]
impl INode2D for SampleEcsNode
{
    fn init(base: Base<Node2D>) -> Self
    {
        let (node_sender, node_receiver) = channel::<EcsEvents>();
        let (ecs_task, ecs_sender) = EcsMain::launch_ecs_thread(node_sender.clone());

        Self
        {
            ecs_task,
            ecs_sender,
            node_receiver,
            node_sender,
            base
        }
    }

    fn ready(&mut self)
    {
        self.send_ecs_request(EcsRequest::ApplicationWillInitialize);
    }

    fn process(&mut self, delta: f64)
    {
        let event = match self.node_receiver.try_recv()
        {
            Err(_) => return,
            Ok(e) => e,
        };

        godot_print!("{:?}", event);

        let response = match event
        {
            EcsEvents::REQUEST(_) => return,
            EcsEvents::RESPONSE(res) => res,
        };

        godot_print!("Event {:?}", response);
    }

}

impl SampleEcsNode
{
    pub fn send_ecs_request(&self, req: EcsRequest)
    {
        let _ = self.ecs_sender.send(EcsEvents::REQUEST(req));
    }
}