#include "squad.h"

void Squad::_ready()
{
	Engine* engine = Engine::get_singleton();
	if (engine->is_editor_hint()) return;

	TypedArray<Node> _arr = this -> get_children();
	
	for (Variant value : _arr)
	{
		Entity* entity = Object::cast_to<Entity>(value);
		if (entity)
		{
			if (!squad_leader)
			{
				squad_leader = entity;
			}
			entities.push_back(value);
		}
	}
}

void Squad::appoint_leader(Node* entity)
{
	Entity* ett = Object::cast_to<Entity>(entity);
	
	if (ett)
	{
		squad_leader = ett;
	}
}

Node* Squad::get_leader()
{
	return Object::cast_to<Node>(squad_leader);
}

void Squad::_bind_methods()
{
	
	ClassDB::bind_method(
		D_METHOD("appoint_leader", "entity"),
		&Squad::appoint_leader
	);

	ClassDB::bind_method(
		D_METHOD("get_leader"),
		&Squad::get_leader
	);
}
