@tool
extends Area2D
#class_name Hurtbox
#
#const DEFAULT_COLOR: Color = Color.RED
#
#@export var active: bool = false: set = _show
#@export var box_colours: Dictionary[StringName, Color] = {}
#
#@export_tool_button("Update Areas", "CollisionShape2D")
#var update: Callable = _update
#
#func _enter_tree() -> void:
	#pass
#
#func _ready() -> void:
	#if box_colours.is_empty():
		#_get_areas()
#
#func _update() -> void: # 현재 자식콜리전들의 리스트를 업데이트
	#box_colours.clear()
	#_get_areas()
#
#func _get_areas(hide: bool = false) -> Array[CollisionShape2D]:
	#var result: Array[CollisionShape2D] = []
#
	#for node: Node in get_children():
		#if node is CollisionShape2D:
			#node.debug_color = DEFAULT_COLOR
			#box_colours[node.name] = DEFAULT_COLOR
			#result.push_back(node)
			#node.disabled = true if hide else false
			#node.visible = false if hide else true
#
	#return result
#
#func _show(toggle: bool) -> void:
	#active = toggle
	#
	#if active:
		#_get_areas()
	#else:
		#_get_areas(true)
#
#func get_pose_owner() -> Unit:
	#return (get_parent() as Pose).pose_by
