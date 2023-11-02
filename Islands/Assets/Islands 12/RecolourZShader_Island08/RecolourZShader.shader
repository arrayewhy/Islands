Shader "Custom/SpriteShaderWithDepth" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _GrayColor ("Gray Color", Color) = (0.5, 0.5, 0.5, 1)
        _Depth ("Depth", Range(0, 1)) = 0.5
        _DepthRange ("Depth Range", Range(0, 1)) = 0.1
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Pass {
            Cull Off
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };
 
            sampler2D _MainTex;
            float _Depth;
            float _DepthRange;
            float4 _GrayColor;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                o.color.a *= (1 - _DepthRange * (o.vertex.z / o.vertex.w - _Depth));
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                if (i.color.a == 0)
                    discard;
                if (col.a == 0)
                    return _GrayColor;
                col.a *= i.color.a;
                col.rgb *= i.color.rgb;
                return col;
            }
            ENDCG
        }
    }
}
