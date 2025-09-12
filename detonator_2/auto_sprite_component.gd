@tool
extends Node2D
class_name AutoSpriteComoponent

@export var dict: Dictionary[StringName, AutoSprite]
@export var index: int = -1
@export var is_playing: bool = false

func _enter_tree() -> void:
	index = _get_sprites(true).size() - 1

func _exit_tree() -> void:
	dict.clear()

func _play(sp_name: String) -> void:
	if dict.has(sp_name):
		for node_name: StringName in dict:
			var sprite: AutoSprite = dict[node_name] as AutoSprite
			if node_name == sp_name:
				sprite.visible = true
			else:
				sprite.visible = false

func _get_sprites(init: bool) -> Array[AutoSprite]:
	var _arr: Array[AutoSprite] = []
	
	var _idx: int = -1
	for node: Node in get_children():
		if node is AutoSprite:
			if init:
				_idx += 1
				dict[node.name] = node
				node.finished.connect(anim_finished)
				node.cascade.connect(on_cascade)
			
			_arr.push_back(node)
	
	return _arr

func anim_finished(sprite_name: StringName) -> void:
	pass

func on_cascade(c: Callable) -> void:
	pass
