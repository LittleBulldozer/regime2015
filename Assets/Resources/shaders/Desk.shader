// CG Shader Program이 Cross Compile될 시 OpenGL ES 3.0미만 버전에서 gl_FragCoord가 missing되는 이슈가 있어서
// 컴파일된 걸 가지고 일일이 고침.

Shader "Unlit/Desk"
{
	Properties
	{
		_MainTex("Albedo", 2D) = "white" {}
		_XOffset("X Offset", Range(-5, 5)) = 0.0
		_YOffset("Y Offset", Range(-5, 5)) = 0.0
		_ScreenWidthWeight("Screen Width Weight", Range(0, 2)) = 1.0
		_ShadowColor("Shadow Color", Color) = (0,0,0,1)
		_TexWidth("Texture Width", Int) = 1024
		_TexHeight("Texture Height", Int) = 512

	}

	CGINCLUDE
	ENDCG

	SubShader
	{
		Tags
		{
			"RenderType"="Transparent"
			"Queue"="AlphaTest"
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
			}

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma multi_compile_fwdbase

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			#include "UnityStandardInput.cginc"
			#include "UnityStandardCore.cginc"

			uniform float4 _MainTex_TexelSize;
			uniform float _XOffset;
			uniform float _YOffset;
			uniform float _ScreenWidthWeight;
			uniform fixed4 _ShadowColor;
			uniform int _TexWidth;
			uniform int _TexHeight;

			struct appdata
			{
				float4 vertex	: POSITION;
				float2 uv		: TEXCOORD0;
			};

			struct v2f
			{
				float4 pos							: SV_POSITION;
				float4 screenPos					: TEXCOORD0;
				unityShadowCoord4 	_ShadowCoord	: TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;

				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.pos);

				TRANSFER_SHADOW(o);

				return o;
			}


			// i.pos는 screen space...
			// Unity Editor에서는 Game View Window전체가 기준이 되버린다. -> 오류를 만든다.
			// x : 0 ~ Screen Width
			// y : 0 ~ Screen Height
			// 좌표계 :
			//   (0,0)   (SW,0)
			//
			//   (0,SH)  (SW,SH)
			half4 frag(v2f i) : SV_Target
			{
				half atten = SHADOW_ATTENUATION(i);

				float4 pos = UNITY_PROJ_COORD(i.screenPos);

				half4 c;

				float screenWidth = _ScreenParams.x;
				float screenHeight = _ScreenParams.y;
				//				float divTextureWidth = 1.0f / _TexWidth;
				//float ratio = (textureWidth / (screenWidth * _ScreenWidthWeight));

				float ratio = 1 / screenWidth;

				i.pos.y = screenHeight - i.pos.y;

				float tx = ratio * i.pos.x;
				float ty = ratio * i.pos.y;

				if (tx < 0 || ty < 0 || tx >= 1 || ty >= 0.995f)
				{
					discard;
				}

				ty = 1 - ty;
				#if UNITY_UV_STARTS_AT_TOP
				ty = 1 - ty;
				#endif

				c = tex2D(_MainTex, float2(tx, ty));
				if (atten < 1) {
					c.rgb *= _ShadowColor.rgb;
				}

				return c;
			}

			ENDCG
		}
	}
}
