#include "cyp_event.h"


Vector2 CypEvent::get_force() const
{
	return force;
}

void CypEvent::set_force(const Vector2& value)
{
	this->force = value;
}

uint64_t CypEvent::get_event_type() const
{
	return uint64_t(event_type);
}

void CypEvent::set_event_type(const uint64_t &type)
{
	event_type = Executioner::EventType(type);
}

void CypEvent::_bind_methods()
{
	// event_type
	ClassDB::bind_method(
		D_METHOD("set_event_type", "type"),
		&CypEvent::set_event_type
	);

	ClassDB::bind_method(
		D_METHOD("get_event_type"),
		&CypEvent::get_event_type
	);

	//ADD_PROPERTY(
	//	PropertyInfo(Variant::INT, "event_type", PROPERTY_HINT_ENUM, "Hit, Parry, Shield, Evade"),
	//	"set_event_type",
	//	"get_event_type"
	//);

	// force
	ClassDB::bind_method(
		D_METHOD("get_force"),
		&CypEvent::get_force
	);

	ClassDB::bind_method(
		D_METHOD("set_force"),
		&CypEvent::set_force
	);

	ADD_PROPERTY(
		PropertyInfo(Variant::VECTOR2, "force", PROPERTY_HINT_NONE, "", PROPERTY_USAGE_DEFAULT),
		"set_force",
		"get_force"
	);

}