use godot::global::Error;
use godot::prelude::*;
use godot::classes::{ResourceLoader, ResourceSaver};

#[derive(GodotClass)]
#[class(init, base=Node)]
pub struct SaveLoadManager
{
    data_path: GString,
    data_dict: Dictionary,
    player_data: Option<Gd<PlayerData>>,

    base: Base<Node>
}

#[derive(GodotClass)]
#[class(init, base=Resource)]
pub struct PlayerData
{
    pos: Vector2,
    
    base: Base<Resource>
}

#[godot_api]
impl SaveLoadManager
{
    fn new_data(&mut self)
    {
        self.player_data = Some(PlayerData::create_data());
    }

    fn save(&mut self) -> bool
    {
        let mut r_saver = ResourceSaver::singleton();
        let data = self.player_data.as_ref().unwrap();
        let err = r_saver.save_ex(data).path("res://").done();

        if err != Error::OK { return false; }

        true
    }

    fn load(&mut self, path: GString) -> bool
    {
        let mut r_loader = ResourceLoader::singleton();

        if let Some(data) = r_loader.load(&path)
        {
            self.player_data = Some(data.cast::<PlayerData>());
            return true;
        }

        false
    }
}

#[godot_api]
impl PlayerData
{
    #[func]
    fn create_data() -> Gd<PlayerData>
    {
        Gd::from_init_fn(|base|
            Self {
                pos: Vector2::ZERO,

                base
            }
        )
    }
}