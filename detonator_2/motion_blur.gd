@tool
extends ScreenShutter

const BLUR_SHADER: Shader = preload("res://shaders/motion_blur.gdshader")

var active: bool: get = is_activated
var time: float = 0.0: get = get_time, set = set_active_time

func _init() -> void:
	material = ShaderMaterial.new()
	material.shader = BLUR_SHADER

func _process(delta: float) -> void:
	if Engine.is_editor_hint(): return
	
	if active and mode == IDLE():
		_active()
		if time > 0.0:
			time -= delta

func _physics_process(delta: float) -> void:
	if Engine.is_editor_hint(): return
	
	if active and mode == PHYSICS():
		_active()
		if time > 0.0:
			time -= delta

func _active() -> void:
	pre_texture = shutter()
	if (pre_texture):
		(material as ShaderMaterial).set_shader_parameter("frame_buffer", pre_texture)

func get_time() -> float:
	return time

func set_active_time(value: float) -> void:
	time = maxf(0.0, value)
	visible = false if time == 0.0 else true

func is_activated() -> bool:
	return time > 0.
