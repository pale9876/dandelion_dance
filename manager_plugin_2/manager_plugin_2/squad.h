#pragma once

#ifndef SQUAD_H
#define SQUAD_H

#include "entity.h"

#include <godot_cpp/classes/node.hpp>
#include <godot_cpp/classes/node2d.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/classes/wrapped.hpp>
#include <godot_cpp/variant/typed_array.hpp>

using namespace godot;

class Squad : public Node2D
{
    GDCLASS(Squad, Node2D)

    public:
    void _ready() override;

    protected:
    static void _bind_methods();

    private:
    TypedArray<Node> entities = {};
    Entity* squad_leader = nullptr;

    void appoint_leader(Node* entity);
    Node* get_leader();
};



#endif