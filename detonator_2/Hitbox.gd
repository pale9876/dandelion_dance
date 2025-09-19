@tool
extends Area2D
#class_name Hitbox
#
#const DEFAULT_COLOR: Color = Color.YELLOW
#
#enum HitType {
	#HIGH,
	#MIDDLE,
	#LOW,
#}
#
#@export var hit_type: HitType = HitType.MIDDLE
#@export var active: bool = false
#
#var _damage: int = 1
#
#@onready var judge_area: CollisionShape2D = $JudgeArea
#
#var cache: Array[CollisionShape2D]
#
#func _enter_tree() -> void:
	#if !Engine.is_editor_hint():
		#area_shape_entered.connect(_area_entered)
		#area_shape_exited.connect(_area_exited)
#
#func _physics_process(delta: float) -> void:
	#pass
#
#func _area_entered(_a_rid:RID, area: Area2D, shape_idx:int, local_idx: int) -> void:
	#if area is Hurtbox:
		#if area.get_pose_owner() == get_pose_owner(): return
		#print("Hurtbox Entered")
		#
		#var shape_owner: HurtArea = area.shape_owner_get_owner(shape_idx) as HurtArea
		#
		#print(shape_owner)
		#
		#if shape_owner.type == hit_type:
			#pass
#
#func _area_exited(_a_rid:RID, area: Area2D, shape_idx:int, local_idx: int) -> void:
	#pass
#
#func _check_ray() -> bool:
	#return false
#
#func hit() -> void:
	#print("Hit")
#
#func get_pose_owner() -> Unit:
	#return (get_parent() as Pose).pose_by
#
#func _set_active(toggle: bool) -> void:
	#pass

#region Garbage

	# var other_shape_owner = area.shape_find_owner(area_shape_index)
	# var other_shape_node = area.shape_owner_get_owner(other_shape_owner)
	# var local_shape_owner = shape_find_owner(local_shape_index)
	# var local_shape_node = shape_owner_get_owner(local_shape_owner)

	#var query: PhysicsRayQueryParameters2D = PhysicsRayQueryParameters2D.create(
			#global_position,
			#area.global_position,
		#)
	#query.collide_with_areas = true
	#var collision: Dictionary = get_world_2d().direct_space_state.intersect_ray(query)
	#
	#print(collision)
	#
	#if collision:
		#print(collision["collider"])
		#if collision["collider"] == area:
			#hit()
			#print("Shape idx: ", shape_idx)
#endregion
