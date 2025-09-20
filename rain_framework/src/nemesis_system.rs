use godot::prelude::*;
use godot::classes::{Node, INode};

use crate::entity::Entity;

#[derive(GodotClass)]
#[class(tool, base=Node)]
pub struct NemesisSystem
{
    #[var(
        get = get_idx, set = set_idx
    )]
    pub e_index: i64,
    pub entities: Dictionary,

    base: Base<Node>
}

#[godot_api]
pub impl INode for NemesisSystem
{
    fn init(base: Base<Node>) -> Self
    {
        Self {
            e_index: -1,
            entities: Dictionary::new(),

            base
        }
    }
}

#[godot_api]
impl NemesisSystem
{
    #[func]
    fn get_entities(&mut self) -> Array<Gd<Entity>>
    {
        let mut result: Array<Gd<Entity>> = Array::new();
        for v in self.entities.values_array().iter_shared()
        {
            let entity = v.to::<Gd<Entity>>();
            result.push(&entity);
        }

        result
    }

    fn entity_entered(&mut self, entity: Gd<Entity>)
    {
        self.e_index += 1;
        self.entities.set(self.e_index, Some(entity));
    }

    fn entity_exited(&mut self, idx: i64)
    {
        self.entities.remove(idx);
    }

    #[func]
    fn get_idx(&self) -> i64
    {
        self.e_index
    }

    #[func]
    fn set_idx(&mut self, value: i64)
    {
        self.e_index = value;
    }
}