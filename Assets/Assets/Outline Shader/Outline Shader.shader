Shader "Custom/OutlineShader"
{
    Properties
    {
        _OutlineWidth ("Outline Width", Range(0.0001, 0.1)) = 0.02
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Front // Renders only backfaces to create the outline effect

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            fixed4 _OutlineColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                
                // Push the vertices outward along their normals to create the outline
                v.vertex.xyz += v.normal * _OutlineWidth;
                
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor; // Solid outline color
            }
            ENDCG
        }
    }
}
