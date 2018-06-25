Shader "Hidden/IsoNoise"
{
	Properties{
		_MainTex("MainTex", 2D) = ""{}
	}
		SubShader{
		Pass{
		CGPROGRAM

		#include "UnityCG.cginc"

		#pragma vertex vert_img
		#pragma fragment frag

		sampler2D _MainTex;
		float _T;


		float random(fixed2 p) {
			return frac(sin(dot(p, fixed2(12.9898, 78.233))) * 43758.5453);
		}

		fixed4 frag(v2f_img i) : COLOR{

						//ピクセル取得
		fixed4 c = tex2D(_MainTex, i.uv);
		float n = i.uv *random(i.uv * _T)*0.5 + 0.7;
		//n *= 1.2;

		c *= fixed4(0.0, n, 0.0, 1.0);
		c.g += 0.12;

		return fixed4(c);
		}
			ENDCG
		}
	}
}
