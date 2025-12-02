@tool
## Watches the attached [ScrollContainer], 
## and makes its content do ping-pong action 
## automatically when exceeds the size of the viewport, 
## this is an easy and design-wise cheap way for 
## addressing UI overflow issues when dealing with 
## the situation when the text becomes larger than it was
## when designing the UI. 
## This script also disables user interactions 
## for the controlled [ScrollContainer].

class_name AutoScroller
extends ScrollContainer

## The scroll speed, in pixel, 
## per second when scrolling the contents.
@export var scroll_speed := 10;

## The duration, in seconds, for
## auto scroller to stay when reaches 
## the top/left or the bottom/right side
## of the viewport.
@export var stay_seconds := 5;

var _h_tween : Tween;
var _v_tween : Tween;
var _h_scroll_bar : HScrollBar;
var _v_scroll_bar : VScrollBar;

func _ready() -> void:
	_h_scroll_bar = get_h_scroll_bar();
	_v_scroll_bar = get_v_scroll_bar();
	
	_h_scroll_bar.changed.connect(_update_horizontal_metrics);
	_v_scroll_bar.changed.connect(_update_vertical_metrics);
	
	_init_control(_h_scroll_bar);
	_init_control(_v_scroll_bar);
	_init_control(self);

func _init_control(control:Control) -> void:
	control.focus_mode = Control.FOCUS_NONE;
	control.mouse_filter = Control.MOUSE_FILTER_IGNORE;

func _update_horizontal_metrics() -> void:
	var content_max := _h_scroll_bar.max_value - _h_scroll_bar.page;
	_h_tween = _reset_tween(_h_tween, content_max, _h_scroll_bar);
	
func _update_vertical_metrics() -> void:
	var content_max := _v_scroll_bar.max_value - _v_scroll_bar.page;
	_v_tween = _reset_tween(_v_tween, content_max, _v_scroll_bar);

func _reset_tween(tween:Tween, content_max:float, scroll_bar:ScrollBar) -> Tween:
	if(tween != null): tween.kill();
	scroll_bar.value = 0;
	var scroll_duration := (scroll_bar.max_value - scroll_bar.page) / scroll_speed;
	var newTween = create_tween();
	newTween.tween_property(scroll_bar, "value", content_max, scroll_duration).set_delay(stay_seconds);
	newTween.tween_property(scroll_bar, "value", 0, scroll_duration).set_delay(stay_seconds);
	newTween.set_loops();
	return newTween;
