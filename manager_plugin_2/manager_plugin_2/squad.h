#pragma once

#ifndef SQUAD_H
#define SQUAD_H

#include "entity.h"



#include <godot_cpp/classes/node2d.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>

using namespace godot;

class Squad : public Node2D
{

protected:
static void _bind_methods();

public:
void _ready() override;


};



#endif