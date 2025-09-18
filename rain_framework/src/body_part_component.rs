use godot::prelude::*;
use godot::classes::{INode2D, Node2D, RigidBody2D};

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct BodyPartComponent
{
    head: OnEditor<Gd<PackedScene>>,
    body: OnEditor<Gd<PackedScene>>,
    left_leg: OnEditor<Gd<PackedScene>>,
    left_arm: OnEditor<Gd<PackedScene>>,
    right_leg: OnEditor<Gd<PackedScene>>,
    right_arm: OnEditor<Gd<PackedScene>>,
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for BodyPartComponent
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            head: OnEditor::default(),
            body: OnEditor::default(),
            left_leg: OnEditor::default(),
            left_arm: OnEditor::default(),
            right_leg: OnEditor::default(),
            right_arm: OnEditor::default(),
            base
        }
    }

}

#[godot_api]
impl BodyPartComponent
{
    #[func]
    fn create_parts(&mut self)
    {
        if self.head.can_instantiate()
        {
            let head_part = self.head.instantiate_as::<RigidBody2D>();
            self.base_mut().add_child(&head_part);
        }
    }
}