@tool
extends ScrollContainer

@export var scroll_speed := 10;
@export var stay_seconds := 5;

var h_tween : Tween;
var v_tween : Tween;

var h_scroll_bar : HScrollBar;
var v_scroll_bar : VScrollBar;

func _ready() -> void:
	h_scroll_bar = get_h_scroll_bar();
	v_scroll_bar = get_v_scroll_bar();
	v_scroll_bar.changed.connect(_v)
	h_scroll_bar.changed.connect(_h)

func _h() -> void:
	var content_max := h_scroll_bar.max_value - h_scroll_bar.page;
	h_tween = _reset_tween(h_tween, content_max, h_scroll_bar);
	pass
	
func _v() -> void:
	var content_max := v_scroll_bar.max_value - v_scroll_bar.page;
	v_tween = _reset_tween(v_tween, content_max, v_scroll_bar);
	pass

func _reset_tween(tween:Tween, content_max:float, scroll_bar:ScrollBar) -> Tween:
	if(tween != null): tween.kill();
	scroll_bar.value = 0;
	var scroll_duration := (scroll_bar.max_value - scroll_bar.page) / scroll_speed;
	var newTween = create_tween();
	newTween.tween_property(scroll_bar, "value", content_max, scroll_duration).set_delay(stay_seconds);
	newTween.tween_property(scroll_bar, "value", 0, scroll_duration).set_delay(stay_seconds);
	newTween.set_loops();
	return newTween;
