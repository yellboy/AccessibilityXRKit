Shader "Custom/TunnelVignette"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,0.85)
        _Center("Center", Vector) = (0.5,0.5,0,0)
        _Radius("Radius", Float) = 0.35
        _Softness("Softness", Float) = 0.15
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

                HLSLPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 _Color;
            float2 _Center;
            float _Radius;
            float _Softness;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float d = distance(uv, _Center);
                // smooth transition from center to edge
                float t = saturate((d - _Radius) / max(0.0001, _Softness));
                float alpha = lerp(0.0, _Color.a, t);
                return fixed4(_Color.rgb, alpha);
            }
            ENDHLSL
        }
    }
    FallBack Off
}