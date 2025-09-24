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
    #[var(
        set=set_eid, get=get_eid,
        usage_flags=[EDITOR, READ_ONLY]
    )]
    eid: i64,
    #[var(usage_flags=[EDITOR])] unique: bool,
    #[var()] first_name: GString,
    #[var()] last_name: GString,
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

#[godot_api]
impl ICharacterBody2D for Entity
{
    fn init(base:Base<CharacterBody2D>) -> Self
    {
        Self{
            eid: 0,
            unique: false,
            first_name: GString::default(),
            last_name: GString::default(),
            e_name: GString::default(),
            is_grabbed: false,
            grabbed_by: Option::None,

            base,
        }

    }

    fn enter_tree(&mut self)
    {
        // set eid
        let mut nemesis_system = Entity::get_nemesis();
        let self_refer = self.to_gd();
        nemesis_system.bind_mut().entity_entered(self_refer);
        self.set_eid(nemesis_system.bind().e_index);

        // self.set_random_fist_name(GString::default());

    }

    fn exit_tree(&mut self)
    {
        let mut nemesis_system: Gd<NemesisSystem> = Entity::get_nemesis();
        nemesis_system.bind_mut().entity_exited(self.eid);
    }
}


#[godot_api]
impl Entity
{
    #[func]
    pub fn get_eid(&self) -> i64
    {
        self.eid
    }

    #[func]
    pub fn set_eid(&mut self, value: i64)
    {
        self.eid = value;
    }

    #[func]
    fn set_random_first_name(&mut self, faction: GString)
    {
        let r_name = Entity::get_default_first_names(faction)
                                .pick_random()
                                .unwrap_or(GString::default());
        self.set_e_name(r_name.clone());
        // Entity::get_nemesis().bind_mut().used_names.set()
    }

    #[func]
    fn get_default_first_names(faction: GString) -> Array<GString>
    {
        let nemesis_system: Gd<NemesisSystem> = Entity::get_nemesis();

        let mut dict = Dictionary::new();

        let koreans: Array<GString> = array![
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
            "Akira",
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

        dict.set(GString::from("korean"), koreans);
        dict.set(GString::from("japanese"), japanese);
        dict.set(GString::from("chinese"), chinese);
        dict.set(GString::from("etc"), etcs);

        let mut result: Array<GString>= Array::new();

        if !faction.is_empty()
        {
            if dict.contains_key(faction.clone())
            {
                let _arr = dict.at(faction).to::<Array<GString>>();
                result.extend_array(&_arr);
            }
        }
        else
        {
            let etcs = dict.at("etc").to::<Array<GString>>();
            result.extend_array(&etcs);
        }

        let used = nemesis_system.bind().get_used_names();

        for name in used.iter_shared()
        {
            if result.contains(&name)
            {
                result.erase(&name);
            }
        }

        result

    }

    #[func]
    fn get_data_from_json(path: GString, faction: GString) -> Dictionary
    {
        let mut result: Variant = Variant::nil();
        let mut dict = Dictionary::new();
        let mut json = Json::new_gd();

        if let Some(file) = FileAccess::open(&path, ModeFlags::READ)
        {
            let data = file.get_as_text();
            let parsed = json.parse(&data);
            if parsed == Error::OK
            {
                result = json.get_data();
            }
        }

        dict = result.to::<Dictionary>();

        dict
    }
    
    // #[func]
    // fn get_last_names_from_json(path: GString, faction: GString)
    // {

    // }

    #[func]
    fn get_nemesis() -> Gd<NemesisSystem>
    {
        let engine = Engine::singleton();
        let nemesis_c_name = &NemesisSystem::class_name().to_string_name();
        let result = (engine.get_singleton(nemesis_c_name).unwrap()).cast::<NemesisSystem>();

        result
    }
}