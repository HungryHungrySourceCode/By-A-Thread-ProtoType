Shader "Custom/TestingShader" {
Properties{
	_Length ("Length", Range(0, 20)) = 1
	_Width ("Width", Range(0, 20)) = 1
	_Gravity("Gravity", Range(0, 20)) = 1
	_Steps("Steps", int) = 1
}
    SubShader {
      Tags {"Queue" = "Geometry"}
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float4 color : COLOR;
      };

	  struct geomInput
{
    float4 pos : POSITION;
    float4 nor : NORMAL;
};
    struct vs_out {
        float4 pos : SV_POSITION;
    };
	

      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = 1;
      }
      ENDCG
    }
}