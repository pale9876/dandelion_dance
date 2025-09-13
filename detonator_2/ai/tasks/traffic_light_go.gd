extends BTAction
class_name BTTrafficLight
 
func _tick(delta: float) -> Status:
	if agent.inc_dec:
		agent.light += 1
	else:
		agent.light -= 1
	
	return SUCCESS
