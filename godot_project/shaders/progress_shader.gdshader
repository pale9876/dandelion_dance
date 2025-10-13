shader_type canvas_item;

uniform bool repeat = false;

// naming in UV system _x_y
uniform vec2 offset_0_0 = vec2(0.0, 0.0);
uniform vec2 offset_0_1 = vec2(0.0, 0.0);
uniform vec2 offset_1_0 = vec2(0.0, 0.0);
uniform vec2 offset_1_1 = vec2(0.0, 0.0);

uniform vec4 first_color: source_color = vec4(1.0);
uniform vec4 second_color: source_color = vec4(1.0);
uniform float position : hint_range(-2, 2) = 0.5;
uniform int stepIntensify : hint_range(1, 50) = 5;

uniform float time_scale = 1.0;

vec2 sample_point(vec2 point) {
	// Corners for UV.y == 0
	vec2 p_0_0 = vec2(0,0) - offset_0_0;
	vec2 p_1_0 = vec2(1,0) - offset_1_0;

	vec2 d_x_0 = p_0_0 + (p_1_0 - p_0_0) * point.x;

	vec2 p_0_1 = vec2(0, 1) - offset_0_1;
	vec2 p_1_1 = vec2(1, 1) - offset_1_1;

	vec2 d_x_1 = p_0_1 + (p_1_1 - p_0_1) * point.x;

	return d_x_0 + (d_x_1 - d_x_0) * point.y;
}

void fragment() {
	float pos = round(
		sin(smoothstep(0., .5, (position + UV.x) / float(stepIntensify)  / 2.0) + (TIME * time_scale)) * 3.
	);
	vec4 color = mix(first_color, second_color, pos / float(stepIntensify));

	vec4 current_color = COLOR * color;

	vec4 _texture = texture(TEXTURE, UV) * color;
	COLOR = _texture;

}


void vertex() {
	VERTEX += sample_point(UV);
}
