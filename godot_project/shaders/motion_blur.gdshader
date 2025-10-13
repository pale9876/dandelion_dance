shader_type canvas_item;

uniform float alpha: hint_range(0.0, 1.0, 0.001) = 0.0;
uniform float blur = 0.0;
uniform sampler2D frame_buffer;

void fragment() {
	COLOR = texture(frame_buffer, SCREEN_UV, blur);
	COLOR.a = alpha;
}