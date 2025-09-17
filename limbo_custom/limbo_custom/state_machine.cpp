#include "state_machine.h"

void StateMachine::_ready()
{
	for (Variant v : this -> get_children())
	{
		LimboState* state = Object::cast_to<LimboState>(v);

		if (state)
		{
			states.set(state -> get_name(), state);
		}
	}
}

void StateMachine::_bind_methods()
{

}

