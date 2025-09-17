use godot::prelude::*;
use godot::classes::{Sprite2D, ISprite2D};

#[derive(GodotClass)]
#[class(tool, base=Sprite2D)]
pub struct AutoSprite
{
    #[export]
    play: bool,
    #[export]
    paused: bool,

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
            base
        }
    }

    fn physics_process(&mut self, delta: f64)
    {
        if self.play
        {
            let current_idx = self.base().get_frame();
            self.base_mut().set_frame(current_idx + 1);
        }
    }

}


#[godot_api]
impl AutoSprite
{
    #[signal]
    fn finished();

    #[func]
    fn play(&mut self)
    {
        self.set_play(true);
    }

    #[func]
    fn pause(&mut self)
    {
        self.set_play(false);
        self.set_paused(true);
    }

    #[func]
    fn is_playing(&mut self) -> bool
    {
        self.play
    }
}