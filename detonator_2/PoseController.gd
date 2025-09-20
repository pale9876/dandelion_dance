@tool
extends Node2D
#class_name PoseController
#
#signal pose_changed(pose: StringName)
#
#@export var box: Dictionary[StringName, Pose] = {}
#
#@export var current_pose: StringName = &""
#@export var index: int = -1
#
#func _enter_tree() -> void:
	#set_owner(get_parent())
	#
	#if not box.is_empty():
		#if current_pose.is_empty():
			#for pose: Pose in get_pose():
				#pass
		#index = get_pose_idx(current_pose)
#
#func get_pose_idx(pose_name: StringName) -> int:
	#var count: int = -1
	#
	#for pose: Pose in get_pose():
		#count += 1
		#if pose.name == pose_name:
			#return count
	#
	#return -1 # Cannot Found The Child Node Index.
#
#func get_pose() -> Array[Pose]:
	#var result: Array[Pose] = []
#
	#for node: Node in get_children():
		#if node is Pose:
			#box[node.name] = node
			#result.push_back(node)
	#
	#return result
#
#func set_idx(value: int) -> void:
	#index = value
#
#func set_current_pose(value: StringName) -> void:
	#current_pose = value
