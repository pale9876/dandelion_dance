#pragma once

#ifndef AUTO_SPRITE_COMPONENT_H
#define AUTO_SPRITE_COMPONENT_H

#include "auto_sprite.h"

#include <godot_cpp/classes/node2d.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/classes/wrapped.hpp>

using namespace godot;

class AutoSpriteComponent: public Node2D
{
    GDCLASS(AutoSpriteComponent, Node2D)

    public:
    void _ready() override;

    protected:
    static void _bind_methods();

    private:
    Callable _update();
    void update();
    int index = -1;
    StringName current_sprite = "";
    TypedDictionary<StringName, Node> sprites = {};
    
    TypedDictionary<StringName, Node> get_sprites() const;

    void set_index(const int idx);
    int get_index() const;

    void set_current_sprite(const StringName sprite_name);
    StringName get_current_sprite() const;

};

#endif