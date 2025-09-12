#include "auto_sprite.h"

void AutoSprite::_physics_process(double delta)
{
    


}

void AutoSprite::play(const bool toggle)
{
    playing = toggle;

}

bool AutoSprite::is_playing() const
{
    return this -> playing;
}
