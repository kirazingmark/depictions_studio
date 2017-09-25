// Copyright 2017 Integrity Software and Games, LLC.

Shader "ModularCartoon/ColorDiffuse" 
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
	}

    SubShader 
    {
      Tags { "RenderType" = "Opaque" }

      CGPROGRAM
      #pragma surface surf Lambert

      fixed4 _Color;

      struct Input 
      {
          float4 color : COLOR;
      };

      void surf (Input IN, inout SurfaceOutput o) 
      {
          o.Albedo = _Color.rgb;
      }
      ENDCG
    }

    Fallback "Diffuse"

}
