#pragma once

#ifndef BT_CHECK_DISTANCE
#define BT_CHECK_DISTANCE

#include "bt/tasks/bt_action.h"
#include "godot_cpp/classes/node2d.hpp"

class BTCheckDistance : public BTAction
{
    GDCLASS(BTCheckDistance, BTAction);
    TASK_CATEGORY(Custom);
    
    public:
    String _generate_name() override;
    BT::Status _tick(double delta) override;

    protected:
    static void _bind_methods();

    private:
    Node2D* target = nullptr;
    float tolorance = 0.0;


    // setget
    void set_tolorance(const float &value);
    float get_tolorance() const;

    // methods
    void set_target(Node2D* target);
    Node2D* get_target() const;
    bool is_in_range();
};


#endif // !BT_CHECK_DISTANCE
