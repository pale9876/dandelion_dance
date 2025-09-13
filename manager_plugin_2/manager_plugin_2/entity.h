#pragma once

#ifndef ENTITY_H
#define ENTITY_H

#include "nemesis_system.h"

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
    // SETGET
    int get_id() const;
    void set_id(const int &id);

    String get_full_name() const;
    void set_full_name(const String &name);

    bool is_unique() const;
    void set_unique(const bool &toggle);

    protected:
    static void _bind_methods();

    private:
    int id = -1;
    String full_name = "";
    bool unique = false;
};

#endif