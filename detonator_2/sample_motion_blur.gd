extends ScreenShutter

func _physics_process(delta: float) -> void:
	if (pre_texture):
		material.set_shader_parameter("framebuffer", pre_texture)
	
	pre_texture = _shutter()
