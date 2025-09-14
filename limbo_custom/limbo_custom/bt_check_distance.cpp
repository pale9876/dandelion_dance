#include "bt_check_distance.h"

String BTCheckDistance::_generate_name()
{
    return vformat("Check Distance range: %s", tolorance);
}

BT::Status BTCheckDistance::_tick(double delta)
{
    if (target)
    {
        return SUCCESS;
    }
    else
    {
        return FAILURE;
    }

    return RUNNING;
}

void BTCheckDistance::set_tolorance(const float value)
{
    this -> tolorance = value;
    this -> set_custom_name(vformat("Check Distance range: %s", tolorance));
}

float BTCheckDistance::get_tolorance() const
{
    return tolorance;
}

void BTCheckDistance::set_target(Node2D* target)
{
    this -> target = target;
}

Node2D* BTCheckDistance::get_target() const
{
    return target;
}

bool BTCheckDistance::is_in_range()
{
    if (target)
    {
        Node2D* agent = Object::cast_to<Node2D>(get_agent());
        if (agent)
        {
            Vector2 pos = (agent->get_global_position());
            float dist = pos.distance_squared_to(target -> get_global_position());
            return dist < godot::Math::pow(double(tolorance), 2.0);
        }
    }

    return false;
}

float BTCheckDistance::get_coyote_time() const
{
    return coyote_time;
}

void BTCheckDistance::set_coyote_time(const float value)
{
    coyote_time = value;
}

void BTCheckDistance::_bind_methods()
{

    //tolorance
    ClassDB::bind_method(
        D_METHOD("set_tolorance", "value"),
        &BTCheckDistance::set_tolorance
    );

    ClassDB::bind_method(
        D_METHOD("get_tolorance"),
        &BTCheckDistance::get_tolorance
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::FLOAT, "tolorance"),
        "set_tolorance",
        "get_tolorance"
    );

    // coyote_time
    ClassDB::bind_method(
        D_METHOD("set_coyote_time", "value"),
        &BTCheckDistance::set_coyote_time
    );

    ClassDB::bind_method(
        D_METHOD("get_coyote_time"),
        &BTCheckDistance::get_coyote_time
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::FLOAT, "coyote_time"),
        "set_coyote_time",
        "get_coyote_time"
    );


    // methods
    ClassDB::bind_method(
        D_METHOD("set_target", "target"),
        &BTCheckDistance::set_target
    );

    ClassDB::bind_method(
        D_METHOD("is_in_range"),
        &BTCheckDistance::is_in_range
    );

    ClassDB::bind_method(
        D_METHOD("get_target"),
        &BTCheckDistance::get_target
    );
}