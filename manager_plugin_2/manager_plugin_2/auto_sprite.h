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
    bool repeat = false;
    bool paused = false;
    float fps = 10.0;
    double time = 0.0;

    void play(const bool toggle);
    bool is_playing() const;

    void set_repeat(const bool toggle);
    bool is_repeat() const;
   
    void set_fps(const float value);
    float get_fps() const;

    void set_pause(const bool toggle);
    bool is_paused() const;

    void set_time(const double value);
    void stop();
    void pause();
};


#endif