// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33095,y:32571,varname:node_4013,prsc:2|diff-1426-OUT,diffpow-711-OUT,transm-6667-OUT,lwrap-5131-OUT,amdfl-7468-RGB,olwid-3771-OUT,olcol-6202-RGB;n:type:ShaderForge.SFN_Posterize,id:1426,x:32764,y:32884,varname:node_1426,prsc:2|IN-9250-OUT,STPS-6499-OUT;n:type:ShaderForge.SFN_Multiply,id:9250,x:32506,y:32700,varname:node_9250,prsc:2|A-6524-OUT,B-7141-OUT,C-5248-RGB;n:type:ShaderForge.SFN_Desaturate,id:7141,x:32215,y:32869,varname:node_7141,prsc:2|COL-8267-RGB,DES-3671-OUT;n:type:ShaderForge.SFN_Dot,id:6524,x:32215,y:32597,varname:node_6524,prsc:2,dt:4|A-2270-OUT,B-2994-OUT;n:type:ShaderForge.SFN_LightColor,id:8267,x:32004,y:32869,varname:node_8267,prsc:2;n:type:ShaderForge.SFN_Vector1,id:3671,x:32215,y:32807,varname:node_3671,prsc:2,v1:1;n:type:ShaderForge.SFN_LightVector,id:2270,x:32004,y:32546,varname:node_2270,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:2994,x:32004,y:32676,prsc:2,pt:False;n:type:ShaderForge.SFN_Fresnel,id:711,x:32764,y:32562,varname:node_711,prsc:2|EXP-7975-OUT;n:type:ShaderForge.SFN_Color,id:5248,x:32004,y:33057,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5248,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:7468,x:32764,y:32723,ptovrint:False,ptlb:Ambient Light Color,ptin:_AmbientLightColor,varname:node_7468,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2573529,c2:0.2573529,c3:0.2573529,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:3771,x:32764,y:33334,ptovrint:False,ptlb:Line Width,ptin:_LineWidth,varname:node_3771,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.01;n:type:ShaderForge.SFN_Slider,id:6499,x:32607,y:33107,ptovrint:False,ptlb:Steps,ptin:_Steps,varname:node_6499,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:12,max:30;n:type:ShaderForge.SFN_Slider,id:5131,x:32607,y:33221,ptovrint:False,ptlb:Light Wrapping,ptin:_LightWrapping,varname:node_5131,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:3;n:type:ShaderForge.SFN_ValueProperty,id:7975,x:32764,y:32500,ptovrint:False,ptlb:Fresnel Strength,ptin:_FresnelStrength,varname:node_7975,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Add,id:678,x:31748,y:32307,varname:node_678,prsc:2|A-6594-UVOUT,B-6842-OUT;n:type:ShaderForge.SFN_Fresnel,id:3759,x:31430,y:32406,varname:node_3759,prsc:2|EXP-6582-OUT;n:type:ShaderForge.SFN_Slider,id:3470,x:31273,y:32558,ptovrint:False,ptlb:Follow Contour,ptin:_FollowContour,varname:node_3470,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6842,x:31605,y:32406,varname:node_6842,prsc:2|A-3759-OUT,B-3470-OUT;n:type:ShaderForge.SFN_Multiply,id:7572,x:31923,y:32307,varname:node_7572,prsc:2|A-678-OUT,B-5116-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5116,x:31903,y:32239,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_5116,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:300;n:type:ShaderForge.SFN_Sin,id:2367,x:32272,y:32307,varname:node_2367,prsc:2|IN-9763-UVOUT;n:type:ShaderForge.SFN_Abs,id:7218,x:32434,y:32307,varname:node_7218,prsc:2|IN-2367-OUT;n:type:ShaderForge.SFN_Desaturate,id:689,x:32595,y:32307,varname:node_689,prsc:2|COL-7218-OUT;n:type:ShaderForge.SFN_Multiply,id:6667,x:32764,y:32307,varname:node_6667,prsc:2|A-689-OUT,B-9250-OUT;n:type:ShaderForge.SFN_Rotator,id:9763,x:32112,y:32307,varname:node_9763,prsc:2|UVIN-7572-OUT,ANG-8337-OUT;n:type:ShaderForge.SFN_Vector1,id:6582,x:31243,y:32406,varname:node_6582,prsc:2,v1:45;n:type:ShaderForge.SFN_Vector1,id:8337,x:32112,y:32239,varname:node_8337,prsc:2,v1:45;n:type:ShaderForge.SFN_TexCoord,id:6594,x:31521,y:32234,varname:node_6594,prsc:2,uv:0;n:type:ShaderForge.SFN_Color,id:6202,x:33006,y:33247,ptovrint:False,ptlb:Outline Color,ptin:_OutlineColor,varname:node_6202,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;proporder:5248-3771-6499-7468-5131-7975-3470-5116-6202;pass:END;sub:END;*/

Shader "Shader Forge/ToonWithEdges" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _LineWidth ("Line Width", Float ) = 0.01
        _Steps ("Steps", Range(1, 30)) = 12
        _AmbientLightColor ("Ambient Light Color", Color) = (0.2573529,0.2573529,0.2573529,1)
        _LightWrapping ("Light Wrapping", Range(0, 3)) = 0
        _FresnelStrength ("Fresnel Strength", Float ) = 5
        _FollowContour ("Follow Contour", Range(0, 1)) = 0
        _Scale ("Scale", Float ) = 300
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _LineWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_LineWidth,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform float4 _AmbientLightColor;
            uniform float _Steps;
            uniform float _LightWrapping;
            uniform float _FresnelStrength;
            uniform float _FollowContour;
            uniform float _Scale;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 w = float3(_LightWrapping,_LightWrapping,_LightWrapping)*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = pow(max(float3(0.0,0.0,0.0), NdotLWrap + w ), pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelStrength));
                float node_9763_ang = 45.0;
                float node_9763_spd = 1.0;
                float node_9763_cos = cos(node_9763_spd*node_9763_ang);
                float node_9763_sin = sin(node_9763_spd*node_9763_ang);
                float2 node_9763_piv = float2(0.5,0.5);
                float2 node_9763 = (mul(((i.uv0+(pow(1.0-max(0,dot(normalDirection, viewDirection)),45.0)*_FollowContour))*_Scale)-node_9763_piv,float2x2( node_9763_cos, -node_9763_sin, node_9763_sin, node_9763_cos))+node_9763_piv);
                float3 node_9250 = (0.5*dot(lightDirection,i.normalDir)+0.5*lerp(_LightColor0.rgb,dot(_LightColor0.rgb,float3(0.3,0.59,0.11)),1.0)*_Color.rgb);
                float3 backLight = pow(max(float3(0.0,0.0,0.0), -NdotLWrap + w ), pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelStrength)) * (dot(abs(sin(node_9763)),float3(0.3,0.59,0.11))*node_9250);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = (forwardLight+backLight) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                indirectDiffuse += _AmbientLightColor.rgb; // Diffuse Ambient Light
                float3 diffuseColor = floor(node_9250 * _Steps) / (_Steps - 1);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
