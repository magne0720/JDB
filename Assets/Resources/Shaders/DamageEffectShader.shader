Shader "Custom/DamageEffectShader" {
	Properties{
		_MainTex("MainTex", 2D) = ""{}
		_Limit("limit", Float) = 0.0

	}
	SubShader{
		Pass{
		CGPROGRAM
	
		#include "UnityCG.cginc"
		
		#pragma vertex vert_img
		#pragma fragment frag
	
		sampler2D _MainTex;
		float _T;
		float _Depth;

		float _Limit;


		float random(fixed2 p) {
			return frac(sin(dot(p, fixed2(12.9898, 78.233))) * 43758.5453);
		}

		fixed4 frag(v2f_img i) : COLOR{

		float n = random(i.uv * _T);
		float speed = 1.3;//秒換算

		//ピクセル取得
		fixed4 c = tex2D(_MainTex, i.uv);

		i.uv -= fixed2(0.5, 0.5);

		float a = c.r * 0.3 + c.g * 0.59 + c.b * 0.11;
		c = fixed4( a,c.g,a,1.0);

		c = fixed4( c.r + ( 0.2 *_Limit), c.g + (0.1 *_Limit), c.b + (0.0 *_Limit), 1.0);

		
		c.r += distance(i.uv*sin(_T*3.14*speed), fixed2(0, 0))*_Depth;
		float gb = distance(i.uv, fixed2(0, 0)) *(n*0.8) *_Depth;
		c.g -= gb;
		c.b -= gb;
		

		return fixed4(c);
		}
			ENDCG
		}
	}
}