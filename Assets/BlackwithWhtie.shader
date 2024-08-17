Shader "Custom/BlackWhiteShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 1, 1, 1)
        _Color1 ("Color 1", Color) = (1, 1, 1, 1) // 白色
        _Color2 ("Color 2", Color) = (0, 0, 0, 1) // 黑色
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

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _Color;
            fixed4 _Color1;
            fixed4 _Color2;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 根据UV坐标，计算x轴的一半
                if (i.uv.x > 0.0)
                    return _Color1; // 右边部分显示白色
                else
                    return _Color2; // 左边部分显示黑色
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
