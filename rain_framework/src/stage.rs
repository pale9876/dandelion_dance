use godot::prelude::*;
use godot::classes::{Engine, INode2D, Node2D, StaticBody2D};

use crate::stage;
use crate::stage_collision::StageCollision;

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct Stage
{
    //props
    #[var(get, set=_change_abs_region,
        hint=NONE,
        usage_flags=[EDITOR]
    )]
    abs_region: Vector2,

    #[var(
        get,set,
        hint=NONE,
        usage_flags=[EDITOR]
    )]
    niobi_region: Vector2,

    #[export]
    sectors: Dictionary,

    #[var(get, set,
        usage_flags = [EDITOR]
    )]
    index: i64,

    #[export]
    stage_rect: Option<Gd<StaticBody2D>>,

    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Stage
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            abs_region: Vector2::ZERO,
            niobi_region: Vector2::ZERO,
            stage_rect: Option::None,
            sectors: Dictionary::new(),
            index: -1,

            base
        }
    }

    fn enter_tree(&mut self)
    {
        self.signals().child_entered_tree().connect_self(
            Self::on_child_entered
        );

        self.signals().child_exiting_tree().connect_self(
            Self::on_child_exited
        );
    }

    fn ready(&mut self)
    {
        if Engine::singleton().is_editor_hint() { return; }

        self.sb_init();
    }
}

#[godot_api]
impl Stage
{
    #[func]
    fn _change_abs_region(&mut self, value: Vector2)
    {
        self.abs_region.set_property(value);

        if self.base().is_node_ready() && self.stage_rect != Option::None
        {
            let sb = self.get_stage_rect().unwrap();
            if let Some(stage_collision) = sb.try_get_node_as::<StageCollision>("StageCollision")
            {
                let mut mut_colli = stage_collision;
                let mut poly: PackedVector2Array = PackedVector2Array::new();

                poly.push(self.base().get_global_position()); // [0]
                poly.push(Vector2{x: value.x, y: 0.}); // [1]
                poly.push(value); // [2]
                poly.push(Vector2 {x: 0., y: value.y}); // [3]

                mut_colli.set_polygon(&poly);
            }
        }
    }

    #[func]
    fn on_child_entered(&mut self, node: Gd<Node>)
    {
        let opt_casted = node.try_cast::<StaticBody2D>();
        match opt_casted
        {
            Ok(opt_casted) => {
                if self.stage_rect == Option::None
                {
                    self.stage_rect.set_property(Some(opt_casted));
                }
            },
            Err(_) => ()
        }
    }

    #[func]
    fn on_child_exited(&mut self, node: Gd<Node>)
    {
        let opt_casted = node.try_cast::<StaticBody2D>();
        match opt_casted
        {
            Ok(opt_casted) => {
                if Some(opt_casted) == self.stage_rect.get_property()
                {
                    self.stage_rect.set_property(Option::None);
                }
            },
            Err(_) => ()
        }
    }

    #[func]
    fn sb_init(&mut self)
    {
        let sb_polygon = self.stage_rect.clone().unwrap().try_get_node_as::<StageCollision>("StageCollision");
        
        if let Some(polygon) = sb_polygon
        {
            self.abs_region = Vector2 {
                x: polygon.get_polygon()[2].x,
                y: polygon.get_polygon()[2].y
            };

            godot_print!("{} => abs_region => {}", self.base().get_name(), self.abs_region);
        }
        else
        {
            let error_point = &self.stage_rect;
            godot_error!("{:?}: There is no polygon in this Stage", error_point);
        }
    }

}