use godot::classes::CollisionShape2D;
use godot::prelude::*;
use godot::global::*;
use godot::classes::{Area2D, IArea2D};

#[derive(GodotClass)]
#[class(tool, base=Area2D)]
pub struct Hitbox
{
    #[var(
        get,
        set=set_hit_grade,
        hint=ENUM,
        hint_string="One, Two, Three",
        usage_flags=[EDITOR]
    )] hit_grade: i64,
    #[export] collisions: Dictionary,

    base: Base<Area2D>
}

#[godot_api]
impl IArea2D for Hitbox
{
    fn init(base: Base<Area2D>) -> Self
    {
        Self {
            hit_grade : 1,
            collisions : Dictionary::new(),
            base
        }
    }

    fn enter_tree(&mut self)
    {
        
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

    fn exit_tree(&mut self)
    {

    }

}

#[godot_api]
impl Hitbox
{
    #[func]
    fn set_hit_grade(&mut self, value: i64)
    {
        let clamped = clampi(value, 1, 3);
        self.hit_grade = clamped;
        self.hit_grade.set_property(clamped);
    }

    #[func]
    fn get_areas(&mut self) -> Array<Gd<CollisionShape2D>>
    {
        let mut result:Array<Gd<CollisionShape2D>> = Array::new();

        for node in self.base().get_children().iter_shared()
        {
            let collision = node.try_cast::<CollisionShape2D>();
            match collision
            {
                Ok(collision) => { result.push(&collision) },
                Err(_) => {}
            }
        }

        result
    }

    // static methods
    // #[func]
    // fn print_hello()
    // {
    //     godot_print!("hello");
    // }

}