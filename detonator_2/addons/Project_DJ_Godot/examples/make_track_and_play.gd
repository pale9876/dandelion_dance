extends PDJE_Wrapper

var engine:PDJE_Wrapper
var player:PlayerWrapper
var muspanel:MusPanelWrapper
var fxpanel:FXWrapper
var fxhandler:FXArgWrapper

func _ready() -> void:
	var engine = PDJE_Wrapper.new()
	engine.InitEngine("res://ROOTDB_DELETE_AFTER_DEMO")
	print(engine.SearchMusic("title", "composer"))
	
	engine.InitEditor("tester", "something", "res://SANDBOXDB_DELETE_AFTER_DEMO")
	var editor = engine.GetEditor()
	print(editor.ConfigNewMusic("title", "composer", "G://YMCA.wav"))#change path here
	var music_bpm_arg = PDJE_EDITOR_ARG.new()
	music_bpm_arg.InitMusicArg("title", "126", 0,0,4)#change bpm
	editor.AddLine(music_bpm_arg)
	var track_bpm_init_arg = PDJE_EDITOR_ARG.new()
	track_bpm_init_arg.InitMixArg(
		PDJE_EDITOR_ARG.EDITOR_TYPE_LIST.BPM_CONTROL,
		PDJE_EDITOR_ARG.EDITOR_DETAIL_LIST.TIME_STRETCH,
		1,#bpm control is env setting. id doesn't matters.
		"126",#track bpm
		"","",#ignored
		0,0,0,#0beat, 0subbeat bpm setting is essential
		0,0,0#doesn't matter
	)
	editor.AddLine(track_bpm_init_arg)
	
	var play_music_on_track = PDJE_EDITOR_ARG.new()
	play_music_on_track.InitMixArg(
		PDJE_EDITOR_ARG.EDITOR_TYPE_LIST.LOAD,
		0,
		1,
		"title",
		"composer",
		"126",
		0,0,0,
		4,0,0
	)
	editor.AddLine(play_music_on_track)
	var unload_music = PDJE_EDITOR_ARG.new()
	unload_music.InitMixArg(
		PDJE_EDITOR_ARG.EDITOR_TYPE_LIST.UNLOAD,
		0,
		1,
		"title",
		"composer",
		"126",
		60,0,0,
		60,0,0
	)
	editor.AddLine(unload_music)
	
	print(editor.render("demo_track"))
	editor.pushToRootDB("title", "composer")
	print(editor.pushTrackToRootDB("demo_track"))
	engine.InitPlayer(PDJE_Wrapper.HYBRID_RENDER, "demo_track", 48)
	player = engine.GetPlayer()
	player.Activate()
	muspanel = player.GetMusicControlPannel()
	muspanel.LoadMusic("title", "composer", 126)
	#editor.demoPlayInit(48, "demo_track")
	#player = engine.GetPlayer()
	#player.Activate()
	
var filter_frequency = 100.0
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
	
	if Input.is_action_pressed("ui_up"):
		filter_frequency = filter_frequency + 100.0
		fxhandler.SetFXArg(EnumWrapper.FILTER, "Filterfreq", filter_frequency)
		print("filter freq: ",filter_frequency)
		
	if Input.is_action_pressed("ui_down"):
		filter_frequency = filter_frequency - 100.0
		fxhandler.SetFXArg(EnumWrapper.FILTER, "Filterfreq", filter_frequency)
		print("filter freq: ",filter_frequency)
		
