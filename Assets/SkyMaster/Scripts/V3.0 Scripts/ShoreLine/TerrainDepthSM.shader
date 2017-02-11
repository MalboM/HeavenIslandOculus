// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

 Shader "SkyMaster/TerrainDepthSM"
 {
     Properties
     {
         _MainTex ("Base (RGB)", 2D) = "white" {}
         _heightFactor ("height Factor", Range(0, 5)) = 1
         _heightCutoff ("height Cutoff", Range(0, 0.1)) = 0.1
         _contrast ("contrast", Range(0.05, 0.15)) = 0.1
     }
     SubShader
     {
         Pass
         {
             CGPROGRAM
 
             #pragma vertex vert
             #pragma fragment frag
             #include "UnityCG.cginc"
             
             uniform sampler2D _MainTex;
             uniform sampler2D _CameraDepthTexture;
             uniform fixed _heightFactor;
             uniform half4 _MainTex_TexelSize;             
             uniform float _heightCutoff;
    		 uniform float _contrast;
     
             struct input
             {
                 float4 pos : POSITION;
                 half2 uv : TEXCOORD0;
             };
 
             struct v2f
             {
                 float4 pos : SV_POSITION;
                 half2 uv : TEXCOORD0;
                 float3 WorldPos: TEXCOORD1;
             }; 
 
             v2f vert(input i)
             {
                 v2f o;
                 o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, i.uv);              
                 #if UNITY_UV_STARTS_AT_TOP
                 if (_MainTex_TexelSize.y < 0)
                         o.uv.y = 1 - o.uv.y;
                 #endif
                 o.pos = mul(UNITY_MATRIX_MVP, i.pos);                  
                 o.WorldPos = mul(unity_ObjectToWorld,o.pos).xyz;                
 
                 return o;
             }
             
             fixed4 frag(v2f o) : COLOR
             {
                 float TerrainDepth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, o.uv));
                 TerrainDepth = pow(Linear01Depth(TerrainDepth), _heightFactor);
                                
                 if(TerrainDepth <  _heightCutoff ){
                  	TerrainDepth=0;
                 }else{
                    TerrainDepth = ((TerrainDepth/0.1)-_contrast);
                 }
           
                 return TerrainDepth;
             }
             
             ENDCG
         }
     } 
 }