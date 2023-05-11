Shader "NeuroFractalsPack/LinesShader" {
    Properties {
        [MaterialToggle] _AutoTime ("AutoTime", Float ) = 0
        _AutoTimeSpeed ("AutoTime Speed", Float ) = 1
        _AnimationTime ("Animation Time", Float ) = 0
        _AnimationJitterTime ("Animation JitterTime", Range(0, 1)) = 0
        [MaterialToggle] _AnimationInvertDirection ("Animation InvertDirection", Float ) = 0
        _ShapeThickness ("Shape Thickness", Float ) = 1
        _ShapeThicknessProfile ("Shape ThicknessProfile", 2D) = "white" {}
        _ShapeThicknessModifier ("Shape ThicknessModifier", 2D) = "black" {}
        _ShapeThicknessModifierValue ("Shape ThicknessModifierValue", Float ) = 2
        [MaterialToggle] _ThicknessOverTime ("Thickness OverTime", Float ) = 0
        _ThicknessModifierOffset ("ThicknessModifier Offset", Float ) = 0
        _ShapeLengthProfile ("Shape LengthProfile", 2D) = "white" {}
        _ShapeCompensateDistance ("Shape CompensateDistance", Range(0, 1)) = 0.5
        [MaterialToggle] _ShapeCroptoStartEnd ("Shape Crop to Start/End", Float ) = 0
        _ShapeStart ("Shape Start", Range(0, 1)) = 0
        _ShapeEnd ("Shape End", Range(0, 1)) = 1
        _ColorRamp ("ColorRamp", 2D) = "white" {}
        _ColorRampOffset ("ColorRamp Offset", Float ) = 0
        [MaterialToggle] _ColorOverTime ("Color OverTime", Float ) = 0
        _ColorHue ("Color Hue", Float ) = 0
        _ColorSaturation ("Color Saturation", Float ) = 0.9
        _ColorMultiplier ("Color Multiplier", Float ) = 1
        _ColorJitterHue ("Color JitterHue", Range(0, 1)) = 0
        _ColorJitterSaturation ("Color JitterSaturation", Range(0, 1)) = 0
        _ColorTint ("ColorTint", Color) = (1,1,1,1)
        _Impulse ("Impulse", 2D) = "black" {}
        [MaterialToggle] _ImpulseOverTime ("Impulse OverTime", Float ) = 0
        _ImpulseOffset ("Impulse Offset", Float ) = 0
        _ImpulseColor ("Impulse Color", Color) = (0,1,1,1)
        _ImpulseMultiplier ("Impulse Multiplier", Float ) = 5
        [MaterialToggle] _FadeIn ("FadeIn", Float ) = 0
        _FadeInDistance ("FadeIn Distance", Float ) = 0.1
        [MaterialToggle] _FadeOut ("FadeOut", Float ) = 0
        _FadeOutDistance ("FadeOut Distance", Float ) = 10
        _FadeOutExponent ("FadeOut Exponent", Float ) = 1
        [MaterialToggle] _HideCorners ("HideCorners", Float ) = 0
        _HideCornersSize ("HideCorners Size", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _ShapeThickness;
            uniform sampler2D _ShapeThicknessProfile; uniform float4 _ShapeThicknessProfile_ST;
            uniform sampler2D _Impulse; uniform float4 _Impulse_ST;
            uniform float _ColorMultiplier;
            uniform float _ShapeCompensateDistance;
            uniform float _ColorHue;
            uniform float _ColorSaturation;
            uniform float _AnimationJitterTime;
            uniform float4 _ImpulseColor;
            uniform fixed _AnimationInvertDirection;
            uniform float _ImpulseMultiplier;
            uniform float _AnimationTime;
            uniform sampler2D _ColorRamp; uniform float4 _ColorRamp_ST;
            uniform float _ColorJitterHue;
            uniform float _ColorJitterSaturation;
            uniform fixed _AutoTime;
            uniform fixed _ColorOverTime;
            uniform float _ShapeStart;
            uniform float _ShapeEnd;
            uniform sampler2D _ShapeLengthProfile; uniform float4 _ShapeLengthProfile_ST;
            uniform float _FadeInDistance;
            uniform fixed _FadeIn;
            uniform float _AutoTimeSpeed;
            uniform fixed _HideCorners;
            uniform float _HideCornersSize;
            uniform float _ColorRampOffset;
            uniform sampler2D _ShapeThicknessModifier; uniform float4 _ShapeThicknessModifier_ST;
            uniform float _ShapeThicknessModifierValue;
            uniform fixed _ThicknessOverTime;
            uniform float _ThicknessModifierOffset;
            uniform float _FadeOutDistance;
            uniform float _FadeOutExponent;
            uniform fixed _FadeOut;
            uniform fixed _ShapeCroptoStartEnd;
            uniform fixed _ImpulseOverTime;
            uniform float _ImpulseOffset;
            uniform float4 _ColorTint;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                float3 node_5308 = normalize(v.normal);
                float3 node_3487 = normalize(mul( unity_WorldToObject, float4(normalize((_WorldSpaceCameraPos-mul(unity_ObjectToWorld, v.vertex).rgb)),0) ).xyz.rgb);
                float4 node_6437 = _Time + _TimeEditor;
                float node_9662 = (lerp( _AnimationTime, (node_6437.a*_AutoTimeSpeed), _AutoTime )*0.1);
                float node_501 = lerp( (o.uv0.r+node_9662), (o.uv0.r-node_9662), _AnimationInvertDirection );
                float node_6356 = lerp(node_501,(node_501+o.vertexColor.g),_AnimationJitterTime);
                float node_1338 = frac(node_6356);
                float node_4109 = 0.5;
                float2 node_1905 = float2((lerp( o.vertexColor.g, node_1338, _ThicknessOverTime )+(_ThicknessModifierOffset*0.1)),node_4109);
                float4 _ShapeThicknessModifier_var = tex2Dlod(_ShapeThicknessModifier,float4(TRANSFORM_TEX(node_1905, _ShapeThicknessModifier),0.0,0));
                float node_9926 = 0.0;
                float node_2206 = 1.0;
                float3 node_8375 = ((normalize(cross(node_5308,node_3487))*(float3(1,1,1)/objScale))*(o.uv0.g*2.0+-1.0)*0.003*_ShapeThickness*(node_2206 + ( (_ShapeThicknessModifier_var.r - node_9926) * (_ShapeThicknessModifierValue - node_2206) ) / (node_2206 - node_9926)));
                float node_2510 = length((mul(unity_ObjectToWorld, v.vertex).rgb-_WorldSpaceCameraPos));
                v.vertex.xyz += lerp(node_8375,(node_8375*node_2510),_ShapeCompensateDistance);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float3 recipObjScale = float3( length(unity_WorldToObject[0].xyz), length(unity_WorldToObject[1].xyz), length(unity_WorldToObject[2].xyz) );
                float3 objScale = 1.0/recipObjScale;
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float node_4010 = 1.0;
                float node_9 = (node_4010 + ( ((1.0 - i.uv0.r) - _ShapeStart) * (0.0 - node_4010) ) / (_ShapeEnd - _ShapeStart));
                float node_9638 = (node_9*1.0+0.0);
                float node_1693 = (1.0 - (step(node_9638,0.0)+step(1.0,node_9638)));
                clip(lerp(1.0,node_1693,_ShapeCroptoStartEnd) - 0.5);
                float4 node_6437 = _Time + _TimeEditor;
                float node_9662 = (lerp( _AnimationTime, (node_6437.a*_AutoTimeSpeed), _AutoTime )*0.1);
                float node_501 = lerp( (i.uv0.r+node_9662), (i.uv0.r-node_9662), _AnimationInvertDirection );
                float node_6356 = lerp(node_501,(node_501+i.vertexColor.g),_AnimationJitterTime);
                float node_1338 = frac(node_6356);
                float node_4109 = 0.5;
                float2 node_7566 = float2((lerp( i.vertexColor.g, node_1338, _ColorOverTime )+(_ColorRampOffset*0.1)),node_4109);
                float4 _ColorRamp_var = tex2D(_ColorRamp,TRANSFORM_TEX(node_7566, _ColorRamp));
                float4 node_8344_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_8344_p = lerp(float4(float4(_ColorRamp_var.rgb,0.0).zy, node_8344_k.wz), float4(float4(_ColorRamp_var.rgb,0.0).yz, node_8344_k.xy), step(float4(_ColorRamp_var.rgb,0.0).z, float4(_ColorRamp_var.rgb,0.0).y));
                float4 node_8344_q = lerp(float4(node_8344_p.xyw, float4(_ColorRamp_var.rgb,0.0).x), float4(float4(_ColorRamp_var.rgb,0.0).x, node_8344_p.yzx), step(node_8344_p.x, float4(_ColorRamp_var.rgb,0.0).x));
                float node_8344_d = node_8344_q.x - min(node_8344_q.w, node_8344_q.y);
                float node_8344_e = 1.0e-10;
                float3 node_8344 = float3(abs(node_8344_q.z + (node_8344_q.w - node_8344_q.y) / (6.0 * node_8344_d + node_8344_e)), node_8344_d / (node_8344_q.x + node_8344_e), node_8344_q.x);;
                float node_8949 = i.vertexColor.g;
                float2 node_8911 = float2(lerp( (i.uv0.r+(_ImpulseOffset*0.1)+i.vertexColor.g), node_6356, _ImpulseOverTime ),node_4109);
                float4 _Impulse_var = tex2D(_Impulse,TRANSFORM_TEX(node_8911, _Impulse));
                float4 _ShapeThicknessProfile_var = tex2D(_ShapeThicknessProfile,TRANSFORM_TEX(i.uv0, _ShapeThicknessProfile));
                float2 node_5927 = float2((1.0 - node_9),0.5);
                float4 _ShapeLengthProfile_var = tex2D(_ShapeLengthProfile,TRANSFORM_TEX(node_5927, _ShapeLengthProfile));
                float3 node_7397 = ((((lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(((node_8344.r+_ColorHue)+(node_8949*_ColorJitterHue))+float3(0.0,-1.0/3.0,1.0/3.0)))-1),((node_8344.g*_ColorSaturation)+((node_8949*2.0+-1.0)*_ColorJitterSaturation)))*(node_8344.b*_ColorMultiplier))*_ColorTint.rgb)+(_ImpulseColor.rgb*(_Impulse_var.r*_ImpulseMultiplier)))*(_ShapeThicknessProfile_var.r*lerp(_ShapeLengthProfile_var.r,(_ShapeLengthProfile_var.r*node_1693),_ShapeCroptoStartEnd))*i.vertexColor.a);
                float node_2510 = length((i.posWorld.rgb-_WorldSpaceCameraPos));
                float node_7313 = node_2510;
                float node_5636 = (_ProjectionParams.g+0.02);
                float node_5277 = 0.0;
                float3 _FadeIn_var = lerp( node_7397, (node_7397*saturate((node_5277 + ( (node_7313 - node_5636) * (1.0 - node_5277) ) / ((node_5636+_FadeInDistance) - node_5636)))), _FadeIn );
                float node_9382 = 0.0;
                float3 _FadeOut_var = lerp( _FadeIn_var, (_FadeIn_var*pow((1.0 - saturate((node_9382 + ( (node_7313 - node_9382) * (1.0 - node_9382) ) / (_FadeOutDistance - node_9382)))),_FadeOutExponent)), _FadeOut );
                float3 node_5308 = normalize(i.normalDir);
                float3 node_3487 = normalize(mul( unity_WorldToObject, float4(normalize((_WorldSpaceCameraPos-i.posWorld.rgb)),0) ).xyz.rgb);
                float node_3865 = (1.0 - _HideCornersSize);
                float node_3086 = 1.0;
                float3 emissive = clamp(lerp( _FadeOut_var, (_FadeOut_var*saturate((node_3086 + ( (abs(dot(node_5308,node_3487)) - node_3865) * (0.0 - node_3086) ) / (node_3086 - node_3865)))), _HideCorners ),0,10000);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
