Shader "Hidden/RadiationBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4  _MainTex_TexelSize;
			float2 _BlurCenter;
			float _BlurPower;

			fixed4 frag (v2f i) : SV_Target
			{

				float2 dir = _BlurCenter - i.uv;
				float distance = length(dir);
				dir = normalize(dir) * _MainTex_TexelSize.xy;
				dir *= _BlurPower * distance;

				fixed4 col = tex2D(_MainTex, i.uv) * 0.19;
				for (int j = 1; j < 10; j++) {
					col += tex2D(_MainTex, i.uv + dir * j) * (0.19 - j * 0.02);
				}

				return col;
			}
			ENDCG
		}
	}
}
