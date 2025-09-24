use bevy::prelude::*;

use crate::ecs::prelude::*;

pub struct ApplicationSystem
{

}

impl ApplicationSystem
{
    pub fn application_will_initialize()
    {
        
    }

    pub fn applictaion_did_initialize(disp_resp: Res<DispatcherResource>)
    {
        disp_resp.dispatch(EcsEvents::RESPONSE(EcsResponse::ApplicationDidInitialize));
    }
}