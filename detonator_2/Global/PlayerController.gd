extends Node
#
#signal input()
#signal changed(unit: Node2D)
#signal shift()
#
#const MARGIN_TIME: float = 0.35
#
#var in_control: Unit
#var p_camera: Camera2D
#var input_component: InputComponent
#
#var _dir: Vector2: get = get_input_dir, set = set_input_dir
#var _cache_last_dir_x: float = 0.0
#var _margin: float = 0.0: set = set_input_margin
#var detecting: bool = false
#
#var dir_record: PackedVector2Array = []
#var abs_dir_record: PackedVector2Array = [] # 방향 절대값, 유닛이 보는 방향을 왼방향 기준으로 인풋 방향을 캐싱
#
#var ctrl_index: Dictionary[int, Unit] = {}
#
#var indexing: bool = false
#
#var clicked: Node2D = null
#var dragging_object: Node2D = null
#
#func _init() -> void:
	#input_component = InputComponent.new()
	#input_component.name = &"InputComponent"
#
##func _enter_tree() -> void:
	##shift.connect(_on_shift)
#
#func _process(delta: float) -> void:
	#indexing = true if Input.is_action_pressed(&"Control") else false
	#if _margin > 0.0:
		#_margin -= delta
#
#func _physics_process(delta: float) -> void:
	#if in_control:
		#in_control.move_dir = get_input_dir()
#
#func _input(event: InputEvent) -> void:
	#if event is InputEventKey:
		#if not event.is_echo():
			#_dir.x = Input.get_action_strength(&"right") - Input.get_action_strength(&"left")
			#
			#if _dir.x != 0.0:
				#if _margin > 0.0 and _cache_last_dir_x == _dir.x:
					#_margin = 0.0
					#shift.emit()
				#else:
					#_margin = MARGIN_TIME # 여유 인풋타임 추가
					#_cache_last_dir_x = _dir.x # 마지막 인풋 방향 캐싱
			#
			#_dir.y = Input.get_action_strength(&"down") - Input.get_action_strength(&"up")
			#
			#detecting = false if _dir == Vector2.ZERO else true # 단순 입력상태 업데이트
			#
			#if indexing:
				#if ["1", "2", "3", "4", "5", "6", "7", "8"].has(event.as_text()):
					#ctrl_index[(event.as_text().to_int())] = in_control
#
		##print(event.as_text())
#
##func _on_shift() -> void:
	##in_control.shift()
#
#func is_retaining() -> void:
	#pass
#
#func set_control(unit: Unit) -> void:
	#if unit.is_handy():
		#if in_control != null:
			#control_disable()
		#in_control = unit
		#unit.add_child(input_component)
		#if in_control.can_shift:
			#shift.connect(in_control.on_shift)
	#else:
		#print("This unit can't control from player.")
#
#
#func control_disable() -> void:
	#if in_control.can_shift:
		#shift.disconnect(in_control.on_shift)
	#
	#in_control.remove_child(input_component)
	#in_control = null
#
##region SETGET
#func get_input_dir() -> Vector2:
	#return _dir
#
#func set_input_dir(value: Vector2) -> void:
	#_dir = value
#
#func set_input_margin(time: float) -> void:
	#_margin = maxf(0.0, time)
##endregion
#
#func get_dragging_object() -> Node2D:
	#return dragging_object
