extends Node

var count: int = 0

var dict: Dictionary[int, Dictionary] = {}

func _physics_process(delta: float) -> void:
	for idx: int in dict:
		dict[idx]["pos"] = dict[idx]["node"].global_position

func add_unit(unit: Unit) -> void:
	var idx: int = count
	dict[idx] = {}
	dict[idx]["name"] = unit.name
	dict[idx]["node"] = unit
	dict[idx]["pos"] = unit.global_position
	count += 1

func delete(idx: int) -> void:
	dict.erase(idx)
	count -= 1

func del_unit(unit: Unit) -> void:
	for idx: int in dict:
		if dict[idx]["node"] == unit:
			delete(idx)
			return
