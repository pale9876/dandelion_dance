shader_type canvas_item;

uniform sampler2D noise: repeat_enable;

uniform vec4 line_color_a: source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec4 line_color_b: source_color = vec4(0.0, 1.0, 1.0, 1.0);
uniform float line_threshold = 0.6;
uniform float inverse_speed = 10.0;
uniform float line_length = 1000.0;
uniform float angle: hint_range(0.0, 360.0) = 0.0;

uniform sampler2D screen_texture: hint_screen_texture, filter_nearest;

void fragment() {
	vec2 uv = vec2(
		SCREEN_UV.x * cos(radians(angle)) - SCREEN_UV.y * sin(radians(angle)),
		SCREEN_UV.x * sin(radians(angle)) + SCREEN_UV.y * cos(radians(angle))
	);

	vec4 noise_line = texture(
		noise,
		vec2(uv.x / line_length + TIME / inverse_speed, uv.y)
	);

	vec4 color;

	vec4 c = textureLod(screen_texture, uv, 0.);

	if (noise_line.r < line_threshold){
		color = vec4(0.);
	} else {
		color = mix(c, line_color_b, 1.0 - noise_line.r);
	}

	COLOR = color;
}