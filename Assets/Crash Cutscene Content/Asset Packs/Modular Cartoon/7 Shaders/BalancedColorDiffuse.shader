// Copyright 2017 Integrity Software and Games, LLC.

Shader "ModularCartoon/BalancedColorDiffuse"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_Brightness ("Brightness", Range(-1.0,1.0)) = 0.0
	}

    SubShader 
    {
      Tags { "RenderType" = "Opaque" }

      CGPROGRAM
      #pragma surface surf Lambert

      fixed4 _Color;
      fixed _Brightness;

      struct Input 
      {
          float4 color : COLOR;
      };

      void surf (Input IN, inout SurfaceOutput o) 
      {
          o.Albedo = _Color.rgb + _Brightness;
      }
      ENDCG
    }

    Fallback "Diffuse"

}
