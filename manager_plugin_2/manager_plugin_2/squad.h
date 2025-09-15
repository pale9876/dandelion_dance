#pragma once

#ifndef SQUAD_H
#define SQUAD_H

#include "entity.h"

#include <godot_cpp/classes/node2d.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/classes/wrapped.hpp>

using namespace godot;

class Squad : public Node2D
{
    GDCLASS(Squad, Node2D)

    public:
    void _ready() override;


    protected:
    static void _bind_methods();



    };



#endif