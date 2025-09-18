use godot::prelude::*;
use godot::classes::{Sprite2D, ISprite2D};
use godot::classes::Engine;
use crate::trigger::{self, Trigger};

#[derive(GodotClass)]
#[class(tool, base=Sprite2D)]
pub struct AutoSprite
{
    #[var(
        get = is_playing,
        set = set_play,
        usage_flags = [EDITOR]
    )]
    play: bool,
    
    #[export] paused: bool,
    #[export] fps: f64,
    max_time: f64,
    time: f64,
    #[export] time_scale: f64,
    #[export] trigger: OnEditor<Gd<Trigger>>,

    base: Base<Sprite2D>,
}

#[godot_api]
impl ISprite2D for AutoSprite
{
    fn init(base:Base<Sprite2D>) -> Self
    {
        Self
        {
            play: false,
            paused: false,
            fps: 10.0,
            max_time: 10.0,
            time: 10.0,
            time_scale: 1.0,
            trigger: OnEditor::default(),

            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.max_time = 1.0 / self.fps;
        self.time = self.max_time;
    }

    fn physics_process(&mut self, delta: f64)
    {
        if Engine::singleton().is_editor_hint()
        {
            return;
        }

        if self.play
        {
            let _t: f64 = delta * self.time_scale;

            if self.paused
            {
                return
            }
            
            match self.trigger.clone().try_to_unique()
            {
                Ok(uq_obj) => {
                    let opt_obj = Some(uq_obj);
                    self.signals().triggered().emit(&opt_obj);
                },
                Err((shared_obj, rc)) => {}
            }

            self.spend_time(_t);
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
            self.play();
        }
        else
        {
            
        }
    }

    #[func]
    fn stop()
    {

    }

    #[func]
    fn play(&mut self)
    {
        godot_print!("Play");
        if !self.paused
        {
            return
        }
        self.base_mut().set_frame(0);
    }

    #[func]
    fn pause(&mut self)
    {
        self.play = false;
        self.paused = true;
    }

    #[func]
    fn is_playing(&mut self) -> bool
    {
        self.play
    }

    #[func]
    fn idx_inc(&mut self)
    {
        let _frame = self.base().get_frame();
        self.base_mut().set_frame(_frame + 1);

    }

    #[func]
    fn idx_dec(&mut self)
    {
        let current_frame = self.base().get_frame();
        self.base_mut().set_frame(current_frame - 1);
    }

    #[func]
    fn spend_time(&mut self, &spend_time: f64)
    {
        self.time -= spend_time;
        if self.time < 0.
        {
            self.time = self.max_time;
            let _frame = self.base().get_frame();
            self.base_mut().set_frame(_frame + 1);

        }
    }

}
