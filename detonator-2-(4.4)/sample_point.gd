@tool
extends Node2D

@export var mouse: bool = false

func _process(delta: float) -> void:
	if Engine.is_editor_hint(): return
	
	if mouse:
		var pos: Vector2 = get_global_mouse_position()
		global_position = pos


func _draw() -> void:
	draw_circle(
		Vector2.ZERO,
		10.0,
		Color.RED,
		true
	)
