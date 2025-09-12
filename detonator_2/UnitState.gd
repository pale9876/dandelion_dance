extends LimboState
class_name UnitState

@export var individuality: bool = false
@export_range(0.0, 4.0, 0.001) var delay_time: float = 0.85

var old_dir: float = 0.0
var propel: float = 0.0
var _delay: float = 0.0: set = _set_delay

var previous_state: LimboState = null

func _ready() -> void:
	if individuality:
		pass

func _exit() -> void:
	old_dir = 0.0
	propel = 0.0

#region SETGET
func _set_delay(time: float) -> void:
	_delay = maxf(0.0, time)
#endregion
