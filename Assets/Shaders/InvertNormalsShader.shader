Shader "Custom/InvertNormals"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Cull Front // Invert normals by culling the front faces
            Lighting Off
            ZWrite On
            SetTexture [_MainTex] { combine texture }
        }
    }
}
