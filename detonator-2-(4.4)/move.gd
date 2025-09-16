extends UnitState

func _update(delta: float) -> void:
	var unit: Unit = agent as Unit
	unit.move(delta)
