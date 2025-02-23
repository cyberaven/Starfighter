﻿Shader "Unlit/StarField"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Iterations("Inner Iterations", Range(1, 20)) = 13
        _Coefficient("Coefficient", Range(0, 1)) = 0.53
        _VolumeSteps("Volume Steps", Range(1, 20)) = 15
        _StepSize("Step Size", Range(0.01, 0.3)) = 0.1
        _Zoom("Zoom", Range(0.1, 10)) = 1.8
		
		_Tile("Tile", Range(0.2, 5)) = 0.85
        _Speed("Speed", Range(0.0, 1.0)) = 0.05
        _DirectionX("DirectionX", Range(-1, 1)) = 0
        _DirectionY("DirectionY", Range(-1, 1)) = 0
		
		_Brightness("Brightness", Range(0.0, 0.01)) = 0.0015
		_DistFading("Fading", Range(0.1, 1)) = 0.73
		_Darkness("Darkness", Range(0.0, 1)) = 0.3
        _Saturation("Saturation", Range(0.1, 2)) = 0.85
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Iterations;
            float _Coefficient;
            float _VolumeSteps;
            float _StepSize;
     
            float _Zoom;
            float _Tile;
            float _Speed;
            float _DirectionX;
            float _DirectionY;
            
            float _Brightness;
            float _Darkness;
            float _DistFading;
            float _Saturation;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.y *= _ScreenParams.y / _ScreenParams.x;
                return o;
            }
            
            static float _sin30 = -0.99;
            static float _cos30 = 0.15;
            static float _sin45 = 0.85;
            static float _cos45 = 0.52;
            static float2x2 _rotation1 = float2x2(_cos30, _sin30, -_sin30, _cos30);
            static float2x2 _rotation2 = float2x2(_cos45, _sin45, -_sin45, _cos45);

            float3 rotate(float3 p)
            {
                p.xz = mul(_rotation1, p.xz);
                p.xy = mul(_rotation2, p.xy);
                return p;
            }
            
            float3 colorByDistance(float distance)
            {
                return float3(distance, distance * distance, 
                    distance * distance * distance * distance);
            }

            float mod(float x, float y)
            {
                return x - y * floor(x / y);
            }
            
            float3 mod3(float3 x, float3 y)
            {
                return x - y * floor(x / y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 pos = float3(i.uv * _Zoom, 1);
                pos = rotate(pos);
                float time = mod(_Time.y * _Speed, 7200);
                float3 dir = float3(time * _DirectionX, time * _DirectionY, 0);
                dir = rotate(dir);
                float3 color = 0;
                float distance = 0.1;
                float fade = 1.0;
                
                for(int s = 0; s < _VolumeSteps; s++)
                {
                    float3 p = pos * distance + dir;
                    p = abs(_Tile - mod3(p, _Tile * 2.0));
                
                    float a = 0, prev_a = 0;
                    for(int i = 0; i < _Iterations; i++)
                    {
                        p = abs(p) / dot(p, p) - _Coefficient;
                        a += abs(length(p) - prev_a);
                        prev_a = length(p);
                    }
                    if(s > 6) fade *= 1.0 - max(0.0, (_Darkness - a * 0.05));
                    a = pow(a, 3);
                    color += colorByDistance(distance) * a * _Brightness * fade;
                    distance += _StepSize;
                    fade *= _DistFading;
                }
                float3 greyScale = length(color);
                color = lerp(greyScale, color, _Saturation);
                color *= 0.01;
                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}