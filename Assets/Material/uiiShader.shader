Shader "UI/AlwaysOnTopPassThrough"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Overlay"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
        }

        // Always draw on top
        ZWrite Off
        ZTest Always

        // Standard UI blending
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color; // UI uses this for alpha fading
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // get texture exactly as it is
                fixed4 c = tex2D(_MainTex, i.uv);

                // multiply by UI vertex alpha (UI fades)
                return c * i.color.a;
            }
            ENDCG
        }
    }
}
