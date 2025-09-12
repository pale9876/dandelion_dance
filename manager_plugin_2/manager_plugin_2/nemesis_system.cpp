#include "nemesis_system.h"

NemesisSystem* nemesis_system = nullptr;


void NemesisSystem::init_singleton()
{
    nemesis_system = memnew(NemesisSystem);
}

void NemesisSystem::uninit()
{
    memdelete(nemesis_system);
    nemesis_system = nullptr;
}

NemesisSystem& NemesisSystem::get_sys()
{
    // TODO: insert return statement here
    return *nemesis_system;
}

void NemesisSystem::add_first_name_variation(const String& region, const String &first_name)
{
    if (!first_name_variation.has(region))
    {
        first_name_variation.set(region, PackedStringArray{first_name});
    }
    else
    {
        PackedStringArray _arr = first_name_variation[region];
        _arr.push_back(first_name);
    }
}

void NemesisSystem::add_last_name_variation(const String& region, const String &last_name)
{
    if (!last_name_variation.has(region))
    {
        last_name_variation.set(region, PackedStringArray{last_name});
    }
    else
    {
        PackedStringArray _arr = last_name_variation[region];
        _arr.push_back(last_name);
    }


}

String NemesisSystem::give_random_name(String faction)
{
    TypedArray<String> fn_arr;
    TypedArray<String> ln_arr;

    fn_arr = (first_name_variation.has(faction)) ?
        first_name_variation[faction] : first_name_variation[first_name_variation.keys().pick_random()];

    ln_arr = (last_name_variation.has(faction)) ?
        last_name_variation[faction] : last_name_variation[first_name_variation.keys().pick_random()];


    String first_name = fn_arr.pick_random();
    String last_name = fn_arr.pick_random();
    
    String result = first_name + String(" ") + last_name;

    return result;
}

TypedDictionary<String, PackedStringArray> NemesisSystem::get_first_names() const
{
    return first_name_variation;
}

void NemesisSystem::set_first_names(const TypedDictionary<String, PackedStringArray>& dict)
{
    first_name_variation = dict;
}

TypedDictionary<String, PackedStringArray> NemesisSystem::get_last_names() const
{
    return last_name_variation;
}

void NemesisSystem::set_last_names(const TypedDictionary<String, PackedStringArray>& dict)
{
    last_name_variation = dict;
}

NemesisSystem* NemesisSystem::get_nemesis()
{
    return nemesis_system;
}


void NemesisSystem::_bind_methods()
{
    // static methods

    ClassDB::bind_static_method(
        "NemesisSystem",
        D_METHOD("init_singleton"),
        &NemesisSystem::init_singleton
    );

    ClassDB::bind_static_method(
        "NemesisSystem",
        D_METHOD("uninit"),
        &NemesisSystem::uninit
    );

    ClassDB::bind_static_method(
        "NemesisSystem",
        D_METHOD("get_nemesis"),
        &NemesisSystem::get_nemesis
    );


   // methods

}
