#include "auto_sprite.h"

void AutoSprite::_physics_process(double delta)
{
    if (is_playing())
    {
        this -> set_time(time - delta);
        
        if (time < 0.0)
        {
            Vector2i coords = this -> get_frame_coords();

            if (coords.x == this -> get_hframes() - 1)
            {
                if (is_repeat())
                    this -> set_frame_coords(Vector2i(0, coords.y));
                else
                    stop();
            }
            else
            {
                this -> set_frame_coords(Vector2i(coords.x + 1, coords.y));
                time += 1.0 / fps;
            }
        }
    }
}

void AutoSprite::stop()
{
    this -> play(false);
}

void AutoSprite::pause()
{
    playing = false;
}

void AutoSprite::play(const bool toggle)
{
    this -> playing = toggle;

    if (toggle)
    {
        if (!paused)
        {
            time = 1.0 / fps;
            Vector2i coords = this -> get_frame_coords();
            set_frame_coords(
                Vector2i(0, coords.y)
            );
        }
    }
}

bool AutoSprite::is_playing() const
{
    return this -> playing;
}

void AutoSprite::set_repeat(const bool toggle)
{
    this -> repeat = toggle;
}

bool AutoSprite::is_repeat() const
{
    return this -> repeat;
}

void AutoSprite::set_fps(const float value)
{
    this -> fps = value;
}

float AutoSprite::get_fps() const
{
    return this -> fps;
}

void AutoSprite::set_pause(const bool toggle)
{
    if (toggle)
    {
        if (this -> is_playing())
            pause();
        else
            return;
    }
    else
        play(true);

    this -> paused = toggle;
}

bool AutoSprite::is_paused() const
{
    return this -> paused;
}

void AutoSprite::set_time(const double value)
{
    this -> time = value;
}

void AutoSprite::_bind_methods()
{

    StringName play_mthd = "play";
    StringName is_playing_mthd = "is_playing";

    StringName set_fps_mthd = "set_fps";
    StringName get_fps_mthd = "get_fps";

    StringName set_repeat_mthd = "set_repeat";
    StringName is_repeat_mthd = "is_repeat";

    StringName set_pause_mthd = "set_pause";
    StringName is_paused_mthd = "is_paused";

    StringName pause_mthd = "pause";
    StringName stop_mthd = "stop";


    // playing
    ClassDB::bind_method(
        D_METHOD(play_mthd, "toggle"),
        &AutoSprite::play
    );

    ClassDB::bind_method(
        D_METHOD(is_playing_mthd),
        &AutoSprite::is_playing
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::BOOL, "playing", PROPERTY_HINT_NONE),
        play_mthd,
        is_playing_mthd
    );


    // fps
    ClassDB::bind_method(
        D_METHOD(set_fps_mthd, "value"),
        &AutoSprite::set_fps
    );
    ClassDB::bind_method(
        D_METHOD(get_fps_mthd),
        &AutoSprite::get_fps
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::FLOAT, "fps", PROPERTY_HINT_NONE),
        set_fps_mthd,
        get_fps_mthd
    );

    // repeat
    ClassDB::bind_method(
        D_METHOD(set_repeat_mthd, "toggle"),
        &AutoSprite::set_repeat
    );

    ClassDB::bind_method(
        D_METHOD(is_repeat_mthd),
        &AutoSprite::is_repeat
    );

    ADD_PROPERTY(
        PropertyInfo(Variant::BOOL, "repeat"),
        set_repeat_mthd,
        is_repeat_mthd
    );

    // paused
    ClassDB::bind_method(
        D_METHOD(set_pause_mthd, "toggle"),
        &AutoSprite::set_pause
    );
    ClassDB::bind_method(
        D_METHOD(is_paused_mthd),
        &AutoSprite::is_paused
    );
    ADD_PROPERTY(
        PropertyInfo(Variant::BOOL, "paused"),
        set_pause_mthd,
        is_paused_mthd
    );

    // methods
    ClassDB::bind_method(
        D_METHOD(stop_mthd),
        &AutoSprite::stop
    );

    ClassDB::bind_method(
        D_METHOD(pause_mthd),
        &AutoSprite::pause
    );

}