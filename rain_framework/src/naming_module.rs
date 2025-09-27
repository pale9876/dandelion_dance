use godot::classes::file_access::ModeFlags;
use godot::global::Error;
use godot::prelude::*;
use godot::classes::{Node, INode, Engine, Json, FileAccess};

use crate::nemesis_system::NemesisSystem;


#[derive(GodotClass)]
#[class(init, base=Node)]
pub struct NamingModule
{
    name_data: Dictionary, // JSON
    using: Dictionary, 
    base: Base<Node>
}

#[godot_api]
impl INode for NamingModule {
    
}

#[godot_api]
impl NamingModule
{
    #[func]
    fn get_default_first_names(faction: GString) -> Array<GString>
    {
        let nemesis_system: Gd<NemesisSystem> = NamingModule::get_nemesis();
    
        let mut dict = Dictionary::new();
    
        let koreans: Array<GString> = array![
            "Haneul",
            "Yuri",
            "Rinae",
            "Eunju",
            "Hani",
            "Jinseon",
            "Semi",
            "Muna",
            "Mirae",
            "Yuna",
            "Miro",
            "Nagi"
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
            "Yukiteru",
            "Tsubaki",
            "Minene",
            "Aru",
            "Nagi",
            "Tsumugi"
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
            "Ahsin",
            "Marbel"
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
    
        result
    
    }

    #[func]
    fn get_default_last_names(faction: GString) -> Array<GString>
    {
        let mut dict: Dictionary = Dictionary::new();
        let mut result: Array<GString> = Array::new();

        let koreans: Array<GString> = array![
            "Yuu",
            "Gang",
            "Kim",
            "Gwon",
            "Lee",
            "Jang",
        ];

        let japanese: Array<GString> = array![
            "Hasegawa",
            "Gasai",
            "Amano",
            "Kasugano",
            "Hirasaka",
            "Uryuu",
            "Tatsumaki",
            "Hojou",
            "Satou",
            "Akise",
        ];

        dict.set("korean", koreans);
        dict.set("japanese", japanese);

        if !faction.is_empty()
        {
            if dict.contains_key(faction.clone())
            {
                let _arr = dict.at(faction).to::<Array<GString>>();
                result.extend_array(&_arr);
            }
            else
            {
                godot_error!("{} isn't in default last name variants.", faction);
            }
        }
        else
        {
            godot_error!("{} must has some value.", faction);
        }

        result
    }
    
    #[func]
    fn rand_default_first_name(&mut self, faction: GString) -> GString
    {
        let r_name = NamingModule::get_default_first_names(faction)
            .pick_random()
            .unwrap_or(GString::default());
        
        r_name
    }

    #[func]
    fn rand_default_last_name(&mut self, faction: GString) -> GString
    {
        let r_name = NamingModule::get_default_last_names(faction)
            .pick_random()
            .unwrap_or(GString::default());

        r_name
    }

    // static method

    #[func]
    fn get_nemesis() -> Gd<NemesisSystem>
    {
        let engine = Engine::singleton();
        
        let nemesis_c_name = &NemesisSystem::class_name().to_string_name();
        
        let result = engine
            .get_singleton(nemesis_c_name)
            .unwrap()
            .cast::<NemesisSystem>();

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

    #[func]
    fn get_rand_name(has_l_name: bool, faction: GString) -> GString
    {
        let mut result: String = String::default();
        let mut f_name: String;
        let mut l_name: String = String::default();

        if has_l_name
        {
            let l_name_gstr = NamingModule::get_default_last_names(faction.clone())
                .pick_random()
                .unwrap_or(GString::default());
            l_name = String::from(&l_name_gstr);
        }
        
        let f_name_gstr = NamingModule::get_default_first_names(faction.clone())
            .pick_random()
            .unwrap_or(GString::default());

        f_name = String::from(&f_name_gstr);

        result = l_name.clone() + " ".into() + &f_name.clone();

        let result_g = GString::from(result);

        result_g

    }

}




