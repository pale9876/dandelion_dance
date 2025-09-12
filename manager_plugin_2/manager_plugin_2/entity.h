#pragma once

#ifndef ENTITY_H
#define ENTITY_H

#include <godot_cpp/classes/node.hpp>
#include <godot_cpp/classes/wrapped.hpp>
#include <godot_cpp/classes/character_body2d.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/variant/typed_array.hpp>

using namespace godot;

class Entity : public CharacterBody2D
{
    GDCLASS(Entity, CharacterBody2D)

    public:
    TypedDictionary<String, PackedStringArray>
        first_name_variation = {};

    TypedDictionary<String, PackedStringArray>
        last_name_variation = {};


    protected:
    static void _bind_methods();


    private:
    void add_first_name_variation(const String &region, const StringName &first_name);
    void add_last_name_variation(const String &region, const StringName &last_name);

    TypedDictionary<String, PackedStringArray> get_first_names() const;
    void set_first_names(const TypedDictionary<String, PackedStringArray> &dict);
    TypedDictionary<String, PackedStringArray> get_last_names() const;
    void set_last_names(const TypedDictionary<String, PackedStringArray> &dict);

};

#endif