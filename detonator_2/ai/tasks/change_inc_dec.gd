extends BTAction
class_name BTChangeIncDec

func _tick(delta: float) -> Status:
	
	agent.inc_dec = true if !agent.inc_dec else false
	return SUCCESS
