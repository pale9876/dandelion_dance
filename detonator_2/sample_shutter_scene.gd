extends Node2D

@onready var sprite: Sprite2D = $Sprite2D

var shake: bool = false

var _time: float = 0.0
var pre_pos: Vector2 = Vector2.ZERO

func _process(delta: float) -> void:
	sprite.global_position = get_global_mouse_position()

	if Input.is_action_just_pressed("ui_accept"):
		_time = 1.0
	
	if _time > 0.0:
		var rand_pos: Vector2 = Vector2(randf_range(0., 100.), randf_range(0., 100.))
		global_position = (pre_pos + rand_pos) * clampf(_time, 0., 1.)
		_time -= delta
