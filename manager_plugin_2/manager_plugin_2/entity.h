#pragma once

#ifndef ENTITY_H
#define ENTITY_H

#include <godot_cpp/classes/node.hpp>
#include <godot_cpp/classes/wrapped.hpp>

using namespace godot;

class Entity : public Node
{
    GDCLASS(Entity, Node)

    public:

    protected:
    static void _bind_methods();

    private:


};

#endif