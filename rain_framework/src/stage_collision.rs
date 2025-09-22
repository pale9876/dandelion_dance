use godot::classes::collision_polygon_2d::BuildMode;
use godot::prelude::*;
use godot::classes::{CollisionPolygon2D, ICollisionPolygon2D};


#[derive(GodotClass)]
#[class(tool, base=CollisionPolygon2D)]
pub struct StageCollision
{
    base: Base<CollisionPolygon2D>
}

#[godot_api]
impl ICollisionPolygon2D for StageCollision
{
    fn init(base: Base<CollisionPolygon2D>) -> Self
    {
        Self {
            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.base_mut().set_build_mode(BuildMode::SEGMENTS);
    }

}