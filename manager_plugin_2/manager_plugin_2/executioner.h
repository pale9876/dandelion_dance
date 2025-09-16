#pragma once

#ifndef EXECUTIONER_H
#define EXECUTIONER_H

#include "entity.h"

#include <godot_cpp/classes/node.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/variant/array.hpp>

using namespace godot;

class Executioner : public Node
{
	GDCLASS(Executioner, Node);

	protected:
	static void _bind_methods();

	public:
	static bool exec_init();
	static bool deinit();
	static Executioner& get_sys();

	void _physics_process(double delta) override;

	enum EventType
	{
		HIT = 0,
		PARRY = 1,
		SHIELD = 2,
		EVADE = 3,
	};

	bool add_event(int &event_type, Node* from, Node* to);

	private:
	uint64_t index = 0;
	TypedDictionary<int, Dictionary> event_cache;

	bool _handle(uint64_t idx);
};

#endif