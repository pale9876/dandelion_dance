#pragma once

#ifndef STATE_MACHINE_H
#define STATE_MACHINE_H

#include "hsm/limbo_hsm.h"

class StateMachine : public LimboHSM
{

    protected:
    static void _bind_methods();

};

#endif