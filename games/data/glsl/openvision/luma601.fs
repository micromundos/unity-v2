#version 120
#extension GL_ARB_texture_rectangle : enable
#extension GL_EXT_gpu_shader4 : enable

uniform sampler2DRect tex;
const vec3 prim = vec3(0.299, 0.587, 0.114);

void main()
{
	vec4 sample = texture2DRect(tex, gl_TexCoord[0].st);
	float luma = dot(sample.rgb, prim);
	gl_FragColor = vec4(luma, luma, luma, sample.a);
}
