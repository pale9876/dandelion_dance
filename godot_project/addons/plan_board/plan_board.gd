@tool
extends EditorPlugin

const GRAPH_UI: PackedScene = preload("res://addons/plan_board/graph_ui.tscn")

var instance: Control

func _enable_plugin() -> void:
	# Add autoloads here.
	pass


func _disable_plugin() -> void:
	# Remove autoloads here.
	pass


func _enter_tree() -> void:
	instance = GRAPH_UI.instantiate()
	EditorInterface.get_editor_main_screen().add_child(instance)
	instance.set_anchors_preset(Control.PRESET_FULL_RECT)
	_make_visible(false)

func _exit_tree() -> void:
	if instance:
		instance.queue_free()

func _has_main_screen() -> bool:
	return true

func _make_visible(visible: bool) -> void:
	if instance:
		instance.visible = visible

func _get_plugin_name() -> String:
	return "PlanBoard"

func _get_plugin_icon() -> Texture2D:
	return EditorInterface.get_editor_theme().get_icon("Node", "EditorIcons")
