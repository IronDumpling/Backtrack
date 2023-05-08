// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "VFX/PandaShaderSampleURPV1.0"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[Enum(UnityEngine.Rendering.BlendMode)]_Scr("Scr", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)]_Dst("Dst", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("CullMode", Float) = 0
		_MainTex("MainTex", 2D) = "white" {}
		_VTOTex("VTOTex", 2D) = "black" {}
		[Toggle]_VTOTexAR("VTOTexAR", Float) = 1
		[Toggle]_MainTexAR("MainTexAR", Float) = 0
		[HDR]_MainColor("MainColor", Color) = (1,1,1,1)
		_VTOTexUSpeed("VTOTexUSpeed", Float) = 0
		_MainTexUSpeed("MainTexUSpeed", Float) = 0
		_MainTexVSpeed("MainTexVSpeed", Float) = 0
		_VTOTexVSpeed("VTOTexVSpeed", Float) = 0
		[Toggle]_CustomMainTex("CustomMainTex", Float) = 0
		[Toggle(_FMASKTEX_ON)] _FMaskTex("FMaskTex", Float) = 0
		_MaskTex("MaskTex", 2D) = "white" {}
		[Toggle]_MaskTexAR("MaskTexAR", Float) = 1
		_MaskTexUSpeed("MaskTexUSpeed", Float) = 0
		_MaskTexVSpeed("MaskTexVSpeed", Float) = 0
		[Toggle(_FDISTORTTEX_ON)] _FDistortTex("FDistortTex", Float) = 0
		_DistortTex("DistortTex", 2D) = "white" {}
		[Toggle]_DistortTexAR("DistortTexAR", Float) = 1
		_DistortFactor("DistortFactor", Range( 0 , 1)) = 0
		_DistortTexUSpeed("DistortTexUSpeed", Float) = 0
		_DistortTexVSpeed("DistortTexVSpeed", Float) = 0
		[Toggle]_DistortMainTex("DistortMainTex", Float) = 0
		[Toggle]_DistortMaskTex("DistortMaskTex", Float) = 0
		[Toggle]_DistortDissolveTex("DistortDissolveTex", Float) = 0
		[Toggle(_FDISSOLVETEX_ON)] _FDissolveTex("FDissolveTex", Float) = 0
		_DissolveTex("DissolveTex", 2D) = "white" {}
		[Toggle]_DissolveTexAR("DissolveTexAR", Float) = 1
		[HDR]_DissolveColor("DissolveColor", Color) = (1,1,1,1)
		[Toggle]_CustomDissolve("CustomDissolve", Float) = 0
		_DissolveFactor("DissolveFactor", Range( 0 , 1)) = 0
		_DissolveSoft("DissolveSoft", Range( 0 , 1)) = 0.1
		_DissolveWide("DissolveWide", Range( 0 , 1)) = 0.05
		_DissolveTexUSpeed("DissolveTexUSpeed", Float) = 0
		_DissolveTexVSpeed("DissolveTexVSpeed", Float) = 0
		_MainAlpha("MainAlpha", Range( 0 , 10)) = 1
		[Toggle(_FFNL_ON)] _FFnl("FFnl", Float) = 0
		[HDR]_FnlColor("FnlColor", Color) = (1,1,1,1)
		_FnlScale("FnlScale", Range( 0 , 2)) = 0
		[Toggle(_FDEPTH_ON)] _FDepth("FDepth", Float) = 0
		_FnlPower("FnlPower", Range( 1 , 10)) = 1
		[Toggle]_ReFnl("ReFnl", Float) = 0
		[Enum(Alpha,0,Add,1)]_BlendMode("BlendMode", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)]_ZTest("ZTest", Float) = 4
		_DepthFade("DepthFade", Range( 0 , 10)) = 1
		[Toggle(_IFVTO_ON)] _IfVTO("IfVTO", Float) = 0
		[Toggle]_CustomVTO("CustomVTO", Float) = 0
		[ASEEnd]_VTOScale("VTOScale", Float) = 1

		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull [_CullMode]
		AlphaToMask Off
		
		HLSLINCLUDE
		#pragma target 3.0

		#pragma prefer_hlslcc gles
		#pragma exclude_renderers d3d11_9x 

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS

		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForwardOnly" }
			
			Blend [_Scr] [_Dst], One OneMinusSrcAlpha
			ZWrite Off
			ZTest [_ZTest]
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			
			#define _RECEIVE_SHADOWS_OFF 1
			#define ASE_SRP_VERSION 100501
			#define REQUIRE_DEPTH_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _IFVTO_ON
			#pragma shader_feature_local _FDISSOLVETEX_ON
			#pragma shader_feature_local _FDISTORTTEX_ON
			#pragma shader_feature_local _FFNL_ON
			#pragma shader_feature_local _FMASKTEX_ON
			#pragma shader_feature_local _FDEPTH_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogFactor : TEXCOORD2;
				#endif
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			half4 _MainColor;
			float4 _MaskTex_ST;
			float4 _DistortTex_ST;
			float4 _VTOTex_ST;
			half4 _FnlColor;
			float4 _MainTex_ST;
			half4 _DissolveColor;
			float4 _DissolveTex_ST;
			half _DissolveWide;
			half _DissolveSoft;
			half _DissolveTexAR;
			half _DissolveTexUSpeed;
			half _DissolveTexVSpeed;
			half _Dst;
			float _MainAlpha;
			half _ReFnl;
			half _FnlScale;
			half _FnlPower;
			half _MainTexAR;
			half _MaskTexAR;
			half _MaskTexUSpeed;
			half _MaskTexVSpeed;
			half _DistortDissolveTex;
			half _DissolveFactor;
			half _DistortFactor;
			half _DistortMaskTex;
			half _CullMode;
			half _BlendMode;
			half _ZTest;
			half _VTOTexAR;
			half _VTOTexUSpeed;
			half _VTOTexVSpeed;
			float _CustomVTO;
			float _CustomDissolve;
			float _VTOScale;
			half _MainTexUSpeed;
			half _MainTexVSpeed;
			float _CustomMainTex;
			half _DistortMainTex;
			half _DistortTexAR;
			half _DistortTexUSpeed;
			half _DistortTexVSpeed;
			half _Scr;
			float _DepthFade;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _VTOTex;
			sampler2D _MainTex;
			sampler2D _DistortTex;
			sampler2D _DissolveTex;
			sampler2D _MaskTex;
			uniform float4 _CameraDepthTexture_TexelSize;


						
			VertexOutput VertexFunction ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 temp_cast_0 = (0.0).xxx;
				float2 appendResult4_g16 = (float2(_VTOTexUSpeed , _VTOTexVSpeed));
				float2 uv_VTOTex = v.ase_texcoord.xy * _VTOTex_ST.xy + _VTOTex_ST.zw;
				float2 panner5_g16 = ( 1.0 * _Time.y * appendResult4_g16 + uv_VTOTex);
				float4 tex2DNode7_g16 = tex2Dlod( _VTOTex, float4( panner5_g16, 0, 0.0) );
				float4 texCoord393 = v.ase_texcoord1;
				texCoord393.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float3 VTO387 = ( v.ase_normal * ( _VTOTexAR == 0.0 ? tex2DNode7_g16.a : tex2DNode7_g16.r ) * (( _CustomVTO )?( texCoord393.w ):( _VTOScale )) );
				#ifdef _IFVTO_ON
				float3 staticSwitch390 = VTO387;
				#else
				float3 staticSwitch390 = temp_cast_0;
				#endif
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord5.xyz = ase_worldNormal;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord6 = screenPos;
				
				o.ase_color = v.ase_color;
				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				o.ase_texcoord4 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				o.ase_texcoord5.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = staticSwitch390;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				#ifdef ASE_FOG
				o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif
				float Scr106 = _Scr;
				float2 appendResult4_g13 = (float2(_MainTexUSpeed , _MainTexVSpeed));
				float2 uv_MainTex = IN.ase_texcoord3.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 temp_output_3_0_g8 = uv_MainTex;
				float2 appendResult4_g7 = (float2(_DistortTexUSpeed , _DistortTexVSpeed));
				float2 uv_DistortTex = IN.ase_texcoord3.xy * _DistortTex_ST.xy + _DistortTex_ST.zw;
				float2 panner5_g7 = ( 1.0 * _Time.y * appendResult4_g7 + uv_DistortTex);
				float4 tex2DNode7_g7 = tex2D( _DistortTex, panner5_g7 );
				float Distort148 = ( ( _DistortTexAR == 0.0 ? tex2DNode7_g7.a : tex2DNode7_g7.r ) * _DistortFactor );
				#ifdef _FDISTORTTEX_ON
				float2 staticSwitch316 = ( _DistortMainTex == 0.0 ? temp_output_3_0_g8 : ( temp_output_3_0_g8 + Distort148 ) );
				#else
				float2 staticSwitch316 = uv_MainTex;
				#endif
				float4 texCoord328 = IN.ase_texcoord4;
				texCoord328.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult330 = (float2(texCoord328.x , texCoord328.y));
				float2 panner5_g13 = ( 1.0 * _Time.y * appendResult4_g13 + (( _CustomMainTex )?( ( staticSwitch316 + appendResult330 ) ):( staticSwitch316 )));
				float4 tex2DNode7_g13 = tex2D( _MainTex, panner5_g13 );
				float4 MainTexColor215 = ( _MainColor * tex2DNode7_g13 );
				float4 texCoord333 = IN.ase_texcoord4;
				texCoord333.xy = IN.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_275_0 = (-_DissolveWide + ((( _CustomDissolve )?( texCoord333.z ):( _DissolveFactor )) - 0.0) * (1.0 - -_DissolveWide) / (1.0 - 0.0));
				float temp_output_277_0 = ( _DissolveSoft + 0.0001 );
				float temp_output_272_0 = (-temp_output_277_0 + (( temp_output_275_0 + _DissolveWide ) - 0.0) * (1.0 - -temp_output_277_0) / (1.0 - 0.0));
				float2 appendResult4_g10 = (float2(_DissolveTexUSpeed , _DissolveTexVSpeed));
				float2 uv_DissolveTex = IN.ase_texcoord3.xy * _DissolveTex_ST.xy + _DissolveTex_ST.zw;
				float2 temp_output_3_0_g9 = uv_DissolveTex;
				#ifdef _FDISTORTTEX_ON
				float2 staticSwitch314 = ( _DistortDissolveTex == 0.0 ? temp_output_3_0_g9 : ( temp_output_3_0_g9 + Distort148 ) );
				#else
				float2 staticSwitch314 = uv_DissolveTex;
				#endif
				float2 panner5_g10 = ( 1.0 * _Time.y * appendResult4_g10 + staticSwitch314);
				float4 tex2DNode7_g10 = tex2D( _DissolveTex, panner5_g10 );
				float temp_output_351_20 = ( _DissolveTexAR == 0.0 ? tex2DNode7_g10.a : tex2DNode7_g10.r );
				float smoothstepResult264 = smoothstep( temp_output_272_0 , ( temp_output_272_0 + temp_output_277_0 ) , temp_output_351_20);
				float Alpha337 = _MainAlpha;
				float4 lerpResult223 = lerp( MainTexColor215 , _DissolveColor , ( _DissolveColor.a * ( 1.0 - smoothstepResult264 ) * Alpha337 ));
				#ifdef _FDISSOLVETEX_ON
				float4 staticSwitch298 = lerpResult223;
				#else
				float4 staticSwitch298 = MainTexColor215;
				#endif
				float4 temp_cast_0 = (0.0).xxxx;
				float Refnl339 = _ReFnl;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord5.xyz;
				float fresnelNdotV279 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode279 = ( 0.0 + _FnlScale * pow( 1.0 - fresnelNdotV279, _FnlPower ) );
				float temp_output_283_0 = saturate( fresnelNode279 );
				float4 FnlMainColor286 = ( _FnlColor * temp_output_283_0 * _FnlColor.a );
				float4 temp_cast_1 = (0.0).xxxx;
				#ifdef _FFNL_ON
				float4 staticSwitch300 = ( Refnl339 == 0.0 ? FnlMainColor286 : temp_cast_1 );
				#else
				float4 staticSwitch300 = temp_cast_0;
				#endif
				float4 MainColor98 = ( IN.ase_color * ( staticSwitch298 + staticSwitch300 ) );
				float MainTexAlpha138 = ( _MainColor.a * ( _MainTexAR == 0.0 ? tex2DNode7_g13.a : tex2DNode7_g13.r ) );
				float2 appendResult4_g15 = (float2(_MaskTexUSpeed , _MaskTexVSpeed));
				float2 uv_MaskTex = IN.ase_texcoord3.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				float2 temp_output_3_0_g14 = uv_MaskTex;
				#ifdef _FDISTORTTEX_ON
				float2 staticSwitch312 = ( _DistortMaskTex == 0.0 ? temp_output_3_0_g14 : ( temp_output_3_0_g14 + Distort148 ) );
				#else
				float2 staticSwitch312 = uv_MaskTex;
				#endif
				float2 panner5_g15 = ( 1.0 * _Time.y * appendResult4_g15 + staticSwitch312);
				float4 tex2DNode7_g15 = tex2D( _MaskTex, panner5_g15 );
				#ifdef _FMASKTEX_ON
				float staticSwitch291 = ( _MaskTexAR == 0.0 ? tex2DNode7_g15.a : tex2DNode7_g15.r );
				#else
				float staticSwitch291 = 1.0;
				#endif
				float temp_output_270_0 = (-temp_output_277_0 + (temp_output_275_0 - 0.0) * (1.0 - -temp_output_277_0) / (1.0 - 0.0));
				float smoothstepResult256 = smoothstep( temp_output_270_0 , ( temp_output_270_0 + temp_output_277_0 ) , temp_output_351_20);
				float DissolveAlpha212 = smoothstepResult256;
				#ifdef _FDISSOLVETEX_ON
				float staticSwitch299 = DissolveAlpha212;
				#else
				float staticSwitch299 = 1.0;
				#endif
				float ReFnlAlpha318 = ( 1.0 - temp_output_283_0 );
				#ifdef _FFNL_ON
				float staticSwitch319 = ( Refnl339 == 0.0 ? 1.0 : ReFnlAlpha318 );
				#else
				float staticSwitch319 = 1.0;
				#endif
				float4 screenPos = IN.ase_texcoord6;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth375 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth375 = abs( ( screenDepth375 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _DepthFade ) );
				#ifdef _FDEPTH_ON
				float staticSwitch378 = saturate( distanceDepth375 );
				#else
				float staticSwitch378 = 1.0;
				#endif
				float MainAlpha97 = saturate( ( MainTexAlpha138 * staticSwitch291 * IN.ase_color.a * Alpha337 * staticSwitch299 * staticSwitch319 * staticSwitch378 ) );
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = ( Scr106 == 5.0 ? MainColor98 : ( MainColor98 * MainAlpha97 ) ).rgb;
				float Alpha = MainAlpha97;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				return half4( Color, Alpha );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask 0
			AlphaToMask Off

			HLSLPROGRAM
			
			#define _RECEIVE_SHADOWS_OFF 1
			#define ASE_SRP_VERSION 100501
			#define REQUIRE_DEPTH_TEXTURE 1

			
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#pragma shader_feature_local _IFVTO_ON
			#pragma shader_feature_local _FDISTORTTEX_ON
			#pragma shader_feature_local _FMASKTEX_ON
			#pragma shader_feature_local _FDISSOLVETEX_ON
			#pragma shader_feature_local _FFNL_ON
			#pragma shader_feature_local _FDEPTH_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_color : COLOR;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			half4 _MainColor;
			float4 _MaskTex_ST;
			float4 _DistortTex_ST;
			float4 _VTOTex_ST;
			half4 _FnlColor;
			float4 _MainTex_ST;
			half4 _DissolveColor;
			float4 _DissolveTex_ST;
			half _DissolveWide;
			half _DissolveSoft;
			half _DissolveTexAR;
			half _DissolveTexUSpeed;
			half _DissolveTexVSpeed;
			half _Dst;
			float _MainAlpha;
			half _ReFnl;
			half _FnlScale;
			half _FnlPower;
			half _MainTexAR;
			half _MaskTexAR;
			half _MaskTexUSpeed;
			half _MaskTexVSpeed;
			half _DistortDissolveTex;
			half _DissolveFactor;
			half _DistortFactor;
			half _DistortMaskTex;
			half _CullMode;
			half _BlendMode;
			half _ZTest;
			half _VTOTexAR;
			half _VTOTexUSpeed;
			half _VTOTexVSpeed;
			float _CustomVTO;
			float _CustomDissolve;
			float _VTOScale;
			half _MainTexUSpeed;
			half _MainTexVSpeed;
			float _CustomMainTex;
			half _DistortMainTex;
			half _DistortTexAR;
			half _DistortTexUSpeed;
			half _DistortTexVSpeed;
			half _Scr;
			float _DepthFade;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _VTOTex;
			sampler2D _MainTex;
			sampler2D _DistortTex;
			sampler2D _MaskTex;
			sampler2D _DissolveTex;
			uniform float4 _CameraDepthTexture_TexelSize;


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 temp_cast_0 = (0.0).xxx;
				float2 appendResult4_g16 = (float2(_VTOTexUSpeed , _VTOTexVSpeed));
				float2 uv_VTOTex = v.ase_texcoord.xy * _VTOTex_ST.xy + _VTOTex_ST.zw;
				float2 panner5_g16 = ( 1.0 * _Time.y * appendResult4_g16 + uv_VTOTex);
				float4 tex2DNode7_g16 = tex2Dlod( _VTOTex, float4( panner5_g16, 0, 0.0) );
				float4 texCoord393 = v.ase_texcoord1;
				texCoord393.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float3 VTO387 = ( v.ase_normal * ( _VTOTexAR == 0.0 ? tex2DNode7_g16.a : tex2DNode7_g16.r ) * (( _CustomVTO )?( texCoord393.w ):( _VTOScale )) );
				#ifdef _IFVTO_ON
				float3 staticSwitch390 = VTO387;
				#else
				float3 staticSwitch390 = temp_cast_0;
				#endif
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord4.xyz = ase_worldNormal;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord5 = screenPos;
				
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_texcoord3 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;
				o.ase_texcoord4.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = staticSwitch390;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif

				o.clipPos = TransformWorldToHClip( positionWS );
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 appendResult4_g13 = (float2(_MainTexUSpeed , _MainTexVSpeed));
				float2 uv_MainTex = IN.ase_texcoord2.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 temp_output_3_0_g8 = uv_MainTex;
				float2 appendResult4_g7 = (float2(_DistortTexUSpeed , _DistortTexVSpeed));
				float2 uv_DistortTex = IN.ase_texcoord2.xy * _DistortTex_ST.xy + _DistortTex_ST.zw;
				float2 panner5_g7 = ( 1.0 * _Time.y * appendResult4_g7 + uv_DistortTex);
				float4 tex2DNode7_g7 = tex2D( _DistortTex, panner5_g7 );
				float Distort148 = ( ( _DistortTexAR == 0.0 ? tex2DNode7_g7.a : tex2DNode7_g7.r ) * _DistortFactor );
				#ifdef _FDISTORTTEX_ON
				float2 staticSwitch316 = ( _DistortMainTex == 0.0 ? temp_output_3_0_g8 : ( temp_output_3_0_g8 + Distort148 ) );
				#else
				float2 staticSwitch316 = uv_MainTex;
				#endif
				float4 texCoord328 = IN.ase_texcoord3;
				texCoord328.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult330 = (float2(texCoord328.x , texCoord328.y));
				float2 panner5_g13 = ( 1.0 * _Time.y * appendResult4_g13 + (( _CustomMainTex )?( ( staticSwitch316 + appendResult330 ) ):( staticSwitch316 )));
				float4 tex2DNode7_g13 = tex2D( _MainTex, panner5_g13 );
				float MainTexAlpha138 = ( _MainColor.a * ( _MainTexAR == 0.0 ? tex2DNode7_g13.a : tex2DNode7_g13.r ) );
				float2 appendResult4_g15 = (float2(_MaskTexUSpeed , _MaskTexVSpeed));
				float2 uv_MaskTex = IN.ase_texcoord2.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				float2 temp_output_3_0_g14 = uv_MaskTex;
				#ifdef _FDISTORTTEX_ON
				float2 staticSwitch312 = ( _DistortMaskTex == 0.0 ? temp_output_3_0_g14 : ( temp_output_3_0_g14 + Distort148 ) );
				#else
				float2 staticSwitch312 = uv_MaskTex;
				#endif
				float2 panner5_g15 = ( 1.0 * _Time.y * appendResult4_g15 + staticSwitch312);
				float4 tex2DNode7_g15 = tex2D( _MaskTex, panner5_g15 );
				#ifdef _FMASKTEX_ON
				float staticSwitch291 = ( _MaskTexAR == 0.0 ? tex2DNode7_g15.a : tex2DNode7_g15.r );
				#else
				float staticSwitch291 = 1.0;
				#endif
				float Alpha337 = _MainAlpha;
				float4 texCoord333 = IN.ase_texcoord3;
				texCoord333.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_275_0 = (-_DissolveWide + ((( _CustomDissolve )?( texCoord333.z ):( _DissolveFactor )) - 0.0) * (1.0 - -_DissolveWide) / (1.0 - 0.0));
				float temp_output_277_0 = ( _DissolveSoft + 0.0001 );
				float temp_output_270_0 = (-temp_output_277_0 + (temp_output_275_0 - 0.0) * (1.0 - -temp_output_277_0) / (1.0 - 0.0));
				float2 appendResult4_g10 = (float2(_DissolveTexUSpeed , _DissolveTexVSpeed));
				float2 uv_DissolveTex = IN.ase_texcoord2.xy * _DissolveTex_ST.xy + _DissolveTex_ST.zw;
				float2 temp_output_3_0_g9 = uv_DissolveTex;
				#ifdef _FDISTORTTEX_ON
				float2 staticSwitch314 = ( _DistortDissolveTex == 0.0 ? temp_output_3_0_g9 : ( temp_output_3_0_g9 + Distort148 ) );
				#else
				float2 staticSwitch314 = uv_DissolveTex;
				#endif
				float2 panner5_g10 = ( 1.0 * _Time.y * appendResult4_g10 + staticSwitch314);
				float4 tex2DNode7_g10 = tex2D( _DissolveTex, panner5_g10 );
				float temp_output_351_20 = ( _DissolveTexAR == 0.0 ? tex2DNode7_g10.a : tex2DNode7_g10.r );
				float smoothstepResult256 = smoothstep( temp_output_270_0 , ( temp_output_270_0 + temp_output_277_0 ) , temp_output_351_20);
				float DissolveAlpha212 = smoothstepResult256;
				#ifdef _FDISSOLVETEX_ON
				float staticSwitch299 = DissolveAlpha212;
				#else
				float staticSwitch299 = 1.0;
				#endif
				float Refnl339 = _ReFnl;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - WorldPosition );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord4.xyz;
				float fresnelNdotV279 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode279 = ( 0.0 + _FnlScale * pow( 1.0 - fresnelNdotV279, _FnlPower ) );
				float temp_output_283_0 = saturate( fresnelNode279 );
				float ReFnlAlpha318 = ( 1.0 - temp_output_283_0 );
				#ifdef _FFNL_ON
				float staticSwitch319 = ( Refnl339 == 0.0 ? 1.0 : ReFnlAlpha318 );
				#else
				float staticSwitch319 = 1.0;
				#endif
				float4 screenPos = IN.ase_texcoord5;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth375 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth375 = abs( ( screenDepth375 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _DepthFade ) );
				#ifdef _FDEPTH_ON
				float staticSwitch378 = saturate( distanceDepth375 );
				#else
				float staticSwitch378 = 1.0;
				#endif
				float MainAlpha97 = saturate( ( MainTexAlpha138 * staticSwitch291 * IN.ase_color.a * Alpha337 * staticSwitch299 * staticSwitch319 * staticSwitch378 ) );
				
				float Alpha = MainAlpha97;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif
				return 0;
			}
			ENDHLSL
		}

	
	}
	
	CustomEditor "SampleGUIURP"
	Fallback "Hidden/InternalErrorShader"
	
}
