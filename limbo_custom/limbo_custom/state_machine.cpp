#include "state_machine.h"

void StateMachine::_bind_methods()
{

}

void StateMachine::_ready()
{

	TypedArray<Node> childs = this -> get_children();

	for (Variant v : childs)
	{
		LimboState* state= Object::cast_to<LimboState>(v);
		if (state)
		{
				
		}
	}

}

bool StateMachine::add_state(Node* state)
{
	return false;
}

Node* StateMachine::get_state(StringName state_name)
{
	return nullptr;
}
