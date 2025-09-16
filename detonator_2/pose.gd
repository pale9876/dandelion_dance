@tool
extends Node2D
class_name Pose

@export var hitbox: Hitbox = null
@export var hurtbox: Hurtbox = null

@export_tool_button("Update Hit/Hurt boxes", "Area2D")
var update: Callable = _update

var pose_by: Unit = null

func _enter_tree() -> void:
	pose_by = get_parent().get_owner() as Unit

	visibility_changed.connect(_visibility_toggled)

func _exit_tree() -> void:
	visibility_changed.disconnect(_visibility_toggled)

func _visibility_toggled() -> void:
	hurtbox.active = false

func _ready() -> void:
	for node: Node in get_children():
		if node is Hitbox:
			hitbox = node
		elif node is Hurtbox:
			hurtbox = node

func _update() -> void:
	pass
