texture2D scene : RENDERCOLORTARGET;

sampler2D sceneSampler = sampler_state 
{
	  Texture = <scene>;
};


float4 mainPS( float2 texCoord : TEXCOORD) : COLOR 
{
	
    float4 col = tex2D(sceneSampler, texCoord);
    float Intensity;

    // ¬ычисл€ем интенсивность черно-белого пиксел€ по формуле
    // I = 0.299*R + 0.587*G + 0.184*B


       Intensity = 0.5*col.r + 0.5*col.g + 0.5*col.b;
	

    // ќбратите внимание, что можно было использовать и скал€рное произведение
    // Intensity = dot(col,float4(0.299,0.587,0.184,0));

    // ¬озвращаем интенсивность в итоговом цвете
    return float4(Intensity.xxx,col.a);
}

technique technique0 
{
	pass p0 
	{
		PixelShader = compile ps_2_0 mainPS();
	}
}
