use godot::classes::file_access::ModeFlags;
use godot::global::Error;
use godot::prelude::*;
use godot::classes::{CharacterBody2D, FileAccess, ICharacterBody2D};
use godot::classes::Engine;
use godot::classes::Json;

use crate::nemesis_system::NemesisSystem;

#[derive(GodotClass)]
#[class(tool, base=CharacterBody2D)]
pub struct Entity
{
    //props
    eid: i64,
    #[var(usage_flags=[EDITOR])] unique: bool,
    #[var(usage_flags=[EDITOR])] e_name: GString,
    #[var] is_grabbed: bool,
    #[var] grabbed_by: Option<Gd<Entity>>,

    base: Base<CharacterBody2D>
}

#[derive(GodotClass)]
#[class(tool, init, base=CharacterBody2D)]
pub struct Trader
{
    base: Base<CharacterBody2D>
}

trait EID
{
    fn get_eid(&self) -> i64;
    fn set_eid(&mut self, value: i64);
}

#[godot_api]
impl ICharacterBody2D for Entity
{
    fn init(base:Base<CharacterBody2D>) -> Self
    {
        Self{
            eid: 0,
            unique: false,
            e_name: GString::from(""),
            is_grabbed: false,
            grabbed_by: Option::None,

            base,
        }
    }

    fn enter_tree(&mut self)
    {
        let mut nemesis_system = self.get_nemesis();
        nemesis_system.bind_mut().e_index += 1;
        self.set_eid(nemesis_system.bind().e_index);

        if !self.unique
        {
            let first_name = Entity::get_default_first_names().pick_random().unwrap();
            self.set_e_name(first_name);
        }
    }

    fn exit_tree(&mut self)
    {
        let mut nemesis: Gd<NemesisSystem> = self.get_nemesis();
        nemesis.bind_mut().e_index -= 1;
        self.set_eid(-1);
    }
}


#[godot_api]
impl Entity
{
    #[func]
    fn get_eid(&self) -> i64
    {
        self.eid
    }

    #[func]
    fn set_eid(&mut self, value: i64)
    {
        self.eid = value;
    }


    #[func]
    fn get_default_first_names() -> Array<GString>
    {
        let koreans = array![
            "Haneul",
            "Yuri",
            "Rinae",
            "Eunju",
            "Hani",
        ];

        let japanese: Array<GString> = array![
            "Mochi",
            "Hana",
            "Rin",
            "Ren",
            "Yuriko",
            "Yuuko",
            "Yuno",
            "Yupiteru",
        ];

        let chinese: Array<GString> = array![
            "Rhwen",
            "Ling",
            "Yun",
            "Yuulong"
        ];

        let etcs: Array<GString> = array![
            "Santos",
            "Luna",
            "Patria",
            "Khasandra",
            "Elisabeth",
            "Ahsin"

        ];

        let mut result: Array<GString>= Array::new();

        result.extend_array(&koreans);
        result.extend_array(&japanese);
        result.extend_array(&chinese);
        result.extend_array(&etcs);

        result

    }

    fn get_first_name_from_json(&mut self, path: GString) -> GString
    {
        let mut result: GString = GString::from("");
        let mut json = Json::new_gd();

        if let Some(file) = FileAccess::open(&path, ModeFlags::READ)
        {

            let data = file.get_as_text();
            let parsed = json.parse(&data);
            if parsed == Error::OK
            {
                result = json.get_data().stringify();
            }
        }

        result

        // json.set_path(path);
    }

    #[func]
    fn get_nemesis(&mut self) -> Gd<NemesisSystem>
    {
        let engine = Engine::singleton();
        let nemesis_c_name = &NemesisSystem::class_name().to_string_name();
        let result = (engine.get_singleton(nemesis_c_name).unwrap()).cast::<NemesisSystem>();

        result
    }
}