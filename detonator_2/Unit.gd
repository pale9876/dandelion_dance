#@tool
extends CharacterBody2D
class_name Unit

const TO_MOVE: StringName = &"to_move"
const TO_IDLE: StringName = &"to_idle"
const JUMP_UP: StringName = &"jump_up"
const FALL_DOWN: StringName = &"fall_down"

const ALWAYS_MOVE: StringName = &"al_move"
const ALWAYS_IDLE: StringName = &"al_idle"
const ALWAYS_SHIFT: StringName = &"al_shift"

const DEFAULT_FRIC: float = 2250.0
const DEFAULT_ACCEL: float = 3350.0

const MAX_GRAVITY: float = 3300.0

enum AirState {
	NONE,
	JUMPUP,
	AIRBORN,
	FALLDOWN,
}

signal chosen() # (디버그) 플레이어에게 선택받음
signal hp_changed(value: int)
signal max_hp_changed(value: int)

@export var stat: UnitStat
@export var handy: bool = true
@export var can_shift: bool = true

var _current_hp: int: set = set_hp
var _max_hp: int: set = set_max_hp
var _speed: float: set = set_speed

var dragging: bool = false

@export var pickable: bool = true
@export var auto_sprite: Sprite2D

var invincible: bool = false
var _intime: float = 0.0: set = activate_invincible # 무적시간

var in_action: bool = false: get = is_action
var act_time: float = 0.0: set = set_act_time
var current_act: StringName = &""

var _airstate: AirState = AirState.NONE: set = _set_airborn
var move_dir: Vector2 = Vector2.ZERO: set = _set_move_dir
var old_velocity: Vector2 = Vector2.ZERO

@export var state_machine: StateMachine
@export var pose_controller: PoseController

@export var _collisions: Dictionary[StringName, CollisionShape2D] = {}
var current_collision: CollisionShape2D = null

@onready var state_monitor: Label = $StateMonitor

func _enter_tree() -> void:
	if Engine.is_editor_hint(): return
	
	if stat == null:
		stat = UnitStat.new()
	
	_max_hp = stat.hp
	_current_hp = _max_hp
	_speed = stat.speed

func _ready() -> void:
	# [Tool] Get Collisions
	for node: Node in get_children():
		if node is CollisionShape2D:
			_collisions[node.name] = node # add collision_name : collision*

	if Engine.is_editor_hint(): return
	
	if pickable:
		input_pickable = true
		input_event.connect(_on_input_event)

	#UnitController.add_unit(self)


	if state_machine:
		# state machine monitoring connect signal
		state_machine.active_state_changed.connect(_on_state_changed)
		
		state_machine.initial_state = state_machine.get_state(state_machine.IDLE_STATE)
		#print(state_machine.initial_state)
		
		state_machine.initialize(self)
		
		#Add Default Moves Transition
		state_machine.add_transition(
			state_machine.get_state(state_machine.IDLE_STATE),
			state_machine.get_state(state_machine.MOVE_STATE),
			TO_MOVE,
		)
		state_machine.add_transition(
			state_machine.get_state(state_machine.MOVE_STATE),
			state_machine.get_state(state_machine.IDLE_STATE),
			TO_IDLE
		)
		state_machine.add_transition(
			state_machine.ANYSTATE,
			state_machine.get_state(state_machine.FALL_STATE),
			FALL_DOWN
		)
		state_machine.add_transition(
			state_machine.ANYSTATE,
			state_machine.get_state(state_machine.IDLE_STATE),
			ALWAYS_IDLE
		)
		state_machine.add_transition(
			state_machine.ANYSTATE,
			state_machine.get_state(state_machine.MOVE_STATE),
			ALWAYS_MOVE
		)
		
		if can_shift:
			state_machine.add_transition(
				state_machine.ANYSTATE,
				state_machine.get_individual_act(state_machine.SHIFT_STATE),
				ALWAYS_SHIFT
			)

		# activate state_machine
		state_machine.set_active(true)
		state_monitor.text = str(state_machine.get_active_state().name)

func _process(delta: float) -> void:
	if Engine.is_editor_hint(): return
	
	if act_time > 0.0:
		act_time -= delta

func _physics_process(delta: float) -> void:
	if Engine.is_editor_hint(): return
	
	if dragging:
		global_position = global_position.lerp(get_global_mouse_position(), 0.24)
		return

	if is_on_floor():
		_airstate = AirState.NONE
	#else:
		#_get_stage_gravity(delta)

	#_moved(delta)
	move_and_slide()
	
	old_velocity = velocity

#func _exit_tree() -> void:
	#UnitController.del_unit(self)

func _on_state_changed(curr_state: LimboState, prev_state: LimboState) -> void:
	state_monitor.text = str(curr_state.name)
	
	(curr_state as UnitState).previous_state = prev_state

func _on_input_event(vp: Node, ev: InputEvent, s_idx: int) -> void:
	if ev is InputEventMouseMotion:
		if ev.button_mask == MOUSE_BUTTON_MASK_LEFT:
			dragging = true
			current_collision.disabled = true
#
	#if ev is InputEventMouseButton:
		#if ev.is_pressed() and ev.button_index == MOUSE_BUTTON_LEFT:
			#_clicked()
#
#func _input(ev: InputEvent) -> void:
	#if ev is InputEventMouseButton:
		#if ev.is_released() and ev.button_index == MOUSE_BUTTON_LEFT:
			#dragging = false
			#current_collision.disabled = false
#
#func move(delta: float) -> Vector2:
	#velocity.x = move_toward(velocity.x, get_horz() * _speed, delta * DEFAULT_ACCEL)
	#return velocity

func on_shift() -> void:
	transit_state(ALWAYS_SHIFT)

func jump(delta: float) -> Vector2:
	return Vector2.ZERO

func fric(delta: float) -> void:
	velocity.x = move_toward(velocity.x, 0.0, delta * DEFAULT_FRIC)

#func _clicked() -> void:
	#PlayerController.set_control(self)
#
#func get_vert() -> float:
	#return PlayerController.get_input_dir().y
#
#func get_horz() -> float:
	#return PlayerController.get_input_dir().x

#func _get_stage_gravity(delta: float) -> float:
	#velocity.y = minf(
		#velocity.y + StageController.gravity * delta,
		#MAX_GRAVITY
	#)
	#
	#if velocity.y > 0.0: # fall
		#_airstate = AirState.FALLDOWN
		#
	#elif velocity.y < 0.0: # jump up
		#_airstate = AirState.JUMPUP
	#
	#return StageController.gravity

func _damaged(value: int) -> void:
	_current_hp -= value

func transit_state(event: StringName) -> bool:
	return state_machine.dispatch(event) if state_machine else false

func transit_to_aciton(event: StringName) -> bool:
	return false

#region SETGET
func set_speed(value: float) -> void:
	_speed = value

func set_max_hp(value: int) -> void:
	_max_hp = value
	max_hp_changed.emit(value)

func set_hp(value: int) -> void:
	_current_hp = value
	hp_changed.emit(value)

func is_handy() -> bool:
	return handy

#func set_handy(toggle: bool) -> void:
	#handy = toggle
	#if not toggle:
		#if PlayerController.control == self:
			#PlayerController.control_disable()

func _set_move_dir(value: Vector2) -> void:
	if value.x != 0.0:
		transit_state(TO_MOVE)
	else:
		transit_state(TO_IDLE)

	move_dir = value

func _set_airborn(state: AirState) -> void:
	if in_action:
		_airstate = AirState.NONE
	else:
		if _airstate == state: return
		
		_airstate = state
		
		if state == AirState.JUMPUP: # 상승
			transit_state(JUMP_UP)
		elif state == AirState.FALLDOWN: # 낙하
			transit_state(FALL_DOWN)
		elif state == AirState.NONE: # 착지
			if velocity.x != 0.0:
				transit_state(ALWAYS_MOVE)
			else:
				transit_state(ALWAYS_IDLE)

func activate_invincible(time: float) -> void:
	if time < - 1.0: # INF invincible 무한무적
		invincible = true
		return
	
	_intime = maxf(0.0, time)
	
	if _intime > 0.0:
		invincible = true
	else:
		invincible = false

func is_action() -> bool:
	return true if act_time > 0.0 else false

func set_act_time(time: float) -> void:
	act_time = maxf(0.0, time)

func get_speed() -> float:
	return _speed

#endregion
