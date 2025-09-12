#include "entity.h"


void Entity::add_first_name_variation(const String &region, const StringName &first_name)
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

void Entity::add_last_name_variation(const String& region, const StringName& last_name)
{



}

TypedDictionary<String, PackedStringArray> Entity::get_first_names() const
{
    return first_name_variation;
}

void Entity::set_first_names(const TypedDictionary<String, PackedStringArray>& dict)
{
    first_name_variation = dict;
}

TypedDictionary<String, PackedStringArray> Entity::get_last_names() const
{
    return last_name_variation;
}

void Entity::set_last_names(const TypedDictionary<String, PackedStringArray>& dict)
{
    last_name_variation = dict;
}


void Entity::_bind_methods()
{
    ClassDB::bind_method(
        D_METHOD("add_first_name", "first_name"),
        &Entity::add_first_name_variation
        
        );
}