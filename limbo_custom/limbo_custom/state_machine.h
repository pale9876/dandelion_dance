#pragma once

#ifndef STATE_MACHINE_H
#define STATE_MACHINE_H

#include "hsm/limbo_hsm.h"
#include "hsm/limbo_state.h"

#include <godot_cpp/variant/typed_dictionary.hpp>


class StateMachine : public LimboHSM
{
    
    protected:
    static void _bind_methods();

    public:
    const StringName IDLE_STATE = "Idle";
    const StringName MOVE_STATE = "Move";
    const StringName JUMP_STATE = "Jump";
    const StringName DRAG_STATE = "Drag";
    const StringName DASH_STATE = "Dash";
    const StringName FALL_STATE = "Fall";
    
    TypedDictionary<StringName, Node> states = {};
    TypedDictionary<StringName, Node> individual_acts = {};

    void _ready() override;

    bool add_state(Node* state);
    Node* get_state(StringName state_name);

};

#endif