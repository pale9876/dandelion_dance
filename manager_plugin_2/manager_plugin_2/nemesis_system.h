#pragma once

#ifndef NEMESIS_SYSTEM_H
#define NEMESIS_SYSTEM_H

#include <godot_cpp/classes/node.hpp>

using namespace godot;

class NemesisSystem : public godot::Node
{
    GDCLASS(NemesisSystem, Node);

    protected:
    static void bind_methods();




};


#endif