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


    protected:
    static void _bind_methods();


    private:

    String full_name;

};

#endif