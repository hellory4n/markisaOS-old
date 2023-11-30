extends ProgressBar
class_name ParadoxBar

@export var instability = 3
signal finished

func _process(delta):
	if randf_range(0, instability) < 1:
		value += randf_range(0, 90) * delta;
	
	if value >= max_value:
		finished.emit()
