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

void NemesisSystem::add_first_name_variation(const String& region, const StringName& first_name)
{
    if (!first_name_variation.has(region))
    {
        first_name_variation.set(region, TypedArray<StringName>{first_name});
    }
    else
    {
        PackedStringArray _arr = first_name_variation[region];
        _arr.push_back(first_name);
    }
}

void NemesisSystem::add_last_name_variation(const String& region, const StringName& last_name)
{



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


void NemesisSystem::_bind_methods()
{
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

    

}
