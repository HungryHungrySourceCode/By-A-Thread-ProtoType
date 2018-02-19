Shader "MyShaders/FlatColor"{
Properties {
				_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		   }
SubShader {
	pass {
		CGPROGRAM
		#Pragma vertex vert
		#Pragma fragment frag

	 	uniform float4 _Color;

		struct vertexInput{
			float4 vertex : POSITION;
		};
		struct vertexOutput{
			float4 pos : SV_POSITION;
		};

		vertexOutput vert(vertexInput v)
		{
			vertexOutput o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}

		float4 frag(vertexOutput i) : Color
		{
			return _Color;
		}
		ENDCG
		}
	}
}