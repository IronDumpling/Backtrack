Shader "Hidden/KriptoFX/KWS/CausticDecal"
{
	Subshader
	{
		ZWrite Off
		Cull Front

		ZTest Always
		Blend DstColor Zero
		//Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			HLSLPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog

			#pragma multi_compile _ USE_DISPERSION
			#pragma multi_compile _ USE_LOD1 USE_LOD2 USE_LOD3
			#pragma multi_compile _ KW_DYNAMIC_WAVES
			#pragma multi_compile _ USE_DEPTH_SCALE

			#include "../Common/KWS_WaterVariables.cginc"
			#include "../Common/KWS_WaterPassHelpers.cginc"
			#include "KWS_PlatformSpecificHelpers.cginc"
			#include "../Common/KWS_CommonHelpers.cginc"
			#include "../Common/KWS_WaterHelpers.cginc"
			#include "../Common/CommandPass/KWS_CausticDecal_Common.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 screenUV : TEXCOORD0;
			};

			v2f vert(float4 vertex : POSITION)
			{
				v2f o;
				o.vertex = ObjectToClipPos(vertex);
				o.screenUV = ComputeScreenPos(o.vertex);
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				float2 screenUV = i.screenUV.xy / i.screenUV.w;
				float depth;
				float3 worldPos;
				half4 caustic = GetCaustic(screenUV, depth, worldPos);

				half3 fogColor = 0;
				half3 fogOpacity = 0;
				float distanceToCamera = GetWorldToCameraDistance(worldPos);

				//as we don't have Z position (because screen space decal uses volume box), I use manual fog opacity calculation
				#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
					#if defined(FOG_EXP)
						distanceToCamera *= unity_FogParams.x;
						fogOpacity = 1 - saturate(exp2(-distanceToCamera));
					#elif defined(FOG_EXP2)
						distanceToCamera *= unity_FogParams.x;
						fogOpacity = 1 - saturate(exp2(-distanceToCamera * distanceToCamera));
					#elif defined(FOG_LINEAR)
						distanceToCamera = distanceToCamera * unity_FogParams.z + unity_FogParams.w;
						fogOpacity = 1 - saturate(distanceToCamera);
					#endif
				#endif

				caustic.rgb = lerp(caustic.rgb, 1, fogOpacity);
				return caustic;
			}

			ENDHLSL
		}
	}
}