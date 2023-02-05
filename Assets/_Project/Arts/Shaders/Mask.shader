Shader "Custom/Mask"
{
  Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
  SubShader
  {
	 Tags {"Queue" = "Transparent+1"}	 

  Pass
     {
		 Blend Zero One 
     }
  }

}
