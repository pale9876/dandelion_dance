use godot::prelude::*;
use godot::classes::{Node2D, INode2D};
use crate::entity::Entity;

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct Squad
{
    #[export]
    entities: Dictionary,

    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Squad
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            //props
            entities: Dictionary::new(),
            //
            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.signals()
            .child_entered_tree()
            .connect_self(Self::on_child_entered);

        self.signals()
            .child_exiting_tree()
            .connect_self(Self::on_child_exited);
    }

    fn ready(&mut self)
    {
        self._update();
    }
}

#[godot_api]
impl Squad
{
    fn _update(&mut self)
    {

    }

    fn on_child_entered(&mut self, node: Gd<Node>)
    {
        let entity = node.try_cast::<Entity>();

        match entity
        {
            Ok(entity) => {
                let path = self.base().get_path_to(&entity);
                self.entities.set(entity.get_name(), path);
            },
            Err(_) => {}
        }
    }

    fn on_child_exited(&mut self, node: Gd<Node>)
    {

    }
}