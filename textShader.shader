Shader TextMeshProDistance Field AlwaysOnTop
{
    Properties
    {
        _FaceColor     (Face Color, Color) = (1,1,1,1)
        _FaceDilate    (Face Dilate, Range(-1,1)) = 0
        _MainTex       (Font Atlas, 2D) = white {}
        _OutlineColor  (Outline Color, Color) = (0,0,0,1)
        _OutlineWidth  (Outline Width, Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { Queue=Overlay IgnoreProjector=True RenderType=Transparent }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always        👈 ALWAYS ON TOP
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include UnityCG.cginc

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _FaceColor;
            float4 _OutlineColor;
            float _OutlineWidth;
            float _FaceDilate;

            struct appdata
            {
                float4 vertex  POSITION;
                float4 color   COLOR;
                float2 texcoord  TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex  SV_POSITION;
                float4 color   COLOR;
                float2 uv  TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color;
                return o;
            }

            float4 frag (v2f i)  SV_Target
            {
                float sdf = tex2D(_MainTex, i.uv).a;

                 SDF threshold
                float threshold = 0.5 + _FaceDilate;

                 Outline
                float outline = smoothstep(threshold - _OutlineWidth, threshold, sdf);

                 Face
                float face = smoothstep(threshold, threshold + 0.02, sdf);

                float4 col = lerp(_OutlineColor, _FaceColor, face);

                col = i.color;  Tint by TMP vertex color (e.g. from the inspector)
                return col;
            }
            ENDCG
        }
    }
}
