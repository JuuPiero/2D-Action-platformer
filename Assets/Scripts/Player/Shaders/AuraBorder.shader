Shader "Custom/AuraBorder"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Color Change Speed", Range(0, 5)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off  
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            float _Speed;

            // Chuyển Hue thành RGB
            fixed3 HueToRGB(float h)
            {
                float r = abs(h * 6 - 3) - 1;
                float g = 2 - abs(h * 6 - 2);
                float b = 2 - abs(h * 6 - 4);
                return saturate(fixed3(r, g, b));
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                float alpha = color.a;

                // Tạo màu thay đổi theo thời gian
                float hue = frac(_Time.y * _Speed);
                fixed3 newColor = HueToRGB(hue);

                return fixed4(newColor, alpha); // Chỉ thay đổi màu, giữ nguyên độ trong suốt
            }
            ENDCG
        }
    }
}