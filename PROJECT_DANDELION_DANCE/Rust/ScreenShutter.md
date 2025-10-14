
----

연속적인 스크린샷 렌더링이 필요할 때 사용하는 객체입니다.
Class MotionBlur에 사용하였습니다.

``` Rust
use godot::prelude::*;
use godot::classes::{ColorRect, IColorRect, ImageTexture};

#[derive(GodotConvert, Var, Export)]
#[godot(via = i64)]
pub enum Mode
{
    IDLE = 0,
    PHYSICS = 1,
}

#[derive(GodotClass)]
#[class(base=ColorRect)]
pub struct ScreenShutter
{
    #[export]
    mode: Mode,
    #[var]
    pre_texture: Option<Gd<ImageTexture>>,
    base : Base<ColorRect>
}
  

#[godot_api]
impl IColorRect for ScreenShutter
{
    fn init(base: Base<ColorRect>) -> Self
    {
        Self {
            mode: Mode::PHYSICS,
            pre_texture: Option::None,
            base
        }
    }
}

#[godot_api]
impl ScreenShutter
{
    #[func]
    fn shutter(&mut self) -> Gd<ImageTexture>
    {
        let img = self.base_mut()
            .get_viewport()
            .unwrap()
            .get_texture()
            .unwrap()
            .get_image()
            .unwrap();

        let img_texture = ImageTexture::create_from_image(&img).unwrap();
        img_texture

    }

    #[func(rename = PHYSICS)] fn physics() -> i64 {Mode::PHYSICS.to_godot()}
    #[func(rename = IDLE)] fn idle() -> i64 {Mode::IDLE.to_godot()}
}


```