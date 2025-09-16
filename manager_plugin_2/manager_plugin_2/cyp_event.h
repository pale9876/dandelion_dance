#pragma once

#ifndef CYP_EVENT_H
#define CYP_EVENT_H

#include "executioner.h"

#include <godot_cpp/classes/ref.hpp>
#include <godot_cpp/classes/resource.hpp>
#include <godot_cpp/classes/object.hpp>
#include <godot_cpp/classes/node.hpp>

using namespace godot;

class CypEvent : public Resource
{

	GDCLASS(CypEvent, Resource);

	public:
	uint64_t get_event_type() const;
	void set_event_type(const uint64_t &type);

	Vector2 get_force() const;
	void set_force(const Vector2 &value);
	
	protected:
	static void _bind_methods();

	private:
	Executioner::EventType event_type = Executioner::EventType::HIT;
	Vector2 force = Vector2(0.f, 0.f);

};

#endif