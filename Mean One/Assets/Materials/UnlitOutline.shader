Shader "Custom/UnlitOutline"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
        _MainTex("Main Texture", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "Always" }
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
            };
 
            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            float4 _OutlineColor;
            float _OutlineWidth;
            sampler2D _MainTex;
 
            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy; // You may need to adjust this UV calculation depending on your mesh.
                return o;
            }
 
            half4 frag(v2f i) : SV_Target
            {
                half4 c = half4(0, 0, 0, 0);
                half depth = LinearEyeDepth(i.pos.z);
 
                for (half angle = 0; angle < 360; angle += 45)
                {
                    half2 offset = _OutlineWidth * half2(cos(radians(angle)), sin(radians(angle)));
                    half2 uv = i.uv + offset * depth;
 
                    half4 sample = tex2D(_MainTex, uv);
                    c += sample;
                }
 
                return c * _OutlineColor;
            }
 
            ENDCG
        }
 
        Pass
        {
            Name "Base"
            Tags { "LightMode" = "ForwardBase" }
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata_t
            {
                float4 vertex : POSITION;
            };
 
            struct v2f
            {
                float4 pos : POSITION;
            };
 
            float4 _OutlineColor;
            float _OutlineWidth;
 
            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
 
            half4 frag(v2f i) : SV_Target
            {
                return half4(1, 1, 1, 1);
            }
 
            ENDCG
        }
    }
}