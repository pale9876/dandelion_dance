@tool
extends EditorPlugin

const INSPECTOR_SCRIPT: Script = preload("res://addons/sample_plugin/sample_inspector_plugin.gd")

var plugin: EditorInspectorPlugin = null

func _enter_tree() -> void:
	plugin = INSPECTOR_SCRIPT.new()
	add_inspector_plugin(plugin)

func _exit_tree() -> void:
	# Clean-up of the plugin goes here.
	remove_inspector_plugin(plugin)
