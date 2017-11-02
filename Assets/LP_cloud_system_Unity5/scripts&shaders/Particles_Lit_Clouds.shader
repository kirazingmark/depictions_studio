Shader "LowPoly/SimpleLit_Clouds"
{
Properties {
	_TintColor ("Tint Color", Color) = (.5,.5,.5,.5)
	_Emission("Emission", Range(0,1)) = 0
}
SubShader {
	Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
	LOD 200
	Cull Off
CGPROGRAM
#pragma surface surf WrapLambert
#pragma vertex vert
fixed4 _TintColor;
fixed _Emission;
struct Input {
	
	float4 color;
};

 void vert (inout appdata_full v, out Input o) {
   UNITY_INITIALIZE_OUTPUT(Input,o);
   o.color = v.color*_TintColor;
 }



    half4 LightingWrapLambert (SurfaceOutput s, half3 lightDir, half atten) {
        half NdotL = dot (s.Normal, lightDir);
        half NdotL_abs = abs(dot (s.Normal, lightDir));
        half atten_new = lerp(.25,1,atten);
        half diff = (NdotL * 0.5 + 0.5) ;
        half4 c;
        c.rgb = s.Albedo * _LightColor0.rgb * (diff *atten_new);
        c.a = s.Alpha;
        return c;
    }

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c =  float4(IN.color.r,IN.color.g,IN.color.b,IN.color.a);//_TintColor;
	o.Albedo = c.rgb*_Emission;
	o.Alpha = 1;
	o.Emission =c.rgb*c.a*(1+_Emission);
}
ENDCG
}

Fallback "Legacy Shaders/VertexLit"
}