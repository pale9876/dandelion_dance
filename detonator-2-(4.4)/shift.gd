extends UnitState

@export_range(3500.0, 5500.0, 10.0) var fric: float = 4500.0

func _enter() -> void:
	var unit: Unit = agent as Unit
	
	old_dir = unit.get_horz()
	propel = unit.get_speed() * old_dir * 2.25
	
	_delay = delay_time
	unit.act_time = _delay

func _update(delta: float) -> void:
	var unit: Unit = agent as Unit
	
	unit.velocity.x = propel
	propel = move_toward(propel, 0.0, delta * fric)
	
	#if is_active(): print(propel)
	
	_delay -= delta
	unit.act_time = _delay
	
	if _delay == 0.0:
		unit.state_machine.change_active_state(previous_state)
