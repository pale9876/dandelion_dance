extends LimboHSM
#class_name StateMachine
#
## Default State Names
#const IDLE_STATE: StringName = &"Idle"
#const MOVE_STATE: StringName = &"Move"
#const JUMP_STATE: StringName = &"Jump"
#const DRAG_STATE: StringName = &"Drag"
#const DASH_STATE: StringName = &"Dash"
#const FALL_STATE: StringName = &"Fall"
#
## Act State Names
#const SHIFT_STATE: StringName = &"Shift"
#const SLIDE_STATE: StringName = &"Slide"
#
#@export var _dict: Dictionary[StringName, UnitState] = {} # 기본 상태
#@export var individual_acts: Dictionary[StringName, UnitState] = {} # 독립 상태 (커스텀 액션)
#
#func _ready() -> void:
	#for node: Node in get_children():
		#if node is UnitState:
			#if node.individuality:
				#individual_acts[node.name] = node
			#else:
				#_dict[node.name] = node
#
#func add_state(state: LimboState) -> void:
	#add_child(state)
	#_dict[state.name] = state
#
#func get_state(s_name: StringName) -> LimboState:
	#var state: LimboState = _dict[s_name] as LimboState
	#return state
#
#func get_individual_act(s_name: StringName) -> UnitState:
	#var i_state: UnitState = individual_acts[s_name] as UnitState
	#return i_state
#
#func has_state(s_name: StringName) -> bool:
	#return _dict.has(s_name)
#
#func is_individual_act(act_name: String) -> bool:
	#return individual_acts.has(act_name)
