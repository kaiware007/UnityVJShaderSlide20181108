Shader "Hidden/RGBShift"
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
			float2 _ShiftPower;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 shiftpow = _ShiftPower * (1.0 / _ScreenParams.x);
				fixed r = tex2D(_MainTex, i.uv - shiftpow).r;
				fixed2 ga = tex2D(_MainTex, i.uv).ga;
				fixed b = tex2D(_MainTex, i.uv + shiftpow).b;

				return fixed4(r, ga.x, b, ga.y);
			}
			ENDCG
		}
	}
}
