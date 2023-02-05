Shader "Custom/MaskInvert"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)

        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
    
    Category 
    {
        Subshader
        {
            Tags { "Queue"="Transparent+1"}

            Pass
            {
                ZWrite On
                ZTest Greater
                Lighting On
                SetTexture [_MainTex] {}
            }
        }
    }

    Fallback "Specular", 1
}
