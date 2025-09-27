use godot::prelude::*;
use godot::classes::{INode2D, Node2D, PackedScene};

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct BodyPartComponent
{
    #[export] body_part_points: Dictionary,
    
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for BodyPartComponent
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            body_part_points: Dictionary::new(),
            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.signals().child_entered_tree().connect_self(Self::on_child_entered);
        self.signals().child_exiting_tree().connect_self(Self::on_child_exited);
    }
}

#[godot_api]
impl BodyPartComponent
{
    #[func]
    fn create_parts(&mut self)
    {
        for point in self.body_part_points.keys_array().iter_shared()
        {
            let mut scene = point.to::<Gd<PackedScene>>();
            if scene.can_instantiate()
            {
                let mut inst = scene.instantiate().unwrap();
                self.base_mut().add_child(&inst);
            }
        }
    }

    #[func(virtual)]
    fn on_child_entered(&mut self, node: Gd<Node>)
    {
        match node.try_cast::<Node2D>()
        {
            Ok(mark) => {
                let path = self.base().get_path_to(&mark);
                self.body_part_points.set(path, Option::<Gd<PackedScene>>::None);
            },
            Err(_) => {}
        }
    }

    #[func(virtual)]
    fn on_child_exited(&mut self, node: Gd<Node>)
    {
        match node.try_cast::<Node2D>()
        {
            Ok(mark) => {
                let path = self.base().get_path_to(&mark);

                if self.body_part_points.clone().contains_key(path.clone())
                {
                    self.body_part_points.remove(path);
                }
            },
            Err(_) => {}
        }
    }
}