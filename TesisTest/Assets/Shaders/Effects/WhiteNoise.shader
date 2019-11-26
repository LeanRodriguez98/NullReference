Shader "NullReference/WhiteNoise"
{
	SubShader
	{
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _Color;

			float hash(uint2 hashInput)
			{
				uint2 q = 1103515245U * ((hashInput.x >> 1U) ^ (hashInput.yx))*abs(sin(_Time.y));
				uint  n = 1103515245U * ((q.x) ^ (q.y >> 3U));
				return float(n) * (1.0 / float(0xffffffffU));
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				uint2 p = (i.uv*_ScreenParams);

				float f = hash(p);

				col = float4(f, f, f, 1.0);

				return col;
			}
			ENDCG
		}
	}
}
