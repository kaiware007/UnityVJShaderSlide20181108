Shader "Hidden/Glitch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "black" {}
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
			sampler2D _NoiseTex;
			float _Intensity;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 noise = tex2D(_NoiseTex, i.uv);
				
				fixed thresh = 1.001 - _Intensity * 1.001;
				
				fixed slide = step(thresh, pow(noise.b, 2.5));
				fixed ref_a = step(thresh, pow(noise.a, 2.5));
				fixed ref_g = step(thresh, pow(noise.r, 2.5));
				fixed ref_b = step(thresh, pow(noise.g, 2.5));
				fixed d = step(thresh, pow(noise.b, 3.5));

				fixed2 uv_slide = (i.uv + noise.xy * slide) % 1;
				fixed2 uv_ref_r = (i.uv + noise.xy * ref_a) % 1;
				fixed2 uv_ref_g = (i.uv + noise.xy * ref_g) % 1;
				fixed2 uv_ref_b = (i.uv + noise.xy * ref_b) % 1;

				fixed3 col1 = tex2D(_MainTex, uv_slide);
				fixed3 col2 = fixed3(tex2D(_MainTex, uv_ref_r).r, tex2D(_MainTex, uv_ref_g).g, tex2D(_MainTex, uv_ref_b).b);

				return fixed4(lerp(col1, col2, d), 1);
			}
			ENDCG
		}
	}
}
