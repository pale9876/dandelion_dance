extends UnitState

func _enter() -> void:
	pass

func _update(delta: float) -> void:
	var unit: Unit = agent as Unit
	
	if agent.velocity.x != 0.0:
		agent.fric(delta)
