Shader "Hidden/EdgeDetection"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass // コンポジット(pass1)
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

			sampler2D _MainTex;
			sampler2D _EdgeTex;
			float _EdgePower;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv;
				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 edge = tex2D(_EdgeTex, i.uv);

			return edge * _EdgePower;
		}
		ENDCG
	}

		// Sobel Filter
		Pass	// 1
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4  _MainTex_TexelSize;

			float _HCoef[9];
			float _VCoef[9];

			float _Threshold;
			float _Blend;
			float4 _BackColor;
			float4 _EdgeColor;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				fixed3 hcol = fixed3(0, 0, 0);
				fixed3 vcol = fixed3(0, 0, 0);

				hcol += tex2D(_MainTex, i.uv + fixed2(-1, -1) * _MainTex_TexelSize.xy).rgb * _HCoef[0];
				hcol += tex2D(_MainTex, i.uv + fixed2(0, -1) * _MainTex_TexelSize.xy).rgb * _HCoef[1];
				hcol += tex2D(_MainTex, i.uv + fixed2(1, -1) * _MainTex_TexelSize.xy).rgb * _HCoef[2];
				hcol += tex2D(_MainTex, i.uv + fixed2(-1,  0) * _MainTex_TexelSize.xy).rgb * _HCoef[3];
				hcol += tex2D(_MainTex, i.uv + fixed2(0,  0) * _MainTex_TexelSize.xy).rgb * _HCoef[4];
				hcol += tex2D(_MainTex, i.uv + fixed2(1,  0) * _MainTex_TexelSize.xy).rgb * _HCoef[5];
				hcol += tex2D(_MainTex, i.uv + fixed2(-1,  1) * _MainTex_TexelSize.xy).rgb * _HCoef[6];
				hcol += tex2D(_MainTex, i.uv + fixed2(0,  1) * _MainTex_TexelSize.xy).rgb * _HCoef[7];
				hcol += tex2D(_MainTex, i.uv + fixed2(1,  1) * _MainTex_TexelSize.xy).rgb * _HCoef[8];

				vcol += tex2D(_MainTex, i.uv + fixed2(-1, -1) * _MainTex_TexelSize.xy).rgb * _VCoef[0];
				vcol += tex2D(_MainTex, i.uv + fixed2(0, -1) * _MainTex_TexelSize.xy).rgb * _VCoef[1];
				vcol += tex2D(_MainTex, i.uv + fixed2(1, -1) * _MainTex_TexelSize.xy).rgb * _VCoef[2];
				vcol += tex2D(_MainTex, i.uv + fixed2(-1,  0) * _MainTex_TexelSize.xy).rgb * _VCoef[3];
				vcol += tex2D(_MainTex, i.uv + fixed2(0,  0) * _MainTex_TexelSize.xy).rgb * _VCoef[4];
				vcol += tex2D(_MainTex, i.uv + fixed2(1,  0) * _MainTex_TexelSize.xy).rgb * _VCoef[5];
				vcol += tex2D(_MainTex, i.uv + fixed2(-1,  1) * _MainTex_TexelSize.xy).rgb * _VCoef[6];
				vcol += tex2D(_MainTex, i.uv + fixed2(0,  1) * _MainTex_TexelSize.xy).rgb * _VCoef[7];
				vcol += tex2D(_MainTex, i.uv + fixed2(1,  1) * _MainTex_TexelSize.xy).rgb * _VCoef[8];

				float d = sqrt(hcol * hcol + vcol * vcol);
				fixed4 edge = lerp(_BackColor, _EdgeColor, step(_Threshold, d));

				return lerp(edge, col, _Blend);
			}
			ENDCG
		}

			// Laplacian Filter
			Pass	// 2
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

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				float4  _MainTex_TexelSize;

				float _Coef[9];
				float _Blend;
				float4 _BackColor;
				float4 _EdgeColor;
				float _Threshold;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);

					fixed3 fcol = fixed3(0, 0, 0);

					fcol += tex2D(_MainTex, i.uv + fixed2(-1, -1) * _MainTex_TexelSize.xy).rgb * _Coef[0];
					fcol += tex2D(_MainTex, i.uv + fixed2(0, -1) * _MainTex_TexelSize.xy).rgb * _Coef[1];
					fcol += tex2D(_MainTex, i.uv + fixed2(1, -1) * _MainTex_TexelSize.xy).rgb * _Coef[2];
					fcol += tex2D(_MainTex, i.uv + fixed2(-1,  0) * _MainTex_TexelSize.xy).rgb * _Coef[3];
					fcol += tex2D(_MainTex, i.uv + fixed2(0,  0) * _MainTex_TexelSize.xy).rgb * _Coef[4];
					fcol += tex2D(_MainTex, i.uv + fixed2(1,  0) * _MainTex_TexelSize.xy).rgb * _Coef[5];
					fcol += tex2D(_MainTex, i.uv + fixed2(-1,  1) * _MainTex_TexelSize.xy).rgb * _Coef[6];
					fcol += tex2D(_MainTex, i.uv + fixed2(0,  1) * _MainTex_TexelSize.xy).rgb * _Coef[7];
					fcol += tex2D(_MainTex, i.uv + fixed2(1,  1) * _MainTex_TexelSize.xy).rgb * _Coef[8];

					fixed4 edge = lerp(_BackColor, _EdgeColor, step(_Threshold, length(fcol)));

					return lerp(edge, col, _Blend);
				}
					ENDCG
			}

				// Depth type
				Pass	// 3
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

					v2f vert(appdata v)
					{
						v2f o;
						o.vertex = UnityObjectToClipPos(v.vertex);
						o.uv = v.uv;
						return o;
					}

					sampler2D _MainTex;
					float4  _MainTex_TexelSize;
					float4 _BackColor;
					float4 _EdgeColor;
					float _Threshold;

					UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

					float _DepthThreshold;
					float _Blend;

					fixed detectEdge(float2 uv)
					{
						float4 duv = float4(0, 0, _MainTex_TexelSize.xy);

						float d11 = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv + duv.xy);
						float d12 = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv + duv.zy);
						float d21 = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv + duv.xw);
						float d22 = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv + duv.zw);

						float g_d = length(float2(d11 - d22, d12 - d21));
						g_d = saturate((g_d - _DepthThreshold) * 40);

						return g_d;
					}

					fixed4 frag(v2f i) : SV_Target
					{
						fixed4 col = tex2D(_MainTex, i.uv);

						fixed edge = detectEdge(i.uv);

						fixed4 col2 = lerp(_BackColor, _EdgeColor, step(_Threshold, edge));

						return lerp(col2, col, _Blend);
					}
					ENDCG
				}
	}
}
