@tool
extends ScrollContainer

@export var scroll_speed := 10;
@export var stay_seconds := 5;

var h_tween : Tween;
var v_tween : Tween;

var h_scroll_bar : HScrollBar;
var v_scroll_bar : VScrollBar;

var cached_h_max := NAN;
var cached_v_max := NAN;

func _ready() -> void:
	h_scroll_bar = get_h_scroll_bar();
	v_scroll_bar = get_v_scroll_bar();
	connect("sort_children", update_tween_deferred);

func update_tween_deferred() -> void:
	call_deferred("update_tween");
	
func update_tween() -> void:
	_h();
	_v();

func _h() -> void:
	var content_max := h_scroll_bar.max_value;
	if(cached_h_max == content_max): return
	cached_h_max = content_max;
	var current_max = size.x;
	var scroll_duration = (content_max - current_max) / scroll_speed;
	h_tween = _reset_tween(h_tween, content_max, current_max, h_scroll_bar, scroll_duration);
	pass
	
func _v() -> void:
	var content_max := v_scroll_bar.max_value;
	if(cached_v_max == content_max): return
	cached_v_max = content_max;
	var current_max = size.y;
	var scroll_duration = (content_max - current_max) / scroll_speed;
	v_tween = _reset_tween(v_tween, content_max, current_max, v_scroll_bar, scroll_duration);
	pass

func _reset_tween(tween:Tween, content_max:float, current_max:float, scroll_bar:ScrollBar, scroll_duration:float) -> Tween:
	if(tween != null): tween.kill();
	if(current_max >= content_max): return;
	scroll_bar.value = 0;
	var newTween = create_tween();
	newTween.tween_property(scroll_bar, "value", content_max, scroll_duration).set_delay(stay_seconds);
	newTween.tween_property(scroll_bar, "value", 0, scroll_duration).set_delay(stay_seconds);
	newTween.set_loops();
	return newTween;
