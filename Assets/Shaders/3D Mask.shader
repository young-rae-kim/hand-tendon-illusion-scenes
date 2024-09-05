Shader "Custom/3D Mask"
{
    SubShader
    {
        Tags { "Queue" = "Transparent+10" }
        
        Pass
        {
            Blend Zero One
        }
    }
}
