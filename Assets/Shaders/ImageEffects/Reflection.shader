Shader "Hidden/Reflection"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Horizontal ("Horizontal Flip", int) = 0
		_Vertical ("Vertical Flip", int) = 0
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
			int _Horizontal;
			int _Vertical;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv;
				uv.x = ((_Horizontal && i.uv.x > 0.5) ? 1 - i.uv.x : i.uv.x);
				uv.y = ((_Vertical && i.uv.y > 0.5) ? 1 - i.uv.y : i.uv.y);

				fixed4 col = tex2D(_MainTex, uv);
				return col;
			}
			ENDCG
		}
	}
}
