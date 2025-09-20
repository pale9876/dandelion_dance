use godot::prelude::*;
use godot::classes::{Area2D, CollisionShape2D, IArea2D};

#[derive(GodotClass)]
#[class(tool, base=Area2D)]
pub struct Hurtbox
{
    #[export] collisions: Dictionary,
    base: Base<Area2D>
}

#[godot_api]
impl IArea2D for Hurtbox
{
    fn init(base: Base<Area2D>) -> Self
    {
        Self {
            collisions: Dictionary::new(),
            base
        }
    }

    fn ready(&mut self)
    {
        for collision in self.get_areas().iter_shared()
        {
            self.collisions.set(
                collision.get_name(),
                self.base().get_path_to(&collision)
            );
        }
    }

}


#[godot_api]
impl Hurtbox
{
    fn get_areas(&mut self) -> Array<Gd<CollisionShape2D>>
    {
        let mut result: Array<Gd<CollisionShape2D>>= Array::new();

        for node in self.base().get_children().iter_shared()
        {
            let collision = node.try_cast::<CollisionShape2D>();
            match collision
            {
                Ok(collision) => {
                    result.push(&collision);
                },
                Err(_) => {}
            }
        }

        result
    }

}