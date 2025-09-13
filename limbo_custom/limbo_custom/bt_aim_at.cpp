#include "bt_aim_at.h"

Node* BTAimAt::get_target()
{
    return Object::cast_to<Node>(this->get_blackboard()->get_var("target"));
}

Node2D* BTAimAt::get_nearest()
{
    return nullptr;
}

Node2D* BTAimAt::get_farthest()
{
    return nullptr;
}

void BTAimAt::_enter()
{
    if (get_target())
    {
        this->get_blackboard()->set_var(
            StringName("target"), get_target()
        );
    }

    //Blackboard
}

BT::Status BTAimAt::_tick(double delta)
{
    return BT::Status();
}

void BTAimAt::_bind_methods()
{

}