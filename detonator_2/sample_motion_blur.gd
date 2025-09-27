extends ScreenShutter

@export var timer_mode: bool = false

@export var tick: float = 0.05
var timer: float = 0.0: set = set_timer

func _process(delta: float) -> void:
	pass

func _physics_process(delta: float) -> void:
	if timer_mode:
		timer -= delta
	else:
		_update()

func set_timer(value: float) -> void:
	timer = maxf(value, 0.0)
	if timer == 0.0:
		_update()
		timer = tick

func _update() -> void:
	if (pre_texture):
		material.set_shader_parameter("framebuffer", pre_texture)
	
	pre_texture = _shutter()
