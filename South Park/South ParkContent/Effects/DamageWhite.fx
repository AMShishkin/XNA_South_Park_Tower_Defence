texture2D scene : RENDERCOLORTARGET;

sampler2D sceneSampler = sampler_state 
{
	  Texture = <scene>;
};

float4 mainPS( float2 texCoord : TEXCOORD) : COLOR 
{
    float4 col = tex2D(sceneSampler, texCoord);
    float Intensity;

    Intensity = 0.5*col.r + 0.5*col.g + 0.5*col.b;

    return float4(Intensity.xxx,col.a);
}

technique technique0 
{
	pass p0 
	{
		PixelShader = compile ps_2_0 mainPS();
	}
}

