extends Node

enum TrafficLight {
	RED = 0,
	YELLOW = 1,
	GREEN = 2,
}

#const red_wait_tick: int = 180
#const yellow_wait_tick: int = 60
#const green_wait_tick: int = 300
#
@export var light: TrafficLight = TrafficLight.RED
@export var inc_dec: bool = true
#var tick: int = red_wait_tick

#func _physics_process(delta: float) -> void:
	#tick -= 1
	#if tick == 0:
		#light = 0 if (light == 2) else light + 1
#
#func set_state(state: TrafficLight) -> void:
	#light = state
	#match light:
		#TrafficLight.RED:
			#tick = red_wait_tick
		#TrafficLight.YELLOW:
			#tick = yellow_wait_tick
		#TrafficLight.GREEN:
			#tick = green_wait_tick
#
	#print("State: ", state)
