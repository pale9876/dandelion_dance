extends Control

@onready var graph_edit: GraphEdit = $VBoxContainer/GraphEdit

func _ready() -> void:
	graph_edit.gui_input.connect(_on_panel_gui_input)

func _on_panel_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_RIGHT:
			mouse_right_clicked_event_handler()

func mouse_right_clicked_event_handler() -> void:
	var c = Control.new()
	graph_edit.add_child(c)
