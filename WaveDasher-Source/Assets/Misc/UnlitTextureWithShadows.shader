Shader "Unlit/UnlitTextureWithShadows" {
Properties{
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Color("Color", Color) = (1,1,1,1)
	_OverColor("OverColor", Color) = (0,0,0,0)
}

SubShader{
	//Tags{ "Queue" = "Opaque" }
	LOD 100

	Pass{
	Lighting Off
	// Use texture alpha to blend up to white (= full illumination)
	SetTexture[_MainTex]{
		constantColor[_Color]
		combine constant lerp(texture) previous
	}
	// Multiply in texture
	SetTexture[_MainTex]{
		combine previous * texture
	}
	SetTexture[_MainTex]{
		constantColor[_OverColor]
		combine constant + previous
	}
}

// Pass to render object as a shadow caster
Pass
{
	Name "ShadowCaster"
	Tags{ "LightMode" = "ShadowCaster" }

	Fog{ Mode Off }
	ZWrite On ZTest LEqual Cull Off
	Offset 1, 1

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_shadowcaster
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct v2f {
	V2F_SHADOW_CASTER;
};

v2f vert(appdata_base v)
{
	v2f o;
	TRANSFER_SHADOW_CASTER(o)
		return o;
}

float4 frag(v2f i) : COLOR
{
	SHADOW_CASTER_FRAGMENT(i)
}
ENDCG
}

}

}