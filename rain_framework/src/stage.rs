use godot::prelude::*;
use godot::classes::{CollisionPolygon2D, INode2D, Node2D, StaticBody2D};


#[derive(GodotClass)]
#[class(base=Node2D)]
pub struct Stage
{
    //props
    #[var]
    region: Vector2,
    #[export]
    sb: OnEditor<Gd<StaticBody2D>>,

    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Stage
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            region: Vector2::ZERO,
            sb: OnEditor::default(),

            base
        }
    }
}

#[godot_api]
impl Stage
{
    #[func]
    fn sb_init(&mut self)
    {
        let sb_polygon = self.sb.get_node_as::<CollisionPolygon2D>("CollisionPolygon2D");
    }

}