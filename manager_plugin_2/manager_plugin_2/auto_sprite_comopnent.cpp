#include "auto_sprite_component.h"


void AutoSpriteComponent::_ready()
{
    if (0 < this -> get_child_count()) update();

    if (!sprites.is_empty())
    {
        index = 0;
        current_sprite = sprites[sprites.keys()[0]];
    }
}

void AutoSpriteComponent::update()
{
    TypedArray<Node> _childs = this -> get_children();

    for (Variant node: _childs)
    {
        AutoSprite* sprite = Object::cast_to<AutoSprite>(node);

        if (sprite)
        {
            sprites.set(sprite -> get_name(), node);
        }
    }
}

void AutoSpriteComponent::set_sprites(const TypedDictionary<StringName, Node> dict)
{
    this->sprites = dict;
}

TypedDictionary<StringName, Node> AutoSpriteComponent::get_sprites() const
{
    return sprites;
}

void AutoSpriteComponent::set_index(const int idx)
{
    this -> index = idx;
}

int AutoSpriteComponent::get_index() const
{
    return this -> index;
}

void AutoSpriteComponent::set_current_sprite(const StringName sprite_name)
{
    this -> current_sprite = sprite_name;

}

StringName AutoSpriteComponent::get_current_sprite() const
{
    return this -> current_sprite;
}

Callable AutoSpriteComponent::_update()
{
    return callable_mp(
        this, &AutoSpriteComponent::update
    );
}

void AutoSpriteComponent::_bind_methods()
{
    // update tool btn
    ClassDB::bind_method(
        D_METHOD("update"),
        &AutoSpriteComponent::_update
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::CALLABLE, "", PROPERTY_HINT_TOOL_BUTTON, "Update", PROPERTY_USAGE_EDITOR),
        "",
        "update"
    );

    // index
    ClassDB::bind_method(
        D_METHOD("set_index", "idx"),
        &AutoSpriteComponent::set_index
    );

    ClassDB::bind_method(
        D_METHOD("get_index"),
        &AutoSpriteComponent::get_index
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::INT, "index"),
        "set_index",
        "get_index"
    );

    // current_sprite (READ ONLY)


    // sprites (READ ONLY)



    // methods
}
