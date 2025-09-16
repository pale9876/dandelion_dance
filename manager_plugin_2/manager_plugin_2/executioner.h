#pragma once

#ifndef EXECUTIONER_H
#define EXECUTIONER_H

#include <godot_cpp/classes/node.hpp>
#include <godot_cpp/variant/typed_dictionary.hpp>
#include <godot_cpp/variant/array.hpp>

using namespace godot;

class Executioner : public godot::Node
{
	GDCLASS(Executioner, Node);

	protected:
	static void _bind_methods();

	public:
	enum EventType
	{
		HIT = 0,
		Parry = 1,
		Shield = 2,
	};

	private:
	TypedDictionary<int, Array> event_cache;

};

#endif