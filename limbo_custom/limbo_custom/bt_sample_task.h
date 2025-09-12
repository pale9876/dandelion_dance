#pragma once

#ifndef BT_SAMPLE_TASK_H
#define BT_SAMPLE_TASK_H

#include "bt/tasks/bt_action.h"

class BTSampleTask : public BTAction
{
    GDCLASS(BTSampleTask, BTAction);
    TASK_CATEGORY(Utility);



    public:
    BT::Status _tick(double delta) override;

    protected:
    static void _bind_methods();


};


#endif // !1