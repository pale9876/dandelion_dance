use godot::prelude::*;
use godot::classes::{ColorRect, Engine, IColorRect, Image, ImageTexture};

#[derive(GodotClass)]
#[class(base=ColorRect)]
pub struct ScreenShutter
{
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
            pre_texture: Option::None,

            base
        }
    }

}

#[godot_api]
impl ScreenShutter
{
    #[func]
    fn _shutter(&mut self) -> Gd<ImageTexture>
    {
        let mut img = Image::new_gd();
        let mut img_texture = ImageTexture::new_gd();

        img = self.base_mut().get_viewport().unwrap().get_texture().unwrap().get_image().unwrap();
        img_texture = ImageTexture::create_from_image(&img).unwrap();

        img_texture
    }

}