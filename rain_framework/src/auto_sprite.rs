use godot::prelude::*;
use godot::classes::{Sprite2D, ISprite2D};

use crate::trigger::{self, Trigger};

#[derive(GodotClass)]
#[class(tool, init, base=Sprite2D)]
pub struct AutoSprite
{
    #[var(
        get = is_playing,
        set = set_play,
        usage_flags = [EDITOR]
    )] #[init(val = false)]play: bool,
    #[export] #[init(val = false)] paused: bool,
    #[var(get, set=set_fps, usage_flags=[EDITOR])] #[init(val = 10.0)] fps: f64,
    #[export] #[init(val = 1.0 / 10.0)] max_time: f64,
    #[export] #[init(val = 1.0 / 10.0)] time: f64,
    #[export] #[init(val = 1.0)] time_scale: f64,
    #[export] trigger: OnEditor<Gd<Trigger>>,

    base: Base<Sprite2D>,
}

#[godot_api]
impl ISprite2D for AutoSprite
{
    fn enter_tree(&mut self)
    {
        self.max_time = 1.0 / self.fps;
        self.time = self.max_time;
    }

    fn physics_process(&mut self, delta: f64)
    {
        if self.play
        {
            
            if self.paused
            {
                return;
            }
            
            let _t: f64 = delta * self.time_scale;
            self.spend_time(_t);

            // let current_time = self.time;
            // godot_print!("{current_time}");

            // match self.trigger.clone().try_to_unique()
            // {
            //     Ok(uq_obj) => {
            //         let opt_obj = Some(uq_obj);
            //         self.signals().triggered().emit(&opt_obj);
            //     },
            //     Err(_) => {}
            // }
        }
    }
}

#[godot_api]
impl AutoSprite
{
    #[signal] fn finished();
    #[signal] fn triggered(res: Option<Gd<Trigger>>);

    #[func]
    fn set_play(&mut self, toggle: bool)
    {
        self.play = toggle;
        if toggle
        {
            if self.paused
            {
                return
            }
            self.time = self.max_time;
        }
        else
        {
            self.stop();
        }
    }

    #[func]
    fn stop(&mut self)
    {
        self.play = false;
        self.base_mut().set_frame(0);
        godot_print!("Stop");

    }

    #[func]
    fn pause(&mut self, toggle: bool)
    {
        self.paused = toggle;
    }

    #[func]
    fn is_playing(&mut self) -> bool
    {
        self.play
    }

    #[func]
    fn spend_time(&mut self, &spend_time: f64)
    {
        self.time -= spend_time;
        if self.time < 0.
        {
            let _frame = self.base().get_frame();
            let max_frame = self.base().get_hframes() - 1;
            if _frame < max_frame
            {
                self.base_mut().set_frame(_frame + 1);
            }
            else
            {
                self.base_mut().set_frame(0);
                godot_print!("return frame 0");
            }
            
            self.time = self.max_time;
        }
    }

    #[func]
    fn set_fps(&mut self, fps: f64)
    {
        self.fps = fps;
        self.fps.set_property(fps);
        self.max_time = 1.0 / self.fps;
    }

}
