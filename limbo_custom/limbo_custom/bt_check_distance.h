#pragma once

#ifndef BT_CHECK_DISTANCE
#define BT_CHECK_DISTANCE

#include "bt/tasks/bt_action.h"

class BTCheckDistance : public BTAction
{
    GDCLASS(BTCheckDistance, BTAction);
    TASK_CATEGORY(Custom);
    public:
    BT::Status _tick(double delta) override;

    protected:
    static void _bind_methods();

};


#endif // !BT_CHECK_DISTANCE
