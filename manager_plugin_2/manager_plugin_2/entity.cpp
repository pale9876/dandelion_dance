#include "entity.h"


Entity::Entity()
{
}

Entity::~Entity()
{
}

int Entity::get_id() const
{
    return this->id;
}

void Entity::set_id(const int &id)
{
    this->id = id;
}

String Entity::get_full_name() const
{
    return full_name;
}

void Entity::set_full_name(const String name)
{
    this->full_name = name;
}

bool Entity::is_unique() const
{
    return this->unique;
}

void Entity::set_unique(const bool toggle)
{
    this->unique = toggle;
}

void Entity::event_received(uint64_t event, Node* from)
{
    Entity* ett = Object::cast_to<Entity>(from);

    ERR_FAIL_COND_MSG(!ett, "there is no 'from' entity in this tree.");




}

void Entity::_enter_tree()
{
    NemesisSystem* nemesis = NemesisSystem::get_nemesis();
    
    nemesis -> entity_entered(
        Object::cast_to<Entity>(this)
    );
}

void Entity::_exit_tree()
{
    NemesisSystem* nemesis = NemesisSystem::get_nemesis();
    
    nemesis -> delete_entity(this->id);

}


void Entity::_bind_methods()
{
    // id
    ClassDB::bind_method(
        D_METHOD("get_id"),
        &Entity::get_id
    );

    ClassDB::bind_method(
        D_METHOD("set_id", "value"),
        &Entity::set_id
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::INT, "id", PROPERTY_HINT_NONE, "", PROPERTY_USAGE_READ_ONLY | PROPERTY_USAGE_DEFAULT),
        "set_id",
        "get_id"
    );

    // full_name
    ClassDB::bind_method(
        D_METHOD("get_full_name"),
        &Entity::get_full_name
    );

    ClassDB::bind_method(
        D_METHOD("set_full_name", "name"),
        &Entity::set_full_name
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::STRING, "full_name"),
        "set_full_name",
        "get_full_name"
    );

    // unique
    ClassDB::bind_method(
        D_METHOD("is_unique"),
        &Entity::is_unique
    );

    ClassDB::bind_method(
        D_METHOD("set_unique", "toggle"),
        &Entity::set_unique
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::BOOL, "unique", PROPERTY_HINT_NONE, "", PROPERTY_USAGE_DEFAULT),
        "set_unique",
        "is_unique"
    );

}

