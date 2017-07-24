Shader "Hidden/CameraGraysclae"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Fade("Fade",float)=0
		_NightMode("Night Mode", float)= 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			    float rand(float2 co)
                {
                    return frac((sin( dot(co.xy , float2(12.345 * _Time.w, 67.890 * _Time.w) )) * 12345.67890+_Time.w));
                }

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Fade;
			float _NightMode;
			

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// noise vqariable
				float lightNoise = (rand(i.uv)*0.4+0.8);
				// greyscale part
				float grey;
				grey = (col.r + col.g + col.b)/3;
				col.rgb = lerp(col.rgb,float3(grey,grey,grey), _Fade);
				// Night Vision part
				if(_NightMode>0)
				{
					col.rgb = grey * float3(0,1,0) * 2*lightNoise;
				}
				//col = 1 - col;
				
				return col;
			}
			ENDCG
		}
	}
}
