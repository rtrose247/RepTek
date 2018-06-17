#version 330

in vec3 vPosition;
in vec2 vTexturePoint;

out vec2 UV;
uniform mat4 modelview;

void
main()
{
    gl_Position = modelview * vec4(vPosition, 1.0);
 
    //color = vec4( vColor, 1.0);
	UV = vTexturePoint;
}