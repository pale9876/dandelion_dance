use godot::prelude::*;
use godot::classes::{CollisionPolygon2D, ICollisionPolygon2D};

#[derive(GodotClass)]
#[class(tool, base=CollisionPolygon2D)]
pub struct SectorCollision{
    base : Base<CollisionPolygon2D>
}

#[godot_api]
impl ICollisionPolygon2D for SectorCollision
{
    fn init(base: Base<CollisionPolygon2D>) -> Self
    {
        Self
        {
            base
        }
    }
}

#[godot_api]
impl SectorCollision
{
    #[func]
    pub fn create_sector(&mut self, region: Vector2, margin: Vector2)
    {
        let rect = PackedVector2Array::from(
            [
                - margin,
                Vector2 { x: margin.x + region.x, y: - margin.y },
                margin + region,
                Vector2 { x: - margin.x, y: region.y + margin.y }
            ]
        );

        self.base_mut().set_polygon(&rect);
        
    }
}

                    // let mut new_poly = PackedVector2Array::new();
                    // new_poly.resize(4);
    
                    // let entry_point = self.get_entry_margin();
                    // let region = self.get_region();

                    // new_poly[0] = - entry_point; // [0]
                    // new_poly[1] = Vector2 { x: entry_point.x + region.x, y: - entry_point.y }; // [1]
                    // new_poly[2] = entry_point + region; // [2]
                    // new_poly[3] = Vector2 { x: - entry_point.x, y: region.y + entry_point.y }; // [3]
    
                    // collision.set_polygon(&new_poly);