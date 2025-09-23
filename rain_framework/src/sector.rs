use godot::prelude::*;
use godot::classes::{Area2D, Engine, INode2D, Node2D};

use crate::sector_area::SectorArea;
use crate::sector_collision::SectorCollision;

#[derive(GodotClass)]
#[class(tool, base=Node2D)]
pub struct Sector
{
    #[var(
        get, set=sector_waked,
        usage_flags=[EDITOR]
    )]
    wake: bool,

    #[var(
        get,set=region_value_changed,
        hint=NONE,
        usage_flags=[EDITOR]
    )]
    region: Vector2,
    
    #[var(
        get,set,
        hint=NONE,
        usage_flags=[EDITOR]
    )]
    entry_margin: Vector2,

    #[export]
    sector_area: Option<Gd<SectorArea>>,
    base: Base<Node2D>
}

#[godot_api]
impl INode2D for Sector
{
    fn init(base: Base<Node2D>) -> Self
    {
        Self {
            wake: false,
            region: Vector2::ZERO,
            sector_area: Option::None,
            entry_margin: Vector2 {x: 100.0, y: 50.0},

            base
        }
    }

    fn enter_tree(&mut self)
    {
        if !Engine::singleton().is_editor_hint()
        {
            if self.sector_area == Option::None
            {
                godot_error!("{} => There is no Sector area in child", self.base().get_name());
                return;
            }
        }

        self.signals()
            .child_entered_tree()
            .connect_self(Self::on_child_entered);

        self.signals()
            .child_exiting_tree()
            .connect_self(Self::on_child_exited);
    }

    fn draw(&mut self)
    {
        if Engine::singleton().is_editor_hint()
        {
            // draw region
            let rg = self.region;
            self.base_mut().draw_rect(
                Rect2 { position: (Vector2::ZERO), size: {rg} },
                Color::from_html("#cd12190d").unwrap()
            );
    
            // draw entry_margin
            let entry_p = - self.entry_margin;
            let end_margin = self.get_region() + self.entry_margin;
            self.base_mut().draw_rect(
                Rect2 { position: (entry_p), size: (end_margin) },
                Color::from_html("#cd12c10d").unwrap()
            );
        }
    }
}

#[godot_api]
impl Sector
{
    #[func]
    fn sector_waked(&mut self, toggle: bool)
    {
        self.wake.set_property(toggle);
        self.base_mut().set_visible(if toggle {true} else {false});
    }

    #[func]
    fn on_child_entered(&mut self, node: Gd<Node>)
    {
        let sa = node.try_cast::<SectorArea>();
        match sa
        {
            Ok(sa) => {
                // set sector_area prop
                self.sector_area.set_property(Some(sa.clone()));

                // set collision
                // if get collisionpolygon2d => if is not return err
                if let Some(try_getted) = sa.get_node_or_null("SectorCollision")
                {
                    let mut collision = try_getted.cast::<SectorCollision>();
                    collision.bind_mut().create_sector(self.region, self.entry_margin);
                }
                else
                {
                    godot_error!("{} => Cannot get collision in SectorArea", self.base().get_name());
                }

                //Connect Area2d Signals(body enter & exit) in Runtime
                if !Engine::singleton().is_editor_hint()
                {
                    let upcasted_sa = sa.upcast::<Area2D>();

                    upcasted_sa.signals()
                        .body_entered()
                        .connect_other(self, Self::sector_entered);

                    upcasted_sa.signals()
                        .body_exited()
                        .connect_other(self, Self::sector_exited);
                }
            },
            Err(_) => {
                godot_error!("{} => Cannot get sector area", self.base().get_name());
            }
        }

        self.base_mut().queue_redraw();
    }

    #[func]
    fn on_child_exited(&mut self, node: Gd<Node>)
    {
        let sa = node.try_cast::<SectorArea>();
        match sa
        {
            Ok(sa) => {
                if Some(sa) == self.sector_area
                {
                    self.sector_area = Option::None;   
                }
            },
            Err(_) => {}
        }

        self.base_mut().queue_redraw();
    }

    #[func]
    fn region_value_changed(&mut self, value: Vector2)
    {
        self.region.set_property(value);


    }

    #[func(virtual)]
    fn sector_entered(&mut self, _body: Gd<Node2D>)
    {
        self.wake.set_property(true);
    }

    #[func(virtual)]
    fn sector_exited(&mut self, _body: Gd<Node2D>)
    {
        self.wake.set_property(false);
    }

}