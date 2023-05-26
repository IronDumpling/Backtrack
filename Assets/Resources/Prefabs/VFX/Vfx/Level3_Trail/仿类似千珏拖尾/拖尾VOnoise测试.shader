// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AFX/Test/拖尾噪波测试"
{
	Properties
	{
		_VO_Scale("VO_Scale", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("CullMode", Int) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)]_ZtestMode("ZtestMode", Int) = 4
		[Enum(UnityEngine.Rendering.BlendMode)]_Src("Src", Int) = 5
		[Enum(UnityEngine.Rendering.BlendMode)]_Dst("Dst", Int) = 1
		[Enum(OFF,0,ON,1)]_UVSpeed("UV控制流动（+w）", Float) = 0
		_MainTex("MainTex", 2D) = "white" {}
		_MainColor("MainColor", Color) = (1,1,1,1)
		_Emission("Emission", Float) = 1
		_Panner("Panner", Vector) = (0,0,0,0)
		_RGBA_Alpha("RGBA_Alpha", Vector) = (0,0,0,1)
		_MaskTex("MaskTex", 2D) = "white" {}
		_MaskScale("MaskScale", Float) = 1
		_VONoiseTiling("VONoiseTiling", Vector) = (5,5,0,0)
		_VOPanner("VOPanner", Vector) = (1,0,1,1)
		_VOScale("VOScale", Vector) = (0,0,0,0)
		[Toggle]_ToggleSwitch0("遮罩反向", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend [_Src] [_Dst]
		AlphaToMask Off
		Cull [_CullMode]
		ColorMask RGBA
		ZWrite Off
		ZTest [_ZtestMode]
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform int _Src;
			uniform int _CullMode;
			uniform int _ZtestMode;
			uniform int _Dst;
			uniform half _ToggleSwitch0;
			uniform half _VO_Scale;
			uniform half4 _VOPanner;
			uniform half4 _VONoiseTiling;
			uniform half3 _VOScale;
			uniform sampler2D _MainTex;
			uniform half4 _Panner;
			uniform half _UVSpeed;
			uniform half4 _MainTex_ST;
			uniform half4 _MainColor;
			uniform half _Emission;
			uniform half4 _RGBA_Alpha;
			uniform sampler2D _MaskTex;
			uniform half4 _MaskTex_ST;
			uniform half _MaskScale;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half2 texCoord65 = v.ase_texcoord * float2( 1,1 ) + float2( 0,0 );
				half mulTime42 = _Time.y * _VOPanner.z;
				half2 appendResult41 = (half2(_VOPanner.x , _VOPanner.y));
				half2 appendResult58 = (half2(_VONoiseTiling.x , _VONoiseTiling.y));
				half2 appendResult59 = (half2(_VONoiseTiling.z , _VONoiseTiling.w));
				half2 texCoord73 = v.ase_texcoord.xy * appendResult58 + appendResult59;
				half2 panner76 = ( mulTime42 * appendResult41 + texCoord73);
				half simplePerlin2D83 = snoise( panner76 );
				simplePerlin2D83 = simplePerlin2D83*0.5 + 0.5;
				
				o.ase_texcoord1 = v.ase_texcoord;
				o.ase_color = v.color;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( (( _ToggleSwitch0 )?( ( 1.0 - texCoord65.x ) ):( texCoord65.x )) * _VO_Scale * (-1.0 + (simplePerlin2D83 - 0.0) * (1.0 - -1.0) / (1.0 - 0.0)) * _VOScale );
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half mulTime12 = _Time.y * _Panner.z;
				half4 texCoord18 = i.ase_texcoord1;
				texCoord18.xy = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				half2 appendResult10 = (half2(_Panner.x , _Panner.y));
				half2 uv_MainTex = i.ase_texcoord1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				half2 panner8 = ( ( ( mulTime12 + _Panner.w ) + ( texCoord18.z * _UVSpeed ) ) * appendResult10 + uv_MainTex);
				half4 tex2DNode4 = tex2D( _MainTex, panner8 );
				half4 appendResult26 = (half4(( tex2DNode4 * _MainColor * i.ase_color * _Emission ).rgb , ( ( ( tex2DNode4.r * _RGBA_Alpha.x ) + ( _RGBA_Alpha.y * tex2DNode4.g ) + ( _RGBA_Alpha.z * tex2DNode4.b ) + ( _RGBA_Alpha.w * tex2DNode4.a ) ) * i.ase_color.a )));
				float2 uv_MaskTex = i.ase_texcoord1.xy * _MaskTex_ST.xy + _MaskTex_ST.zw;
				
				
				finalColor = ( appendResult26 * ( tex2D( _MaskTex, uv_MaskTex ).r * _MaskScale ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18800
26;15;2508;1325;-936.4861;156.6329;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;95;-1368.901,-733.5633;Inherit;False;3302.155;1479.807;;6;87;86;85;84;68;26;颜色输出;1,0.7122642,0.7122642,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;85;-1283.844,-514.0452;Inherit;False;895.001;640;;10;9;17;12;18;15;13;10;16;11;8;主贴图UV流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;9;-1233.844,-326.0458;Inherit;False;Property;_Panner;Panner;9;0;Create;True;0;0;0;False;0;False;0,0,0,0;-1,0,1.4,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;12;-1041.843,-246.0457;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1224.344,-146.0456;Inherit;False;0;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-1217.844,9.954474;Inherit;False;Property;_UVSpeed;UV控制流动（+w）;5;1;[Enum];Create;False;0;2;OFF;0;ON;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-881.8427,-150.0456;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-873.3427,-246.0457;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;96;-911.4533,883.7606;Inherit;False;2815.959;1366.642;;8;49;88;89;90;91;92;93;94;顶点偏移计算;0.8537736,1,0.9433743,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-874.3427,-464.0451;Inherit;False;0;4;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-724.3428,-242.0457;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-791.3429,-341.0458;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;94;-861.4533,1036.361;Inherit;False;709.0757;277.9998;;4;57;58;59;73;噪波的UV;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;57;-811.4533,1096.362;Inherit;False;Property;_VONoiseTiling;VONoiseTiling;13;0;Create;True;0;0;0;False;0;False;5,5,0,0;0.7,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;84;-303.8894,-616.954;Inherit;False;606;713.382;;5;4;6;30;5;7;主贴图;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;92;-675.6485,1363.532;Inherit;False;772.6505;452.5594;;4;41;42;40;76;噪波UV的流动;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;8;-593.8427,-326.0458;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;86;-122.0142,205.7523;Inherit;False;988.7238;492.135;;7;20;19;22;21;23;25;27;主贴图使用通道选择;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;40;-625.6486,1604.092;Inherit;False;Property;_VOPanner;VOPanner;14;0;Create;True;0;0;0;False;0;False;1,0,1,1;-1,0.1,1.2,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;58;-612.4534,1086.362;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;59;-608.4534,1179.362;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;20;-72.01418,278.3558;Inherit;False;Property;_RGBA_Alpha;RGBA_Alpha;10;0;Create;True;0;0;0;False;0;False;0,0,0,1;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-253.8895,-296.1273;Inherit;True;Property;_MainTex;MainTex;6;0;Create;True;0;0;0;False;0;False;-1;None;b868cf65ef6c8cd4997394fc51496858;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;240.127,562.8873;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;42;-399.3784,1653.093;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;238.4628,370.5593;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;240.4628,468.5593;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;238.4628,273.5593;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;88;1070.111,1049.149;Inherit;False;672.8599;352;;3;67;66;65;UV的U通道做偏移的遮罩;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;41;-366.6685,1550.091;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;73;-394.3781,1132.032;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;91;624.7066,1483.497;Inherit;False;527.158;418.3929;;3;61;62;60;Noise映射到有正有负;1,1,1,1;0;0
Node;AmplifyShaderEditor.VertexColorNode;7;-123.446,-110.5715;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;93;180.5721,1135.075;Inherit;False;314;309;;1;83;噪波;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;6;-169.8896,-465.1274;Inherit;False;Property;_MainColor;MainColor;7;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;30;-134.2699,-566.954;Inherit;False;Property;_Emission;Emission;8;0;Create;True;0;0;0;False;0;False;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;445.5075,262.8728;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;65;1120.111,1095.149;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;87;913.1503,358.2428;Inherit;False;634.9999;387.9995;;3;71;69;70;主贴图的遮罩;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;76;-173.9983,1413.532;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;66;1340.791,1184.842;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;89;1226.481,1682.854;Inherit;False;261;238;;1;54;3个方向强度控制;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;140.1106,-273.1274;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;69;963.1501,408.2432;Inherit;True;Property;_MaskTex;MaskTex;11;0;Create;True;0;0;0;False;0;False;-1;None;1eb2bb82dc6e7c244acb33afd0035178;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;71;1235.15,630.2437;Inherit;False;Property;_MaskScale;MaskScale;12;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;704.7092,255.7525;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;674.7066,1711.889;Inherit;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;90;1274.988,1959.605;Inherit;False;221;166;;1;64;总体偏移强度控制;1,1,1,1;0;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;83;230.5722,1185.075;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;680.7066,1785.89;Inherit;False;Constant;_Float1;Float 1;14;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;26;1499.526,90.58102;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;64;1324.988,2009.605;Inherit;False;Property;_VO_Scale;VO_Scale;0;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;54;1276.481,1732.854;Inherit;False;Property;_VOScale;VOScale;15;0;Create;True;0;0;0;False;0;False;0,0,0;0.7,0.7,0.7;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ToggleSwitchNode;67;1491.971,1112.629;Inherit;True;Property;_ToggleSwitch0;遮罩反向;16;0;Create;False;0;0;0;False;0;False;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;1386.15,446.2437;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;60;861.8651,1533.497;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;1771.254,89.83611;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.IntNode;28;2245.516,365.582;Inherit;False;Property;_Src;Src;3;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.BlendMode;True;0;False;5;5;False;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;2;1956.35,363.6919;Inherit;False;Property;_CullMode;CullMode;1;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;0;False;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;3;2093.35,364.6919;Inherit;False;Property;_ZtestMode;ZtestMode;2;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.CompareFunction;True;0;False;4;4;False;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;1636.834,1691.285;Inherit;True;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.IntNode;29;2376.515,366.582;Inherit;False;Property;_Dst;Dst;4;1;[Enum];Create;True;0;1;Option1;0;1;UnityEngine.Rendering.BlendMode;True;0;False;1;10;False;0;1;INT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;1957.85,439.6919;Half;False;True;-1;2;ASEMaterialInspector;100;1;AFX/Test/拖尾噪波测试;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;8;5;True;28;1;True;29;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;2;True;2;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;True;3;True;False;0;False;-1;0;False;-1;True;2;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;12;0;9;3
WireConnection;15;0;18;3
WireConnection;15;1;17;0
WireConnection;13;0;12;0
WireConnection;13;1;9;4
WireConnection;16;0;13;0
WireConnection;16;1;15;0
WireConnection;10;0;9;1
WireConnection;10;1;9;2
WireConnection;8;0;11;0
WireConnection;8;2;10;0
WireConnection;8;1;16;0
WireConnection;58;0;57;1
WireConnection;58;1;57;2
WireConnection;59;0;57;3
WireConnection;59;1;57;4
WireConnection;4;1;8;0
WireConnection;23;0;20;4
WireConnection;23;1;4;4
WireConnection;42;0;40;3
WireConnection;21;0;20;2
WireConnection;21;1;4;2
WireConnection;22;0;20;3
WireConnection;22;1;4;3
WireConnection;19;0;4;1
WireConnection;19;1;20;1
WireConnection;41;0;40;1
WireConnection;41;1;40;2
WireConnection;73;0;58;0
WireConnection;73;1;59;0
WireConnection;25;0;19;0
WireConnection;25;1;21;0
WireConnection;25;2;22;0
WireConnection;25;3;23;0
WireConnection;76;0;73;0
WireConnection;76;2;41;0
WireConnection;76;1;42;0
WireConnection;66;0;65;1
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;5;2;7;0
WireConnection;5;3;30;0
WireConnection;27;0;25;0
WireConnection;27;1;7;4
WireConnection;83;0;76;0
WireConnection;26;0;5;0
WireConnection;26;3;27;0
WireConnection;67;0;65;1
WireConnection;67;1;66;0
WireConnection;70;0;69;1
WireConnection;70;1;71;0
WireConnection;60;0;83;0
WireConnection;60;3;61;0
WireConnection;60;4;62;0
WireConnection;68;0;26;0
WireConnection;68;1;70;0
WireConnection;49;0;67;0
WireConnection;49;1;64;0
WireConnection;49;2;60;0
WireConnection;49;3;54;0
WireConnection;1;0;68;0
WireConnection;1;1;49;0
ASEEND*/
//CHKSM=F4442D4E2A7CF82C8F30E6611FF9913CDE76102E