Shader "Unlit/PreviewShader"
{
    Properties
    {
        _ColorATex("_ColorATex", 2D) = "white" {}
        _ColorBTex("_ColorATex", 2D) = "white" {}
        _ColorBlackTex("_ColorATex", 2D) = "white" {}
        _ColorA("_ColorA", Color) = (1,1,1,1)
        _ColorB("_ColorB", Color) = (1,1,1,1)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _ColorATex;
            sampler2D _ColorBTex;
            sampler2D _ColorBlackTex;
            float4 _ColorA;
            float4 _ColorB;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 col = lerp(1.0,_ColorA, 1.-tex2D(_ColorATex, i.uv).x);
            col *= lerp(1.0, _ColorB, 1.-tex2D(_ColorBTex, i.uv).x);
            col *= lerp(1.0, 0., 1.-tex2D(_ColorBlackTex, i.uv).x);

                return fixed4(col,1.0);
            }
            ENDCG
        }
    }
}
