// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Unlit/AlwaysOnTop"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Alpha (A)", 2D) = "white" {}
        
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        LOD 200

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType"="Plane"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Offset -1, -1
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_instancing
                #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO

                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    half4 color : COLOR;
                    float2 texcoord : TEXCOORD0;

                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    half4 color : COLOR;
                    float2 texcoord : TEXCOORD0;

                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _Color;

                v2f vert (appdata_t v)
                {
                    UNITY_SETUP_INSTANCE_ID(v);

                    v2f o;
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.color = v.color;

#ifdef UNITY_HALF_TEXEL_OFFSET
                    o.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1, 1);
#endif

                    return o;
                }

                half4 frag (v2f i) : COLOR
                {
                    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                    half4 col = i.color;
                    col.a *= tex2D(_MainTex, i.texcoord).a;
                    col = col * _Color;
                    clip(col.a - 0.01);
                    return col;
                }
            ENDCG
        }
    }
}
