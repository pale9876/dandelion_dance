extends Node2D

@onready var limbo_hsm: LimboHSM = $LimboHSM
@onready var limbo_state: LimboState = $Node/LimboState

func _ready() -> void:
	limbo_hsm.initial_state = limbo_state
	limbo_hsm.initialize(self)
	limbo_hsm.set_active(true)
