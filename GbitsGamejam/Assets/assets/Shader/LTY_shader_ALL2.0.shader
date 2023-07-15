// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LTY/ShaderNew/ALL2.0"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][Enum(Less or Equal,4,Always,8)]_ZTestMode("深度测试", Float) = 4
		[Enum(AlphaBlend,10,Additive,1)]_Dst("材质模式", Float) = 1
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("剔除模式", Float) = 0
		[Header(MainTex)]_maintex("maintex", 2D) = "white" {}
		[HDR]_Maincolor("Maincolor", Color) = (1,1,1,1)
		[Enum(R,0,A,1)]_A_R("A_R", Float) = 0
		[Enum(OFF,0,ON,1)]_one_UV("one_UV", Float) = 0
		[Enum(Repeat,0,Clmap,1)]_MianClamp("MianClamp", Float) = 0
		_soft("soft", Float) = 0
		_mainRotator("mainRotator", Float) = 0
		_Main_U_Speed("Main_U_Speed", Float) = 0
		_Main_V_Speed("Main_V_Speed", Float) = 0
		[Header(GAM)]_Gam("Gam", 2D) = "white" {}
		_GAMRotator("GAMRotator", Float) = 0
		[Header(MASKTEX)]_MASKTEX("MASKTEX", 2D) = "white" {}
		[Enum(Repeat,0,Clmap,1)]_MaskClamp("MaskClamp", Float) = 0
		_MASKRotator("MASKRotator", Float) = 0
		_MASK_u_speed("MASK_u_speed", Float) = 0
		_MASK_v_speed("MASK_v_speed", Float) = 0
		[Header(DissovleTex)]_DissovleTex("DissovleTex", 2D) = "white" {}
		[Toggle(_USE_DISSLOVE_ON)] _use_disslove("use_disslove", Float) = 0
		[Enum(OFF,0,ON,1)]_DissSC("Diss,S/C", Float) = 0
		_smooth("smooth", Range( 0.5 , 1)) = 0.5
		_Disspower("Disspower", Float) = 0
		_Dissovle_U_speed("Dissovle_U_speed", Float) = 0
		_Dissovle_V_speed("Dissovle_V_speed", Float) = 0
		[Header(NIUQU_Tex)]_NIUQU_Tex("NIUQU_Tex", 2D) = "white" {}
		[Enum(OFF,0,ON,1)]_NIUQUONOFF("NIUQU,ON/OFF", Float) = 0
		_NIUQU_Power("NIUQU_Power", Float) = 0
		_Niuqu_U_speed("Niuqu_U_speed", Float) = 0
		_Niuqu_V_speed("Niuqu_V_speed", Float) = 0
		[HDR][Header(Fresnel)]_FFFREncolor("FFFREncolor", Color) = (1,1,1,1)
		[Toggle(_FRE_ONOFF_ON)] _FRE_ONOFF("FRE_ON/OFF", Float) = 0
		[Enum(outside,0,inside,1)]_FRE_BF("FRE_B/F", Float) = 0
		_fre_scale("fre_scale", Float) = 1
		_fre_power("fre_power", Float) = 5
		[Header(Wpo_Tex)]_wpo_tex("wpo_tex", 2D) = "white" {}
		[Toggle(_ONOFF__VERTEX_ON)] _ONOFF__vertex("ON/OFF__vertex", Float) = 0
		[Enum(OFF,0,ON,1)]_IS_vertex("IS_vertex", Float) = 0
		_WPO_tex("WPO_tex", Float) = 0
		_Vertexpower("Vertexpower", Vector) = (1,1,1,0)
		_WPO_U_Speed("WPO_U_Speed", Float) = 0
		[ASEEnd]_WPO_V_Speed("WPO_V_Speed", Float) = 0

	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }

		Cull Off
		HLSLINCLUDE
		#pragma target 2.0
		ENDHLSL

		
		Pass
		{
			Name "Sprite Lit"
			Tags { "LightMode"="Universal2D" }
			
			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define ASE_SRP_VERSION 999999
			#define REQUIRE_DEPTH_TEXTURE 1

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_0
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_1
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_2
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_3

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITELIT

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
			
			#if USE_SHAPE_LIGHT_TYPE_0
			SHAPE_LIGHT(0)
			#endif

			#if USE_SHAPE_LIGHT_TYPE_1
			SHAPE_LIGHT(1)
			#endif

			#if USE_SHAPE_LIGHT_TYPE_2
			SHAPE_LIGHT(2)
			#endif

			#if USE_SHAPE_LIGHT_TYPE_3
			SHAPE_LIGHT(3)
			#endif

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature _ONOFF__VERTEX_ON
			#pragma shader_feature _FRE_ONOFF_ON
			#pragma shader_feature _USE_DISSLOVE_ON


			sampler2D _wpo_tex;
			sampler2D _maintex;
			sampler2D _NIUQU_Tex;
			sampler2D _DissovleTex;
			sampler2D _Gam;
			uniform float4 _CameraDepthTexture_TexelSize;
			sampler2D _MASKTEX;
			CBUFFER_START( UnityPerMaterial )
			float4 _Gam_ST;
			float4 _MASKTEX_ST;
			float4 _FFFREncolor;
			float4 _NIUQU_Tex_ST;
			float4 _wpo_tex_ST;
			float4 _Vertexpower;
			float4 _Maincolor;
			float4 _DissovleTex_ST;
			float4 _maintex_ST;
			float _smooth;
			float _Dissovle_U_speed;
			float _Dissovle_V_speed;
			float _Disspower;
			float _ZTestMode;
			float _FRE_BF;
			float _GAMRotator;
			float _soft;
			float _A_R;
			float _MASK_u_speed;
			float _MASK_v_speed;
			float _DissSC;
			float _fre_scale;
			float _mainRotator;
			float _MianClamp;
			float _Dst;
			float _CullMode;
			float _WPO_tex;
			float _IS_vertex;
			float _WPO_U_Speed;
			float _WPO_V_Speed;
			float _fre_power;
			float _Main_U_Speed;
			float _one_UV;
			float _NIUQU_Power;
			float _Niuqu_U_speed;
			float _Niuqu_V_speed;
			float _NIUQUONOFF;
			float _MASKRotator;
			float _Main_V_Speed;
			float _MaskClamp;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float4 screenPosition : TEXCOORD2;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D(_AlphaTex); SAMPLER(sampler_AlphaTex);
				float _EnableAlphaTexture;
			#endif

			
			VertexOutput vert ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 temp_cast_0 = (0.0).xxxx;
				float4 texCoord395 = v.ase_texcoord1;
				texCoord395.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float lerpResult398 = lerp( _WPO_tex , texCoord395.y , _IS_vertex);
				float2 appendResult386 = (float2(_WPO_U_Speed , _WPO_V_Speed));
				float2 uv_wpo_tex = v.uv0.xy * _wpo_tex_ST.xy + _wpo_tex_ST.zw;
				float2 panner390 = ( 1.0 * _Time.y * appendResult386 + uv_wpo_tex);
				#ifdef _ONOFF__VERTEX_ON
				float4 staticSwitch408 = ( lerpResult398 * float4( v.normal , 0.0 ) * tex2Dlod( _wpo_tex, float4( panner390, 0, 0.0) ) * _Vertexpower );
				#else
				float4 staticSwitch408 = temp_cast_0;
				#endif
				float4 Vertex117 = staticSwitch408;
				
				float3 ase_worldPos = mul(GetObjectToWorldMatrix(), v.vertex).xyz;
				o.ase_texcoord4.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.normal);
				o.ase_texcoord5.xyz = ase_worldNormal;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord6 = screenPos;
				
				o.ase_color = v.color;
				o.ase_texcoord3 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord4.w = 0;
				o.ase_texcoord5.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = Vertex117.rgb;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;
				o.screenPosition = ComputeScreenPos( o.clipPos, _ProjectionParams.x );
				return o;
			}

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 appendResult298 = (float2(( _Main_U_Speed * _TimeParameters.x ) , ( _TimeParameters.x * _Main_V_Speed )));
				float2 uv_maintex = IN.texCoord0.xy * _maintex_ST.xy + _maintex_ST.zw;
				float4 texCoord288 = IN.ase_texcoord3;
				texCoord288.xy = IN.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult301 = (float2(texCoord288.z , texCoord288.w));
				float2 lerpResult308 = lerp( ( appendResult298 + uv_maintex ) , ( uv_maintex + appendResult301 ) , _one_UV);
				float2 appendResult283 = (float2(_Niuqu_U_speed , _Niuqu_V_speed));
				float2 uv_NIUQU_Tex = IN.texCoord0.xy * _NIUQU_Tex_ST.xy + _NIUQU_Tex_ST.zw;
				float2 panner286 = ( 1.0 * _Time.y * appendResult283 + uv_NIUQU_Tex);
				float lerpResult309 = lerp( 0.0 , ( _NIUQU_Power * (-0.5 + (tex2D( _NIUQU_Tex, panner286 ).r - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) ) , _NIUQUONOFF);
				float2 ONE214 = ( lerpResult308 + lerpResult309 );
				float cos321 = cos( ( ( _mainRotator * PI ) / 180.0 ) );
				float sin321 = sin( ( ( _mainRotator * PI ) / 180.0 ) );
				float2 rotator321 = mul( ONE214 - float2( 0.5,0.5 ) , float2x2( cos321 , -sin321 , sin321 , cos321 )) + float2( 0.5,0.5 );
				float2 lerpResult411 = lerp( rotator321 , saturate( rotator321 ) , _MianClamp);
				float4 tex2DNode1 = tex2D( _maintex, lerpResult411 );
				float3 ase_worldPos = IN.ase_texcoord4.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord5.xyz;
				float dotResult343 = dot( ase_worldViewDir , ase_worldNormal );
				float temp_output_350_0 = saturate( abs( dotResult343 ) );
				float lerpResult370 = lerp( ( pow( ( 1.0 - temp_output_350_0 ) , _fre_power ) * ( _fre_scale * 1 ) ) , temp_output_350_0 , _FRE_BF);
				#ifdef _FRE_ONOFF_ON
				float staticSwitch372 = lerpResult370;
				#else
				float staticSwitch372 = 1.0;
				#endif
				float2 appendResult342 = (float2(_Dissovle_U_speed , _Dissovle_V_speed));
				float2 uv_DissovleTex = IN.texCoord0.xy * _DissovleTex_ST.xy + _DissovleTex_ST.zw;
				float2 panner346 = ( 1.0 * _Time.y * appendResult342 + uv_DissovleTex);
				float five300 = texCoord288.x;
				float lerpResult351 = lerp( _Disspower , five300 , _DissSC);
				float smoothstepResult368 = smoothstep( ( 1.0 - _smooth ) , _smooth , saturate( ( ( tex2D( _DissovleTex, panner346 ).r + 1.0 ) - ( lerpResult351 * 2.0 ) ) ));
				#ifdef _USE_DISSLOVE_ON
				float staticSwitch371 = smoothstepResult368;
				#else
				float staticSwitch371 = 1.0;
				#endif
				float4 four222 = ( staticSwitch372 * _FFFREncolor * staticSwitch371 );
				float2 uv_Gam = IN.texCoord0.xy * _Gam_ST.xy + _Gam_ST.zw;
				float cos399 = cos( ( ( _GAMRotator * PI ) / 180.0 ) );
				float sin399 = sin( ( ( _GAMRotator * PI ) / 180.0 ) );
				float2 rotator399 = mul( uv_Gam - float2( 0.5,0.5 ) , float2x2( cos399 , -sin399 , sin399 , cos399 )) + float2( 0.5,0.5 );
				float4 Gam258 = tex2D( _Gam, rotator399 );
				float4 screenPos = IN.ase_texcoord6;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth393 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth393 = abs( ( screenDepth393 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _soft ) );
				float soft219 = saturate( distanceDepth393 );
				float lerpResult409 = lerp( tex2DNode1.r , tex2DNode1.a , _A_R);
				float Three217 = ( staticSwitch372 * _FFFREncolor.a * staticSwitch371 );
				float2 appendResult332 = (float2(_MASK_u_speed , _MASK_v_speed));
				float2 uv_MASKTEX = IN.texCoord0.xy * _MASKTEX_ST.xy + _MASKTEX_ST.zw;
				float2 panner334 = ( 1.0 * _Time.y * appendResult332 + uv_MASKTEX);
				float cos335 = cos( ( ( _MASKRotator * PI ) / 180.0 ) );
				float sin335 = sin( ( ( _MASKRotator * PI ) / 180.0 ) );
				float2 rotator335 = mul( panner334 - float2( 0.5,0.5 ) , float2x2( cos335 , -sin335 , sin335 , cos335 )) + float2( 0.5,0.5 );
				float2 lerpResult413 = lerp( rotator335 , saturate( rotator335 ) , _MaskClamp);
				float two215 = tex2D( _MASKTEX, lerpResult413 ).r;
				
				float4 Color = ( ( IN.ase_color * _Maincolor * tex2DNode1 * four222 * Gam258 * soft219 ) * ( IN.ase_color.a * _Maincolor.a * lerpResult409 * Three217 * soft219 * two215 ) );
				float Mask = 1;
				float3 Normal = float3( 0, 0, 1 );

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D(_AlphaTex, sampler_AlphaTex, IN.texCoord0.xy);
					Color.a = lerp ( Color.a, alpha.r, _EnableAlphaTexture);
				#endif
				
				Color *= IN.color;

				return CombinedShapeLightShared( Color, Mask, IN.screenPosition.xy / IN.screenPosition.w );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "Sprite Normal"
			Tags { "LightMode"="NormalsRendering" }
			
			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define ASE_SRP_VERSION 999999
			#define REQUIRE_DEPTH_TEXTURE 1

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITENORMAL

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
			
			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature _ONOFF__VERTEX_ON
			#pragma shader_feature _FRE_ONOFF_ON
			#pragma shader_feature _USE_DISSLOVE_ON


			sampler2D _wpo_tex;
			sampler2D _maintex;
			sampler2D _NIUQU_Tex;
			sampler2D _DissovleTex;
			sampler2D _Gam;
			uniform float4 _CameraDepthTexture_TexelSize;
			sampler2D _MASKTEX;
			CBUFFER_START( UnityPerMaterial )
			float4 _Gam_ST;
			float4 _MASKTEX_ST;
			float4 _FFFREncolor;
			float4 _NIUQU_Tex_ST;
			float4 _wpo_tex_ST;
			float4 _Vertexpower;
			float4 _Maincolor;
			float4 _DissovleTex_ST;
			float4 _maintex_ST;
			float _smooth;
			float _Dissovle_U_speed;
			float _Dissovle_V_speed;
			float _Disspower;
			float _ZTestMode;
			float _FRE_BF;
			float _GAMRotator;
			float _soft;
			float _A_R;
			float _MASK_u_speed;
			float _MASK_v_speed;
			float _DissSC;
			float _fre_scale;
			float _mainRotator;
			float _MianClamp;
			float _Dst;
			float _CullMode;
			float _WPO_tex;
			float _IS_vertex;
			float _WPO_U_Speed;
			float _WPO_V_Speed;
			float _fre_power;
			float _Main_U_Speed;
			float _one_UV;
			float _NIUQU_Power;
			float _Niuqu_U_speed;
			float _Niuqu_V_speed;
			float _NIUQUONOFF;
			float _MASKRotator;
			float _Main_V_Speed;
			float _MaskClamp;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float3 normalWS : TEXCOORD2;
				float4 tangentWS : TEXCOORD3;
				float3 bitangentWS : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				float4 ase_texcoord7 : TEXCOORD7;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			
			VertexOutput vert ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float4 temp_cast_0 = (0.0).xxxx;
				float4 texCoord395 = v.ase_texcoord1;
				texCoord395.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float lerpResult398 = lerp( _WPO_tex , texCoord395.y , _IS_vertex);
				float2 appendResult386 = (float2(_WPO_U_Speed , _WPO_V_Speed));
				float2 uv_wpo_tex = v.uv0.xy * _wpo_tex_ST.xy + _wpo_tex_ST.zw;
				float2 panner390 = ( 1.0 * _Time.y * appendResult386 + uv_wpo_tex);
				#ifdef _ONOFF__VERTEX_ON
				float4 staticSwitch408 = ( lerpResult398 * float4( v.normal , 0.0 ) * tex2Dlod( _wpo_tex, float4( panner390, 0, 0.0) ) * _Vertexpower );
				#else
				float4 staticSwitch408 = temp_cast_0;
				#endif
				float4 Vertex117 = staticSwitch408;
				
				float3 ase_worldPos = mul(GetObjectToWorldMatrix(), v.vertex).xyz;
				o.ase_texcoord6.xyz = ase_worldPos;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord7 = screenPos;
				
				o.ase_texcoord5 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord6.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = Vertex117.rgb;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;

				float3 normalWS = TransformObjectToWorldNormal( v.normal );
				o.normalWS = NormalizeNormalPerVertex( normalWS );
				float4 tangentWS = float4( TransformObjectToWorldDir( v.tangent.xyz ), v.tangent.w );
				o.tangentWS = normalize( tangentWS );
				o.bitangentWS = cross( normalWS, tangentWS.xyz ) * tangentWS.w;
				return o;
			}

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 appendResult298 = (float2(( _Main_U_Speed * _TimeParameters.x ) , ( _TimeParameters.x * _Main_V_Speed )));
				float2 uv_maintex = IN.texCoord0.xy * _maintex_ST.xy + _maintex_ST.zw;
				float4 texCoord288 = IN.ase_texcoord5;
				texCoord288.xy = IN.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult301 = (float2(texCoord288.z , texCoord288.w));
				float2 lerpResult308 = lerp( ( appendResult298 + uv_maintex ) , ( uv_maintex + appendResult301 ) , _one_UV);
				float2 appendResult283 = (float2(_Niuqu_U_speed , _Niuqu_V_speed));
				float2 uv_NIUQU_Tex = IN.texCoord0.xy * _NIUQU_Tex_ST.xy + _NIUQU_Tex_ST.zw;
				float2 panner286 = ( 1.0 * _Time.y * appendResult283 + uv_NIUQU_Tex);
				float lerpResult309 = lerp( 0.0 , ( _NIUQU_Power * (-0.5 + (tex2D( _NIUQU_Tex, panner286 ).r - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) ) , _NIUQUONOFF);
				float2 ONE214 = ( lerpResult308 + lerpResult309 );
				float cos321 = cos( ( ( _mainRotator * PI ) / 180.0 ) );
				float sin321 = sin( ( ( _mainRotator * PI ) / 180.0 ) );
				float2 rotator321 = mul( ONE214 - float2( 0.5,0.5 ) , float2x2( cos321 , -sin321 , sin321 , cos321 )) + float2( 0.5,0.5 );
				float2 lerpResult411 = lerp( rotator321 , saturate( rotator321 ) , _MianClamp);
				float4 tex2DNode1 = tex2D( _maintex, lerpResult411 );
				float3 ase_worldPos = IN.ase_texcoord6.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float dotResult343 = dot( ase_worldViewDir , IN.normalWS );
				float temp_output_350_0 = saturate( abs( dotResult343 ) );
				float lerpResult370 = lerp( ( pow( ( 1.0 - temp_output_350_0 ) , _fre_power ) * ( _fre_scale * 1 ) ) , temp_output_350_0 , _FRE_BF);
				#ifdef _FRE_ONOFF_ON
				float staticSwitch372 = lerpResult370;
				#else
				float staticSwitch372 = 1.0;
				#endif
				float2 appendResult342 = (float2(_Dissovle_U_speed , _Dissovle_V_speed));
				float2 uv_DissovleTex = IN.texCoord0.xy * _DissovleTex_ST.xy + _DissovleTex_ST.zw;
				float2 panner346 = ( 1.0 * _Time.y * appendResult342 + uv_DissovleTex);
				float five300 = texCoord288.x;
				float lerpResult351 = lerp( _Disspower , five300 , _DissSC);
				float smoothstepResult368 = smoothstep( ( 1.0 - _smooth ) , _smooth , saturate( ( ( tex2D( _DissovleTex, panner346 ).r + 1.0 ) - ( lerpResult351 * 2.0 ) ) ));
				#ifdef _USE_DISSLOVE_ON
				float staticSwitch371 = smoothstepResult368;
				#else
				float staticSwitch371 = 1.0;
				#endif
				float4 four222 = ( staticSwitch372 * _FFFREncolor * staticSwitch371 );
				float2 uv_Gam = IN.texCoord0.xy * _Gam_ST.xy + _Gam_ST.zw;
				float cos399 = cos( ( ( _GAMRotator * PI ) / 180.0 ) );
				float sin399 = sin( ( ( _GAMRotator * PI ) / 180.0 ) );
				float2 rotator399 = mul( uv_Gam - float2( 0.5,0.5 ) , float2x2( cos399 , -sin399 , sin399 , cos399 )) + float2( 0.5,0.5 );
				float4 Gam258 = tex2D( _Gam, rotator399 );
				float4 screenPos = IN.ase_texcoord7;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth393 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth393 = abs( ( screenDepth393 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _soft ) );
				float soft219 = saturate( distanceDepth393 );
				float lerpResult409 = lerp( tex2DNode1.r , tex2DNode1.a , _A_R);
				float Three217 = ( staticSwitch372 * _FFFREncolor.a * staticSwitch371 );
				float2 appendResult332 = (float2(_MASK_u_speed , _MASK_v_speed));
				float2 uv_MASKTEX = IN.texCoord0.xy * _MASKTEX_ST.xy + _MASKTEX_ST.zw;
				float2 panner334 = ( 1.0 * _Time.y * appendResult332 + uv_MASKTEX);
				float cos335 = cos( ( ( _MASKRotator * PI ) / 180.0 ) );
				float sin335 = sin( ( ( _MASKRotator * PI ) / 180.0 ) );
				float2 rotator335 = mul( panner334 - float2( 0.5,0.5 ) , float2x2( cos335 , -sin335 , sin335 , cos335 )) + float2( 0.5,0.5 );
				float2 lerpResult413 = lerp( rotator335 , saturate( rotator335 ) , _MaskClamp);
				float two215 = tex2D( _MASKTEX, lerpResult413 ).r;
				
				float4 Color = ( ( IN.color * _Maincolor * tex2DNode1 * four222 * Gam258 * soft219 ) * ( IN.color.a * _Maincolor.a * lerpResult409 * Three217 * soft219 * two215 ) );
				float3 Normal = float3( 0, 0, 1 );
				
				Color *= IN.color;

				return NormalsRenderingShared( Color, Normal, IN.tangentWS.xyz, IN.bitangentWS, IN.normalWS);
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "Sprite Forward"
			Tags { "LightMode"="UniversalForward" }

			Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define ASE_SRP_VERSION 999999
			#define REQUIRE_DEPTH_TEXTURE 1

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITEFORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature _ONOFF__VERTEX_ON
			#pragma shader_feature _FRE_ONOFF_ON
			#pragma shader_feature _USE_DISSLOVE_ON


			sampler2D _wpo_tex;
			sampler2D _maintex;
			sampler2D _NIUQU_Tex;
			sampler2D _DissovleTex;
			sampler2D _Gam;
			uniform float4 _CameraDepthTexture_TexelSize;
			sampler2D _MASKTEX;
			CBUFFER_START( UnityPerMaterial )
			float4 _Gam_ST;
			float4 _MASKTEX_ST;
			float4 _FFFREncolor;
			float4 _NIUQU_Tex_ST;
			float4 _wpo_tex_ST;
			float4 _Vertexpower;
			float4 _Maincolor;
			float4 _DissovleTex_ST;
			float4 _maintex_ST;
			float _smooth;
			float _Dissovle_U_speed;
			float _Dissovle_V_speed;
			float _Disspower;
			float _ZTestMode;
			float _FRE_BF;
			float _GAMRotator;
			float _soft;
			float _A_R;
			float _MASK_u_speed;
			float _MASK_v_speed;
			float _DissSC;
			float _fre_scale;
			float _mainRotator;
			float _MianClamp;
			float _Dst;
			float _CullMode;
			float _WPO_tex;
			float _IS_vertex;
			float _WPO_U_Speed;
			float _WPO_V_Speed;
			float _fre_power;
			float _Main_U_Speed;
			float _one_UV;
			float _NIUQU_Power;
			float _Niuqu_U_speed;
			float _Niuqu_V_speed;
			float _NIUQUONOFF;
			float _MASKRotator;
			float _Main_V_Speed;
			float _MaskClamp;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D( _AlphaTex ); SAMPLER( sampler_AlphaTex );
				float _EnableAlphaTexture;
			#endif

			
			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float4 temp_cast_0 = (0.0).xxxx;
				float4 texCoord395 = v.ase_texcoord1;
				texCoord395.xy = v.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float lerpResult398 = lerp( _WPO_tex , texCoord395.y , _IS_vertex);
				float2 appendResult386 = (float2(_WPO_U_Speed , _WPO_V_Speed));
				float2 uv_wpo_tex = v.uv0.xy * _wpo_tex_ST.xy + _wpo_tex_ST.zw;
				float2 panner390 = ( 1.0 * _Time.y * appendResult386 + uv_wpo_tex);
				#ifdef _ONOFF__VERTEX_ON
				float4 staticSwitch408 = ( lerpResult398 * float4( v.normal , 0.0 ) * tex2Dlod( _wpo_tex, float4( panner390, 0, 0.0) ) * _Vertexpower );
				#else
				float4 staticSwitch408 = temp_cast_0;
				#endif
				float4 Vertex117 = staticSwitch408;
				
				float3 ase_worldPos = mul(GetObjectToWorldMatrix(), v.vertex).xyz;
				o.ase_texcoord3.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(v.normal);
				o.ase_texcoord4.xyz = ase_worldNormal;
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord5 = screenPos;
				
				o.ase_texcoord2 = v.ase_texcoord1;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.w = 0;
				o.ase_texcoord4.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = Vertex117.rgb;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( v.vertex.xyz );

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 appendResult298 = (float2(( _Main_U_Speed * _TimeParameters.x ) , ( _TimeParameters.x * _Main_V_Speed )));
				float2 uv_maintex = IN.texCoord0.xy * _maintex_ST.xy + _maintex_ST.zw;
				float4 texCoord288 = IN.ase_texcoord2;
				texCoord288.xy = IN.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult301 = (float2(texCoord288.z , texCoord288.w));
				float2 lerpResult308 = lerp( ( appendResult298 + uv_maintex ) , ( uv_maintex + appendResult301 ) , _one_UV);
				float2 appendResult283 = (float2(_Niuqu_U_speed , _Niuqu_V_speed));
				float2 uv_NIUQU_Tex = IN.texCoord0.xy * _NIUQU_Tex_ST.xy + _NIUQU_Tex_ST.zw;
				float2 panner286 = ( 1.0 * _Time.y * appendResult283 + uv_NIUQU_Tex);
				float lerpResult309 = lerp( 0.0 , ( _NIUQU_Power * (-0.5 + (tex2D( _NIUQU_Tex, panner286 ).r - 0.0) * (0.5 - -0.5) / (1.0 - 0.0)) ) , _NIUQUONOFF);
				float2 ONE214 = ( lerpResult308 + lerpResult309 );
				float cos321 = cos( ( ( _mainRotator * PI ) / 180.0 ) );
				float sin321 = sin( ( ( _mainRotator * PI ) / 180.0 ) );
				float2 rotator321 = mul( ONE214 - float2( 0.5,0.5 ) , float2x2( cos321 , -sin321 , sin321 , cos321 )) + float2( 0.5,0.5 );
				float2 lerpResult411 = lerp( rotator321 , saturate( rotator321 ) , _MianClamp);
				float4 tex2DNode1 = tex2D( _maintex, lerpResult411 );
				float3 ase_worldPos = IN.ase_texcoord3.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = IN.ase_texcoord4.xyz;
				float dotResult343 = dot( ase_worldViewDir , ase_worldNormal );
				float temp_output_350_0 = saturate( abs( dotResult343 ) );
				float lerpResult370 = lerp( ( pow( ( 1.0 - temp_output_350_0 ) , _fre_power ) * ( _fre_scale * 1 ) ) , temp_output_350_0 , _FRE_BF);
				#ifdef _FRE_ONOFF_ON
				float staticSwitch372 = lerpResult370;
				#else
				float staticSwitch372 = 1.0;
				#endif
				float2 appendResult342 = (float2(_Dissovle_U_speed , _Dissovle_V_speed));
				float2 uv_DissovleTex = IN.texCoord0.xy * _DissovleTex_ST.xy + _DissovleTex_ST.zw;
				float2 panner346 = ( 1.0 * _Time.y * appendResult342 + uv_DissovleTex);
				float five300 = texCoord288.x;
				float lerpResult351 = lerp( _Disspower , five300 , _DissSC);
				float smoothstepResult368 = smoothstep( ( 1.0 - _smooth ) , _smooth , saturate( ( ( tex2D( _DissovleTex, panner346 ).r + 1.0 ) - ( lerpResult351 * 2.0 ) ) ));
				#ifdef _USE_DISSLOVE_ON
				float staticSwitch371 = smoothstepResult368;
				#else
				float staticSwitch371 = 1.0;
				#endif
				float4 four222 = ( staticSwitch372 * _FFFREncolor * staticSwitch371 );
				float2 uv_Gam = IN.texCoord0.xy * _Gam_ST.xy + _Gam_ST.zw;
				float cos399 = cos( ( ( _GAMRotator * PI ) / 180.0 ) );
				float sin399 = sin( ( ( _GAMRotator * PI ) / 180.0 ) );
				float2 rotator399 = mul( uv_Gam - float2( 0.5,0.5 ) , float2x2( cos399 , -sin399 , sin399 , cos399 )) + float2( 0.5,0.5 );
				float4 Gam258 = tex2D( _Gam, rotator399 );
				float4 screenPos = IN.ase_texcoord5;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth393 = LinearEyeDepth(SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy ),_ZBufferParams);
				float distanceDepth393 = abs( ( screenDepth393 - LinearEyeDepth( ase_screenPosNorm.z,_ZBufferParams ) ) / ( _soft ) );
				float soft219 = saturate( distanceDepth393 );
				float lerpResult409 = lerp( tex2DNode1.r , tex2DNode1.a , _A_R);
				float Three217 = ( staticSwitch372 * _FFFREncolor.a * staticSwitch371 );
				float2 appendResult332 = (float2(_MASK_u_speed , _MASK_v_speed));
				float2 uv_MASKTEX = IN.texCoord0.xy * _MASKTEX_ST.xy + _MASKTEX_ST.zw;
				float2 panner334 = ( 1.0 * _Time.y * appendResult332 + uv_MASKTEX);
				float cos335 = cos( ( ( _MASKRotator * PI ) / 180.0 ) );
				float sin335 = sin( ( ( _MASKRotator * PI ) / 180.0 ) );
				float2 rotator335 = mul( panner334 - float2( 0.5,0.5 ) , float2x2( cos335 , -sin335 , sin335 , cos335 )) + float2( 0.5,0.5 );
				float2 lerpResult413 = lerp( rotator335 , saturate( rotator335 ) , _MaskClamp);
				float two215 = tex2D( _MASKTEX, lerpResult413 ).r;
				
				float4 Color = ( ( IN.color * _Maincolor * tex2DNode1 * four222 * Gam258 * soft219 ) * ( IN.color.a * _Maincolor.a * lerpResult409 * Three217 * soft219 * two215 ) );

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D( _AlphaTex, sampler_AlphaTex, IN.texCoord0.xy );
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture );
				#endif

				Color *= IN.color;

				return Color;
			}

			ENDHLSL
		}
		
	}
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18800
0;0;1706.667;997.6667;886.2205;1029.837;1.023574;True;False
Node;AmplifyShaderEditor.CommentaryNode;276;-2987.463,-1166.493;Inherit;False;1363.104;498.9412;扭曲;13;310;309;307;304;303;299;292;286;283;282;281;280;278;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;280;-2977.584,-929.0648;Inherit;False;Property;_Niuqu_V_speed;Niuqu_V_speed;30;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;281;-2975.144,-1004.034;Inherit;False;Property;_Niuqu_U_speed;Niuqu_U_speed;29;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;283;-2779.824,-978.3558;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;277;-2989.603,-1666.087;Inherit;False;1344.04;453.14;UV流动;14;308;306;305;302;301;300;298;297;293;289;288;287;285;284;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;282;-2979.589,-1125.442;Inherit;False;0;292;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;278;-2634.417,-935.5421;Inherit;False;585.5043;262.0287;映射;5;296;295;294;291;290;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;287;-2929.824,-1530.802;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;336;-3844.916,437.3795;Inherit;False;2252.001;531.4145;溶解;23;374;371;368;367;366;364;361;360;357;355;353;352;351;349;348;347;346;344;342;341;340;339;217;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;284;-2936.225,-1602.943;Inherit;False;Property;_Main_U_Speed;Main_U_Speed;10;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;285;-2936.218,-1458.536;Inherit;False;Property;_Main_V_Speed;Main_V_Speed;11;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;286;-2659.359,-1121.285;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;378;-3202.821,-150.4367;Inherit;False;1593.73;556.9348;菲尼尔;18;375;373;372;370;369;365;363;362;359;358;356;354;350;345;343;338;337;222;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;291;-2618.088,-892.9009;Inherit;False;Constant;_Float0;Float 0;41;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;339;-3832.71,785.2356;Inherit;False;Property;_Dissovle_V_speed;Dissovle_V_speed;25;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;338;-3144.096,-100.4366;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;293;-2682.416,-1585.136;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;337;-3148.709,59.32548;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;290;-2619.849,-820.3259;Inherit;False;Constant;_Float2;Float 2;41;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;294;-2455.948,-823.7765;Inherit;False;Constant;_Float1;Float 1;41;0;Create;True;0;0;0;False;0;False;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;292;-2473.137,-1129.707;Inherit;True;Property;_NIUQU_Tex;NIUQU_Tex;26;1;[Header];Create;True;1;NIUQU_Tex;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;288;-2973.396,-1388.448;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;340;-3826.595,699.0553;Inherit;False;Property;_Dissovle_U_speed;Dissovle_U_speed;24;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;295;-2455.222,-746.0923;Inherit;False;Constant;_Float10;Float 10;41;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;289;-2682.594,-1482.974;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;296;-2233.871,-855.4952;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;343;-2979.543,-110.2437;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;341;-3802.025,491.9929;Inherit;False;0;353;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;301;-2737.74,-1309.354;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;297;-2539.971,-1466.887;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;300;-2761.334,-1386.104;Inherit;False;five;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;342;-3624.18,684.9503;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;299;-2187.636,-1016.173;Inherit;False;Property;_NIUQU_Power;NIUQU_Power;28;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;298;-2535.707,-1555.938;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;304;-2019.32,-748.9233;Inherit;False;Property;_NIUQUONOFF;NIUQU,ON/OFF;27;1;[Enum];Create;True;0;2;OFF;0;ON;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;305;-2287.417,-1526.172;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;302;-2279.468,-1406.292;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;303;-2005.593,-972.3661;Inherit;False;Constant;_Float4;Float 4;43;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;347;-3412.123,750.6153;Inherit;False;300;five;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;307;-2024.403,-883.0859;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;306;-2287.856,-1288.875;Inherit;False;Property;_one_UV;one_UV;6;1;[Enum];Create;True;0;2;OFF;0;ON;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;311;-3051.371,-631.6;Inherit;False;1006.245;458.0202;MASK;15;215;335;325;327;413;414;334;324;331;332;323;333;329;330;322;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;348;-3411.679,671.8097;Inherit;False;Property;_Disspower;Disspower;23;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;346;-3555.99,503.1738;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;345;-2785.645,-99.70898;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;344;-3411.378,827.2977;Inherit;False;Property;_DissSC;Diss,S/C;21;1;[Enum];Create;True;0;2;OFF;0;ON;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;352;-3226.908,858.3678;Inherit;False;Constant;_Float7;Float 7;11;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;350;-2611.48,-105.0669;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;351;-3240.978,727.1977;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;312;-1992.634,-631.6931;Inherit;False;353.1464;462.3351;旋转;6;319;318;316;317;320;321;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;329;-3031.581,-390.994;Inherit;False;Property;_MASK_v_speed;MASK_v_speed;18;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;349;-3240.223,652.5835;Inherit;False;Constant;_Float3;Float 3;10;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;308;-2028.514,-1429.516;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;330;-3031.231,-466.0962;Inherit;False;Property;_MASK_u_speed;MASK_u_speed;17;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;322;-3026.796,-320.0094;Inherit;False;Property;_MASKRotator;MASKRotator;16;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;353;-3368.6,468.483;Inherit;True;Property;_DissovleTex;DissovleTex;19;1;[Header];Create;True;1;DissovleTex;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;309;-1836.978,-891.5655;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;332;-2848.948,-451.6427;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;323;-3028.05,-246.3203;Inherit;False;Constant;_Float11;Float 11;13;0;Create;True;0;0;0;False;0;False;180;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;331;-3035.311,-590.3092;Inherit;False;0;327;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;333;-2835.189,-313.4993;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;355;-3063.644,492.9502;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;354;-2929.999,157.3961;Inherit;False;Property;_fre_power;fre_power;35;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;316;-1974.436,-474.4632;Inherit;False;Property;_mainRotator;mainRotator;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;358;-2916.351,340.9777;Inherit;False;Property;_fre_scale;fre_scale;34;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;356;-2592.497,13.30281;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;357;-3056.048,744.7742;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;310;-1865,-1124.067;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;380;-2463.454,1123.277;Inherit;False;867.3103;379.8349;Comment;8;406;399;396;391;389;385;381;258;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;324;-2627.642,-280.4022;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;318;-1945.801,-271.8814;Inherit;False;Constant;_Float6;Float 6;13;0;Create;True;0;0;0;False;0;False;180;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;334;-2752.645,-586.0758;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;362;-2761.506,160.1481;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;317;-1821.62,-471.2045;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;361;-2817.052,752.7044;Inherit;False;Property;_smooth;smooth;22;0;Create;True;0;0;0;False;0;False;0.5;0.5;0.5;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;379;-3830.122,987.3759;Inherit;False;1349.064;513.0515;顶点偏移;16;408;405;404;402;401;398;397;395;394;392;390;387;386;383;382;117;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;360;-2846.1,495.103;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;214;-1514.585,-1115.013;Inherit;False;ONE;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleNode;359;-2739.184,302.1069;Inherit;False;1;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;381;-2450.709,1340.185;Inherit;False;Property;_GAMRotator;GAMRotator;13;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;382;-3818.937,1419.505;Inherit;False;Property;_WPO_V_Speed;WPO_V_Speed;42;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;383;-3818.837,1339.238;Inherit;False;Property;_WPO_U_Speed;WPO_U_Speed;41;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;384;-2460.001,992.4749;Inherit;False;864.7548;118.4831;软粒子;4;400;393;388;219;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;320;-1758.598,-284.0762;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;365;-2343.675,163.3536;Inherit;False;Property;_FRE_BF;FRE_B/F;33;1;[Enum];Create;True;0;2;outside;0;inside;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;364;-2621.776,615.3464;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;363;-2567.724,131.7422;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;335;-2576.802,-585.7549;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;389;-2282.907,1342.864;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;366;-2614.754,508.791;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;385;-2281.071,1416.624;Inherit;False;Constant;_Float12;Float 12;13;0;Create;True;0;0;0;False;0;False;180;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;319;-1978.921,-596.9757;Inherit;False;214;ONE;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;388;-2334.899,1030.884;Inherit;False;Property;_soft;soft;8;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;391;-2449.146,1170.262;Inherit;False;0;406;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;386;-3647.843,1353.906;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;387;-3815.869,1208.67;Inherit;False;0;397;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;272;-1519.365,-574.176;Inherit;False;317.8248;263.5372;Clamp;2;314;411;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;396;-2074.318,1358.011;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;325;-2408.112,-486.0625;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;321;-1819.908,-592.9556;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;370;-2369.275,-13.74646;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;414;-2572.51,-433.094;Inherit;False;Property;_MaskClamp;MaskClamp;15;1;[Enum];Create;True;0;2;Repeat;0;Clmap;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;368;-2457.427,596.5144;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;367;-2390.737,477.1368;Inherit;False;Constant;_Float8;Float 8;14;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;369;-2346.08,-109.8452;Inherit;False;Constant;_Float9;Float 9;35;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;413;-2207.387,-508.5221;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;392;-3604.973,1032.314;Inherit;False;Property;_WPO_tex;WPO_tex;39;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;399;-2237.698,1174.879;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DepthFade;393;-2165.148,1017.667;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;373;-2198.919,69.12958;Inherit;False;Property;_FFFREncolor;FFFREncolor;31;2;[HDR];[Header];Create;True;1;Fresnel;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;412;-1521.403,-214.0435;Inherit;False;Property;_MianClamp;MianClamp;7;1;[Enum];Create;True;0;2;Repeat;0;Clmap;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;394;-3527.48,1231.442;Inherit;False;Property;_IS_vertex;IS_vertex;38;1;[Enum];Create;True;0;2;OFF;0;ON;1;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;371;-2169.855,542.6441;Inherit;False;Property;_use_disslove;use_disslove;20;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;395;-3808.532,1030.739;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;314;-1518.292,-473.201;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;372;-2185.792,-83.57086;Inherit;False;Property;_FRE_ONOFF;FRE_ON/OFF;32;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;390;-3497.247,1330.114;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;402;-3035.424,1317.285;Inherit;False;Property;_Vertexpower;Vertexpower;40;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalVertexDataNode;401;-3315.26,1159.387;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;398;-3431.046,1037.063;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;397;-3316.791,1300.451;Inherit;True;Property;_wpo_tex;wpo_tex;36;1;[Header];Create;True;1;Wpo_Tex;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;374;-1883.046,498.9162;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;406;-2061.972,1160.638;Inherit;True;Property;_Gam;Gam;12;1;[Header];Create;True;1;GAM;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;411;-1346.54,-529.9504;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;375;-1942.362,-78.32035;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;400;-1920.191,1039.207;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;327;-2325.247,-366.8751;Inherit;True;Property;_MASKTEX;MASKTEX;14;1;[Header];Create;True;1;MASKTEX;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;405;-3012.15,1072.146;Inherit;False;Constant;_Float5;Float 5;30;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;222;-1818.422,92.34608;Inherit;False;four;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;219;-1777.701,1031.745;Inherit;False;soft;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;215;-2265.728,-592.8505;Inherit;False;two;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;258;-1775.635,1159.943;Inherit;False;Gam;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;404;-3010.465,1155.361;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-1175.839,-557.0746;Inherit;True;Property;_maintex;maintex;3;1;[Header];Create;True;1;MainTex;0;0;False;0;False;-1;None;629368bebfedf3049bd04e14168b627e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;410;-1045.522,-342.8385;Inherit;False;Property;_A_R;A_R;5;1;[Enum];Create;True;0;2;R;0;A;1;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;141;-968.7116,-134.2785;Inherit;False;468.0941;422.9443;ALPHA模式连到不透明度，ADD模式连到Emission;5;137;218;216;220;257;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;217;-1792.005,699.8013;Inherit;False;Three;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;408;-2848.823,1129.806;Inherit;False;Property;_ONOFF__vertex;ON/OFF__vertex;37;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;223;-635.1386,-503.4599;Inherit;False;222;four;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;409;-784.9424,-447.7434;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;220;-946.7918,127.3409;Inherit;False;219;soft;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;218;-948.713,50.93473;Inherit;False;217;Three;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;3;-1064.054,-957.387;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;257;-945.7484,-38.10739;Inherit;False;258;Gam;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;216;-950.9857,208.3114;Inherit;False;215;two;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-1181.079,-757.1948;Inherit;False;Property;_Maincolor;Maincolor;4;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,1;4.978539,4.728046,4.728046,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;-632.8237,-96.29089;Inherit;False;6;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;117;-2669.048,1292.653;Inherit;False;Vertex;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-364.8642,-637.1379;Inherit;False;6;6;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;118;-251.5047,-198.4205;Inherit;False;117;Vertex;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;462;-413.1663,-338.2635;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;135;260.1654,-469.7425;Inherit;False;Property;_Dst;材质模式;1;1;[Enum];Create;False;0;2;AlphaBlend;10;Additive;1;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;464;-219.5437,-429.6326;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;142;256.2853,-385.8326;Inherit;False;Property;_CullMode;剔除模式;2;1;[Enum];Create;False;0;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;238;254.7006,-297.8818;Inherit;False;Property;_ZTestMode;深度测试;0;1;[Enum];Create;False;0;2;Less or Equal;4;Always;8;0;True;0;False;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;473;-12.99491,-491.2329;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Normal;0;1;Sprite Normal;0;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=NormalsRendering;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;472;-12.99491,-491.2329;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;LTY/ShaderNew/ALL2.0;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Lit;0;0;Sprite Lit;6;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=Universal2D;False;0;Hidden/InternalErrorShader;0;0;Standard;1;Vertex Position;1;0;3;True;True;True;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;474;-12.99491,-491.2329;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Forward;0;2;Sprite Forward;0;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;283;0;281;0
WireConnection;283;1;280;0
WireConnection;286;0;282;0
WireConnection;286;2;283;0
WireConnection;293;0;284;0
WireConnection;293;1;287;0
WireConnection;292;1;286;0
WireConnection;289;0;287;0
WireConnection;289;1;285;0
WireConnection;296;0;292;1
WireConnection;296;1;291;0
WireConnection;296;2;290;0
WireConnection;296;3;294;0
WireConnection;296;4;295;0
WireConnection;343;0;338;0
WireConnection;343;1;337;0
WireConnection;301;0;288;3
WireConnection;301;1;288;4
WireConnection;300;0;288;1
WireConnection;342;0;340;0
WireConnection;342;1;339;0
WireConnection;298;0;293;0
WireConnection;298;1;289;0
WireConnection;305;0;298;0
WireConnection;305;1;297;0
WireConnection;302;0;297;0
WireConnection;302;1;301;0
WireConnection;307;0;299;0
WireConnection;307;1;296;0
WireConnection;346;0;341;0
WireConnection;346;2;342;0
WireConnection;345;0;343;0
WireConnection;350;0;345;0
WireConnection;351;0;348;0
WireConnection;351;1;347;0
WireConnection;351;2;344;0
WireConnection;308;0;305;0
WireConnection;308;1;302;0
WireConnection;308;2;306;0
WireConnection;353;1;346;0
WireConnection;309;0;303;0
WireConnection;309;1;307;0
WireConnection;309;2;304;0
WireConnection;332;0;330;0
WireConnection;332;1;329;0
WireConnection;333;0;322;0
WireConnection;355;0;353;1
WireConnection;355;1;349;0
WireConnection;356;0;350;0
WireConnection;357;0;351;0
WireConnection;357;1;352;0
WireConnection;310;0;308;0
WireConnection;310;1;309;0
WireConnection;324;0;333;0
WireConnection;324;1;323;0
WireConnection;334;0;331;0
WireConnection;334;2;332;0
WireConnection;362;0;356;0
WireConnection;362;1;354;0
WireConnection;317;0;316;0
WireConnection;360;0;355;0
WireConnection;360;1;357;0
WireConnection;214;0;310;0
WireConnection;359;0;358;0
WireConnection;320;0;317;0
WireConnection;320;1;318;0
WireConnection;364;0;361;0
WireConnection;363;0;362;0
WireConnection;363;1;359;0
WireConnection;335;0;334;0
WireConnection;335;2;324;0
WireConnection;389;0;381;0
WireConnection;366;0;360;0
WireConnection;386;0;383;0
WireConnection;386;1;382;0
WireConnection;396;0;389;0
WireConnection;396;1;385;0
WireConnection;325;0;335;0
WireConnection;321;0;319;0
WireConnection;321;2;320;0
WireConnection;370;0;363;0
WireConnection;370;1;350;0
WireConnection;370;2;365;0
WireConnection;368;0;366;0
WireConnection;368;1;364;0
WireConnection;368;2;361;0
WireConnection;413;0;335;0
WireConnection;413;1;325;0
WireConnection;413;2;414;0
WireConnection;399;0;391;0
WireConnection;399;2;396;0
WireConnection;393;0;388;0
WireConnection;371;1;367;0
WireConnection;371;0;368;0
WireConnection;314;0;321;0
WireConnection;372;1;369;0
WireConnection;372;0;370;0
WireConnection;390;0;387;0
WireConnection;390;2;386;0
WireConnection;398;0;392;0
WireConnection;398;1;395;2
WireConnection;398;2;394;0
WireConnection;397;1;390;0
WireConnection;374;0;372;0
WireConnection;374;1;373;4
WireConnection;374;2;371;0
WireConnection;406;1;399;0
WireConnection;411;0;321;0
WireConnection;411;1;314;0
WireConnection;411;2;412;0
WireConnection;375;0;372;0
WireConnection;375;1;373;0
WireConnection;375;2;371;0
WireConnection;400;0;393;0
WireConnection;327;1;413;0
WireConnection;222;0;375;0
WireConnection;219;0;400;0
WireConnection;215;0;327;1
WireConnection;258;0;406;0
WireConnection;404;0;398;0
WireConnection;404;1;401;0
WireConnection;404;2;397;0
WireConnection;404;3;402;0
WireConnection;1;1;411;0
WireConnection;217;0;374;0
WireConnection;408;1;405;0
WireConnection;408;0;404;0
WireConnection;409;0;1;1
WireConnection;409;1;1;4
WireConnection;409;2;410;0
WireConnection;137;0;3;4
WireConnection;137;1;2;4
WireConnection;137;2;409;0
WireConnection;137;3;218;0
WireConnection;137;4;220;0
WireConnection;137;5;216;0
WireConnection;117;0;408;0
WireConnection;4;0;3;0
WireConnection;4;1;2;0
WireConnection;4;2;1;0
WireConnection;4;3;223;0
WireConnection;4;4;257;0
WireConnection;4;5;220;0
WireConnection;464;0;4;0
WireConnection;464;1;137;0
WireConnection;472;1;464;0
WireConnection;472;4;118;0
ASEEND*/
//CHKSM=F0397BE8AF310313D4973048953B0DDED83FF2AF