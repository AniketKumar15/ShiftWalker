Shader "Custom/GlitchPostEffect"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _GlitchAmount ("Glitch Amount", Range(0,1)) = 0.1
        _Speed ("Speed", Range(0,10)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _GlitchAmount;
            float _Speed;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float glitch = sin(_Time.y * _Speed + i.uv.y * 100.0) * _GlitchAmount;
                float2 uv = i.uv + float2(glitch, 0);
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
