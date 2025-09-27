extends Node

@export var text: String = "나는 타인의 그림자이자 심연이다."

func _ready() -> void:
	var kte: KoreanTypingEffecter = KoreanTypingEffecter.create(text);
	print(kte.get_chars());
	kte.divide(true)
