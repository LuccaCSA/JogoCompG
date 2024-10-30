Shader "Custom/Contorno"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineThickness ("Outline Thickness", Range (.002, 0.03)) = .005
    }
    SubShader
    {
        Tags {"Queue"="Overlay"} // Isso força o contorno a ser desenhado por último

        Pass
        {
            Name "OUTLINE"
            Cull Front
            ZWrite On // Mantém a gravação no depth buffer
            ZTest LEqual // Passa o teste de profundidade

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            fixed4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata v)
            {
                // Expande o modelo para fora com base nas normais
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                float3 norm = mul((float3x3) UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(norm.xy) * _OutlineThickness;
                o.pos.xy += offset * o.pos.w;
                o.color = _OutlineColor;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
}