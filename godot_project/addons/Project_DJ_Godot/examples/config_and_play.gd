extends PDJE_Wrapper

var engine:PDJE_Wrapper
var player:PlayerWrapper
var muspanel:MusPanelWrapper

func _ready() -> void:
	var engine = PDJE_Wrapper.new()
	engine.InitEngine("res://rootdb")
	if engine.SearchMusic("title", "composer").is_empty():
		engine.InitEditor("name", "none", "res://sandbox")
		var editor = engine.GetEditor()
		print(editor.ConfigNewMusic("title", "composer", "G://YMCA.wav"))#change path here
		var edit_args = PDJE_EDITOR_ARG.new()
		edit_args.InitMusicArg("title", "120", 0,0,4)
		
		editor.AddLine(edit_args)
		print(editor.render("sample_track"))
		print(editor.pushToRootDB("title", "composer"))
	
	engine.InitPlayer(PDJE_Wrapper.FULL_MANUAL_RENDER, "void", 48)
	player = engine.GetPlayer()
	print(player.Activate())
	muspanel = player.GetMusicControlPannel()
	print(muspanel.GetLoadedMusicList())
	print(muspanel.LoadMusic("title", "composer", 120))
		

var turn_on = false
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("ui_accept"):
		if turn_on:
			print("stopped")
			muspanel.SetMusic("title", false)
			turn_on = false
		else:
			print("playback")
			print(muspanel.SetMusic("title", true))
			print(muspanel.GetLoadedMusicList())
			turn_on = true
