#include "executioner.h"

Executioner* executioner = nullptr;

bool Executioner::exec_init()
{
	executioner = memnew(Executioner);
	return executioner;
}

bool Executioner::deinit()
{
	memdelete(executioner);
	return !executioner;
}

Executioner& Executioner::get_sys()
{
	return *executioner;
}

void Executioner::_physics_process(double delta)
{
	if (!event_cache.is_empty())
	{
		for (int i : event_cache.keys())
		{
			_handle(i);
		}
		
		index = 0;
	}
}

bool Executioner::add_event(int &event_type, Node* from, Node* to)
{
	if (!from || !to)
	{
		print_error(vformat("From or to Nodes are not valid"));
		return false;
	}

	EventType ev_type = EventType(event_type);

	Dictionary _dict = {};

	_dict.set("event", event_type);
	_dict.set("from", from);
	_dict.set("to", to);

	event_cache.set(index, _dict);

	index += 1;

	return true;
}

bool Executioner::_handle(uint64_t idx)
{
	Dictionary cache = event_cache[idx];

	EventType ev = EventType(uint64_t(cache["event"]));
	Entity* from = Object::cast_to<Entity>(cache["from"]);
	Entity* to = Object::cast_to<Entity>(cache["to"]);

	if (!from || !to)
	{
		print_error(vformat("From or to Nodes are not valid"));
		return false;
	}

	to -> event_received(ev, from);

	return true;
}

void Executioner::_bind_methods()
{
	BIND_ENUM_CONSTANT(HIT);
	BIND_ENUM_CONSTANT(PARRY);
	BIND_ENUM_CONSTANT(SHIELD);

	ClassDB::bind_method(
		D_METHOD("add_event", "event_type", "from", "to"),
		&Executioner::add_event
	);

	ClassDB::bind_static_method(
		"Executioner",
		D_METHOD("get_sys"),
		&Executioner::get_sys()
	);

}


