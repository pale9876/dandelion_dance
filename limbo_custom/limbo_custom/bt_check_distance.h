#pragma once

#ifndef BT_CHECK_DISTANCE
#define BT_CHECK_DISTANCE

#include "bt/tasks/bt_action.h"
#include "godot_cpp/classes/node2d.hpp"

// manager_plugin
#include "../../manager_plugin_2/manager_plugin_2/entity.h"
#include "../../manager_plugin_2/manager_plugin_2/nemesis_system.h"

class BTCheckDistance : public BTAction
{
    GDCLASS(BTCheckDistance, BTAction);
    TASK_CATEGORY(Custom);
    
    public:

    enum Targeting{
        NEAR,
        FAR,
    };

    String _generate_name() override;
    void _enter() override;
    BT::Status _tick(double delta) override;

    protected:
    static void _bind_methods();

    private:
    Node2D* target = nullptr;
    float tolorance = 0.0;
    float coyote_time = 0.5;

    // setget
    void set_tolorance(const float value);
    float get_tolorance() const;

    float get_coyote_time() const;
    void set_coyote_time(const float value);

    // methods
    //void set_target(Node2D* target);
    Node2D* get_target() const;
    bool is_in_range();
};


#endif // !BT_CHECK_DISTANCE
