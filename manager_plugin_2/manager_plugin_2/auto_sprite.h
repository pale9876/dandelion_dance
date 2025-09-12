#pragma once

#ifndef AUTO_SPRITE_H
#define AUTO_SPRITE_H

#include <godot_cpp/classes/sprite2d.hpp>

using namespace godot;


class AutoSprite : public Sprite2D
{
    GDCLASS(AutoSprite, Sprite2D)

    public:
    void _physics_process(double delta) override;

    protected:
    static void _bind_methods();

    private:
    bool playing = false;
    float fps = 10.0;

    void play(const bool toggle);
    bool is_playing() const;


};


#endif