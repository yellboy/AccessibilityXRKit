Shader "Custom/FrostedGlassOverlay"
{
    Properties
    {
        _MainTex ("Base (Screen)", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 4
        _TintColor ("Tint Color", Color) = (1,1,1,0.2)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _BlurSize;
            float4 _TintColor;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;
                float4 col = float4(0,0,0,0);

                // simple 3x3 box blur
                float offset = _BlurSize / 512; // tweak according to resolution
                [unroll]
                for (int x=-1; x<=1; x++)
                    for (int y=-1; y<=1; y++)
                        col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(x,y)*offset);

                col /= 9.0;
                col.rgb = lerp(col.rgb, _TintColor.rgb, _TintColor.a); // apply tint
                col.a = _TintColor.a;

                return col;
            }
            ENDHLSL
        }
    }
}
