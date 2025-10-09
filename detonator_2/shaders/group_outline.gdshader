shader_type canvas_item;
render_mode unshaded;

uniform sampler2D screen_texture: hint_screen_texture, repeat_disable, filter_linear;
uniform vec4 line_colour: source_color = vec4(0.,0.,0.,1.);
uniform float line_thickness: hint_range(0, 10) = 1.;

uniform bool draw = false;

const vec2 OFFSETS[8] = {
	vec2(-1, 0), vec2(0, -1), vec2(0, 1), vec2(1, 0),
	vec2(-1, -1), vec2(1, 1), vec2(1, -1), vec2(-1, 1)
};

void fragment() {
	
	vec2 sz = SCREEN_PIXEL_SIZE * line_thickness;
	float outline = 0.;
	
	for(int i = 0; i< OFFSETS.length(); i+=1)
	{
		outline += texture(screen_texture, SCREEN_UV + sz * OFFSETS[i]).a;
	}
	
	outline = min(outline, 1.);
	
	vec4 c = textureLod(screen_texture, SCREEN_UV, 0.);
	
	if (c.a > .0001)
	{
		c.rgb /= c.a;
	}
	
	COLOR = (draw) ? mix(c, line_colour, outline - c.a) : textureLod(screen_texture, SCREEN_UV, 0.);
}

