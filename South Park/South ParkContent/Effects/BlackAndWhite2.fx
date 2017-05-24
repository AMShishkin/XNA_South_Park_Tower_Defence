
float CenterX : register(C0);
 

float CenterY : register(C1);
 

float BlurAmount : register(C2);
 
sampler2D input : register(s0);
 
float4 main(float2 uv : TEXCOORD) : COLOR
 
{
    float4 c = 0;
    uv.x -= CenterX;
    uv.y -= CenterY;
 
    float distanceFactor = pow(pow(uv.x,2) + pow(uv.y, 2),2);
 
    for(int i=0; i < 15; i++)
    {
        float scale = 1.0 - distanceFactor * BlurAmount * (i / 30.0);
        float2 coord = uv * scale;
        coord.x += CenterX;
        coord.y += CenterY;
        c += tex2D(input,coord);
    }
 
    c /= 15;
    return c;
}

   technique technique0 
{
	pass p0 
	{
		PixelShader = compile ps_2_0 main();
	}
}
