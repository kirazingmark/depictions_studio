// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Copyright 2017 Integrity Software and Games, LLC.

Shader "ModularCartoon/ColorVertexLit" 
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
	}

	SubShader 
	{
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Color;

			//#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 col : COLOR0;
			};

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);

				float4x4 modelMatrixInverse = unity_WorldToObject;
				float3 normalDirection = normalize(float3(mul(float4(v.normal, 0.0), modelMatrixInverse).xyz));
				float3 lightDirection = normalize(float3(_WorldSpaceLightPos0.xyz));
				float3 diffuseReflection = float3(_LightColor0.xyz) * max(0.0, dot(normalDirection, lightDirection));

				o.col = float4(diffuseReflection, 1.0) + UNITY_LIGHTMODEL_AMBIENT;

				o.col = o.col * _Color;

				return o;

			}

			fixed4 frag(v2f i) : COLOR0
			{
				return i.col;
			}

			ENDCG
		}
	}
}
