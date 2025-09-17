#pragma once

#ifndef STATE_MACHINE_H
#define STATE_MACHINE_H

#include "hsm/limbo_hsm.h"

#include <godot_cpp/variant/typed_dictionary.hpp>

class StateMachine : public LimboHSM
{

    protected:
    static void _bind_methods();

    public:
    void _ready() override;

    TypedDictionary<StringName, Node> states = {};
    TypedDictionary<StringName, Node> individuality_states = {};

    private:
    

};

#endif