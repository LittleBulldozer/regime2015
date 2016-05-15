Shader "Unlit/PPP"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
//	_BorderTex("Texture", 2D) = "" {}
		_Zoom("Zoom", Float) = 0.0
		_DistortionPower("_DistortionPower", Float) = 0.0

			_RedCyan("RedCyan", Float) = 0.0

			_GreenMagenta("GreenMagenta", Float) = 0.0

			_BlueYellow("BlueYellow", Float) = 0.0

			_BorderOnOff("BorderOnOff", Int) = 0


	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
//			sampler2D _BorderTex;
			float4 _MainTex_ST;
			uniform half _Zoom;
			uniform half _DistortionPower;

			uniform float _RedCyan;
			uniform float _GreenMagenta;
			uniform float _BlueYellow;
			uniform float _BarrelDistortion;
			uniform int _BorderOnOff;

			float4 barrelDistortion(float4 p)
			{
				float2 v = p.xy / p.w;
				float r2 = v.x * v.x + v.y * v.y;
				float f = 1 + _DistortionPower * r2;

				p.xy = (f * (1.0 + _Zoom) * v) * p.w;
				return p;
			}

			float4 DistortVert(float4 p)
			{
				float2 v = p.xy / p.w;
				float radius = length(v);
				if (radius > 0)
				{
					float theta = atan2(v.y, v.x);
					radius = pow(radius, 1.5);

					v.x = radius * cos(theta);
					v.y = radius * sin(theta);
					p.xy = v.xy * p.w;
				}
				return p;
			}
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.vertex = barrelDistortion(o.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 theScreen;// = tex2D(_MainTex, i.uv);

			/*
				if ((_RedCyan == 0) && (_GreenMagenta == 0) && (_BlueYellow == 0))
				{
					if (_BorderOnOff == 1)
					{
						theScreen.r = tex2D(_MainTex, i.uv).r * tex2D(_BorderTex, ((i.uv * 0.9375) + 0.03125)).r;
						theScreen.g = tex2D(_MainTex, i.uv).g * tex2D(_BorderTex, ((i.uv * 0.9375) + 0.03125)).g;
						theScreen.b = tex2D(_MainTex, i.uv).b * tex2D(_BorderTex, ((i.uv * 0.9375) + 0.03125)).b;
					}
					if (_BorderOnOff == 0)
					{
						theScreen.r = tex2D(_MainTex, i.uv).r;
						theScreen.g = tex2D(_MainTex, i.uv).g;
						theScreen.b = tex2D(_MainTex, i.uv).b;
					}
				}*/

				half2 redUV;
				half2 greUV;
				half2 bluUV;

				float redcyan = _RedCyan*0.001;
				float gremag = _GreenMagenta*0.001;
				float bluyel = _BlueYellow*0.001;

				redUV = (i.uv * (1.0 + redcyan)) - (redcyan / 2.0);
				greUV = (i.uv * (1.0 + gremag)) - (gremag / 2.0);
				bluUV = (i.uv * (1.0 + bluyel)) - (bluyel / 2.0);
				/*
				if (_BorderOnOff == 1)
				{
				theScreen.r = tex2D(_MainTex, redUV).r * tex2D(_BorderTex, ((redUV * 0.9375) + 0.03125)).r;
				theScreen.g = tex2D(_MainTex, greUV).g * tex2D(_BorderTex, ((greUV * 0.9375) + 0.03125)).g;
				theScreen.b = tex2D(_MainTex, bluUV).b * tex2D(_BorderTex, ((bluUV * 0.9375) + 0.03125)).b;
				}
				if (_BorderOnOff == 0)
				{
				}

				*/
				theScreen.r = tex2D(_MainTex, redUV).r;
				theScreen.g = tex2D(_MainTex, greUV).g;
				theScreen.b = tex2D(_MainTex, bluUV).b;

				return theScreen;
			}
			ENDCG
		}
	}
}
