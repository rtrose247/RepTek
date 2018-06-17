#version 330

in vec2 UV;
out vec3 outputColor;
uniform sampler2D texUnit;

void
main()
{
	outputColor = texture(texUnit, UV).rgb;
}