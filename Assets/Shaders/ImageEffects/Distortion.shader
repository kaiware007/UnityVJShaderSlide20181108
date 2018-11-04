Shader "Hidden/Distortion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DistortionNoiseScale("Noise Scale", Range(0,10)) = 0.1
		_DistortionNoisePosition("Noise Position", vector) = (0,0,0,0)
		_DistortionPower ("Power", Range(0,10)) = 0.1
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
			#include "Assets/Shaders/Libs/SimplexNoiseGrad3D.cginc"

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

			float _DistortionNoiseScale;
			float3 _DistortionNoisePosition;
			float _DistortionPower;

			float2 getRotationUV(float2 uv, float angle, float power) {
				float2 v = (float2)0;
				float rad = angle * 3.14159265359;
				
				v.x = uv.x + cos(rad) * power;
				v.y = uv.y + sin(rad) * power;

				return v;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float3 uv1 = float3(i.uv * _DistortionNoiseScale,0);
				float3 noise = snoise_grad(uv1 + _DistortionNoisePosition);
				
				float2 uv = getRotationUV(i.uv, noise.x, noise.y * _DistortionPower);
				fixed4 col = tex2D(_MainTex, uv);
				
				return col;
			}
			ENDCG
		}
	}
}
