extends Node2D

@onready var from: Node2D = $Node2D
@onready var to: Area2D = $Area2D

func _physics_process(delta: float) -> void:
	var query: PhysicsRayQueryParameters2D = PhysicsRayQueryParameters2D.create(
		from.global_position,
		to.global_position,
	)
	
	query.collide_with_areas = true
	
	var collision: Dictionary = get_world_2d().direct_space_state.intersect_ray(query)
	
	print(collision)
