#pragma once

#ifndef BT_AIM_AT_H
#define BT_AIM_AT_H


//bt
#include "bt/tasks/bt_action.h"

//manager_plugin
#include "../../manager_plugin_2/manager_plugin_2/nemesis_system.h"

#include <godot_cpp/classes/node2d.hpp>

class BTAimAt : public BTAction
{
    GDCLASS(BTAimAt, BTAction);
    TASK_CATEGORY(Custom);

    public:
    void _enter() override;
    BT::Status _tick(double delta) override;

    protected:
    static void _bind_methods();

    private:
    Node* get_target();
    Node2D* get_nearest();
    Node2D* get_farthest();
};

#endif