@tool
extends Sprite2D
class_name AutoSprite

enum Type {
	IDLE,
	PHYSICS,
	CUSTOM,
}

signal cascade(c: Callable)
signal finished(sprite_name: StringName)

@export var type: Type = Type.CUSTOM
@export var fps: float = 10.0
@export var trigger: Dictionary[int, Callable]

var _component: AutoSpriteComoponent = null

func _enter_tree() -> void:
	_component = get_parent() as AutoSpriteComoponent if get_parent() != null else null
	
	frame_changed.connect(_on_frame_changed)

func _exit_tree() -> void:
	if _component:
		finished.disconnect(_component.anim_finished)
		cascade.disconnect(_component.on_cascade)

	frame_changed.disconnect(_on_frame_changed)
	
	trigger.clear()

func _on_frame_changed() -> void:
	if trigger.has(frame):
		cascade.emit(trigger[frame])
	
	if frame == get_max():
		finished.emit(name)

func action() -> void:
	pass

func _get_configuration_warnings() -> PackedStringArray:
	var warn: PackedStringArray = []
	return warn

func get_max() -> int:
	return hframes * vframes - 1
