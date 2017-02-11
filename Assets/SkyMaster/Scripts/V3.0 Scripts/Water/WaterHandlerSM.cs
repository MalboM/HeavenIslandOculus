﻿using UnityEngine;
using System.Collections;

namespace Artngame.SKYMASTER {

[ExecuteInEditMode]
public class WaterHandlerSM : MonoBehaviour {

		public bool followCamera=false;//follow camera toggle
		public bool DisableUnderwater = false;
		public float HeightSpecularCorrect = 0f; 
		public float RefractOffset=0;

		public float BumpFocusOffset=0;
		public float FresnelOffset = 0;
		public float FresnelBias = 0;

		//REFLECT COLOR
		public Color ReflectColor;
		public bool OverrideReflectColor = false;

		//SHORE LINE
		public float TerrainScale = 500;
		public float ShoreWavesFade = 0;
		public Transform TerrainDepthCamera;
		public TerrainDepthSM DepthRenderController;

		//FOAM
		public float FoamCutoff = 0;
		public float FoamOffset = 0;

		//AUDIO
		public AudioSource SeaAudio;
		public bool DisableSeaAudio=false;
		public float maxAudioVolume = 0.65f;
		public float SoundFadeSpeed = 0.02f;

		//BLEND
		public float ShoreBlendOffset = 0;
		public float minShiftSpeed = 5f;

		public Vector4 WaveAmpOffset = new Vector4 (0, 0, 0, 0); 
		public Vector4 WaveFreqOffset = new Vector4 (0, 0, 0, 0); 
		public Vector4 WaveSpeedOffset = new Vector4 (0, 0, 0, 0); 
		public Vector4 WaveDir1Offset = new Vector4 (0, 0, 0, 0); 
		public Vector4 WaveDir2Offset = new Vector4 (0, 0, 0, 0); 
		public float DepthColorOffset = 0;

		public Vector4 ExtraWavesFactor = 		new Vector4 (0, 		0, 		0, 		0); 
		public Vector4 ExtraWavesFreqFactor = 	new Vector4 (2.33f, 	2.11f, 	1.24f, 	1);
		public Vector4 ExtraWavesAmpFactor = 	new Vector4 (0.09f, 	1, 		1, 		1);
		public Vector4 ExtraWavesDirFactor = 	new Vector4 (-4.61f, 	1, 		1, 		1);
		public Vector4 ExtraWavesSteepFactor = 	new Vector4 (2, 		1, 		2, 		1);

		//Caustics
		public Projector CausticsProjector;
		public Material CausticsMat;
		public float CausticIntensity = 0.4f;
		public float CausticSize = 40;

		//v3.0 underwater shader
		public WaterBaseSM WaterBase;
		public float SunShaftsInt=100;
		public float bumpTilingXoffset = 0;
		public float bumpTilingYoffset = 0;

		//v3.0 - depth fog switch
		public float DepthFogSwitch = 10;
		public bool BelowWater = false;
		public enum WaterPreset {Custom, Emerald, Muddy, Caribbean, River, Ocean, DarkOcean, FocusOcean, SmallWaves, Lake, Atoll};
		public enum UnderWaterPreset {Custom, Fancy, Turbulent, Calm};

		public WaterPreset waterType = WaterPreset.Caribbean;
		//WaterPreset prevWaterType = WaterPreset.Caribbean;

		public UnderWaterPreset underWaterType = UnderWaterPreset.Turbulent;
		//UnderWaterPreset prevunderWaterType = UnderWaterPreset.Turbulent;

		public Vector3 waterScaleFactor = Vector3.one;
		public Vector3 waterScaleOffset = Vector3.zero;

		//v3.0
		bool prev_fog_setting;//fog setting in SkyManager before underwater
		int prev_fog_mode;
		float prev_fog_density;
		public UnderWaterImageEffect UnderwaterBlur;
		public UnderWaterImageEffect UnderwaterBlurL;
		public UnderWaterImageEffect UnderwaterBlurR;
		Color prev_fog_color;
		Vector2 PrevStartEndDist;
		public Color Fog_Color = new Color (70f / 255f, 110f / 255f, 134f / 255f, 255f / 255f);

		public SkyMasterManager SkyManager;
		public Material oceanMat;
		//Color StartWaterColor;
		public Color NightWaterColor= new Color(50f/255f,50f/255f,50f/255f,255f/255f);
		public Color StormWaterColor= new Color(35f/255f,35f/255f,35f/255f,255f/255f);
		public Color SnowStormWaterColor= new Color(235f/255f,235f/255f,235f/255f,255f/255f);
		public Color DayWaterColor = new Color(91f/255f,139f/255f,129f/255f,209f/255f);
		public Color DuskWaterColor = new Color(191f/255f,99f/255f,119f/255f,209f/255f);

		public Color NightReflectColor= new Color(0f/255f,0f/255f,0f/255f,255f/255f);
		public Color DayReflectColor = new Color(13f/255f,245f/255f,255f/255f,110f/255f);
		public Color DuskReflectColor = new Color(231f/255f,169f/255f,199f/255f,209f/255f);

		public Color DayRiverColor = new Color(35f/255f,75f/255f,67f/255f,244f/255f);
		public Color DayRiverReflectColor = new Color(234f/255f,234f/255f,234f/255f,206f/255f);

		public Color DayOceanColor = new Color(40f/255f,100f/255f,90f/255f,200f/255f);
		public Color DayOceanReflectColor = new Color(150f/255f,210f/255f,230f/255f,150f/255f);

		public Color DarkOceanColor = new Color(36f/255f,60f/255f,50f/255f,240f/255f);
		public Color DarkOceanReflectColor = new Color(150f/255f,210f/255f,230f/255f,150f/255f);

		public Color MuddyWaterColor = new Color(250f/255f,25f/255f,75f/255f,205f/255f);
		public Color MuddyReflectColor = new Color(160f/255f,110f/255f,135f/255f,210f/255f);

		public Color EmeraldWaterColor = new Color(85f/255f,225f/255f,185f/255f,205f/255f);
		public Color EmeraldReflectColor = new Color(115f/255f,240f/255f,255f/255f,108f/255f);

		public Color AtollWaterColor = new Color(118f/255f,225f/255f,184f/255f,105f/255f);
		public Color AtollReflectColor = new Color(210f/255f,240f/255f,250f/255f,255f/255f);

		public SpecularLightingSM SpecularSource;

	public Vector3 Water_start;
	public Vector2 Water_size = new Vector2(0,0);//keep water size here for reference

	public SeasonalTerrainSKYMASTER TerrainManager;

	// Use this for initialization
	void Start () {

			if (TerrainDepthCamera != null && oceanMat != null) {
				oceanMat.SetVector("_DepthCameraPos", new Vector4(TerrainDepthCamera.position.x,TerrainDepthCamera.position.y,TerrainDepthCamera.position.z,1));
				//oceanMat.SetFloat("_ShoreFadeFactor", ShoreWavesFade);
				//oceanMat.SetFloat("_TerrainScale", TerrainScale);
			}
			if (oceanMat != null) {
				oceanMat.SetFloat("_ShoreFadeFactor", ShoreWavesFade);
				oceanMat.SetFloat("_TerrainScale", TerrainScale);
			}

		if (Application.isPlaying) {
			Water_start = this.transform.position;
			this_transf = transform;
			cam_transf = Camera.main.transform;

			if (TerrainManager != null) {
				prev_preset = TerrainManager.FogPreset;
			}

			//v3.0 - if not defined, get material for water
			if (oceanMat == null) {
				oceanMat = GetComponentsInChildren<MeshRenderer> (true) [0].material;
			}
			//StartWaterColor = oceanMat.GetColor ("_BaseColor");
			SpecularSource = this.gameObject.GetComponent<SpecularLightingSM> ();

			if (RenderSettings.fogMode == FogMode.Exponential) {
				prev_fog_mode = 0;
			}
			if (RenderSettings.fogMode == FogMode.ExponentialSquared) {
				prev_fog_mode = 1;
			}
			if (RenderSettings.fogMode == FogMode.Linear) {
				prev_fog_mode = 2;
			}
			prev_fog_density = RenderSettings.fogDensity;
			prev_fog_color = RenderSettings.fogColor;
			PrevStartEndDist = new Vector2 (RenderSettings.fogStartDistance, RenderSettings.fogEndDistance);

				//v3.0
				if (SkyManager != null && oceanMat != null) {
					
					UpdateWaterParams(true);
					
				}
		}
	}

		public void DisableColliders(){
			MeshCollider[] colliders = this.gameObject.GetComponentsInChildren<MeshCollider> ();
			if (colliders != null && colliders.Length > 0) {
				for(int i=0;i<colliders.Length;i++){
					colliders[i].enabled = false;
				}
			}
		}
		public void EnableColliders(){
			MeshCollider[] colliders = this.gameObject.GetComponentsInChildren<MeshCollider> ();
			if (colliders != null && colliders.Length > 0) {
				for(int i=0;i<colliders.Length;i++){
					colliders[i].enabled = true;
				}
			}
		}

		Transform this_transf;
		Transform cam_transf;
		int prev_preset;
	
	
	void UpdateWaterParams (bool Init) {

			//v3.0 - new automatic TOD
			//bool is_DayLight  = (SkyManager.AutoSunPosition && SkyManager.Rot_Sun_X > 0 ) | (!SkyManager.AutoSunPosition && SkyManager.Current_Time > ( 9.0f + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn));

			bool is_DayLight    = (SkyManager.AutoSunPosition && SkyManager.Rot_Sun_X > 0 ) | (!SkyManager.AutoSunPosition && SkyManager.Current_Time > ( 9.0f + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (SkyManager.NightTimeMax + SkyManager.Shift_dawn));

			bool is_after_22  = (SkyManager.AutoSunPosition && SkyManager.Rot_Sun_X < 5 ) | (!SkyManager.AutoSunPosition && SkyManager.Current_Time >  (21.0f + SkyManager.Shift_dawn));
			bool is_after_17  = (SkyManager.AutoSunPosition && SkyManager.Rot_Sun_X > 65) | (!SkyManager.AutoSunPosition && SkyManager.Current_Time >  (17.0f + SkyManager.Shift_dawn));

			float speedy = 0.1f * Time.deltaTime * (SkyManager.SPEED + minShiftSpeed);

			if (Init) {
				speedy = 100000;
				ReflectColor = oceanMat.GetColor ("_ReflectionColor");
			} else {
				//oceanMat.SetColor("_ReflectionColor",ReflectColor);
			}

			//FOAM
			Vector4 Foam = oceanMat.GetVector("_Foam");
			if (Init) {
				//FoamOffset = Foam.x;
				//FoamCutoff = Foam.y;
			} else {
				oceanMat.SetVector ("_Foam", Vector4.Lerp (Foam, new Vector4 (0 + FoamOffset, 0 + FoamCutoff, -0.16f, -0.21f), speedy));
			}

			//HEAVY STORM
			if(SkyManager.currentWeatherName == Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm){
				//no moon-sun
				SpecularSource.specularLight = null;
				
				oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), StormWaterColor, speedy));
				oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), NightReflectColor, speedy));
			}
			//SNOW STORM
			else if(SkyManager.currentWeatherName == Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.SnowStorm){
				//no moon-sun
				SpecularSource.specularLight = null;
				
				oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), SnowStormWaterColor, speedy));
				oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), SnowStormWaterColor, speedy));
			}
			else{
				
				//TIME OF DAY CHANGE
				//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)) {
				if(is_DayLight ){
					//moon-sun
					SpecularSource.specularLight = SkyManager.SunObj.transform;
					
					//if (SkyManager.Current_Time > (21 + SkyManager.Shift_dawn)) {
					if(is_after_22 ){
						oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DuskWaterColor, speedy));
						oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DuskReflectColor*0.9f, speedy));
					}else
					//if (SkyManager.Current_Time > (17 + SkyManager.Shift_dawn)) {
					if(is_after_17 ){

						if( waterType == WaterPreset.Muddy ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), MuddyWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), MuddyReflectColor, speedy));
						}else if( waterType == WaterPreset.Atoll ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), AtollWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), AtollReflectColor, speedy));
						}else{
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DuskWaterColor,speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DuskReflectColor, speedy));
						}

					} else {

						if(waterType == WaterPreset.Custom){

						}else if( waterType == WaterPreset.Caribbean |  waterType == WaterPreset.FocusOcean){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DayWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DayReflectColor, speedy));
						}else if( waterType == WaterPreset.Emerald ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), EmeraldWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), EmeraldReflectColor, speedy));
						}else if( waterType == WaterPreset.SmallWaves ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), EmeraldWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), EmeraldReflectColor, speedy));
						}else if( waterType == WaterPreset.Lake ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), EmeraldWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), EmeraldReflectColor, speedy));
						}else if( waterType == WaterPreset.Muddy ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), MuddyWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), MuddyReflectColor, speedy));
						}else if(waterType == WaterPreset.River){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DayRiverColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DayRiverReflectColor, speedy));
						}else if(waterType == WaterPreset.Ocean){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DayOceanColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DayOceanReflectColor, speedy));
						}else if(waterType == WaterPreset.DarkOcean){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DarkOceanColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DarkOceanReflectColor, speedy));
						}else if( waterType == WaterPreset.Atoll ){
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), AtollWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), AtollReflectColor, speedy));
						}else{
							oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), DayWaterColor, speedy));
							oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), DayReflectColor, speedy));
						}
					}
				} else {
					
					//moon-sun
					SpecularSource.specularLight = SkyManager.MoonObj.transform;

					if( waterType == WaterPreset.Atoll ){
						oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), AtollWaterColor*0.6f, speedy));
						oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), AtollReflectColor*0.6f, speedy));
					}else{
						oceanMat.SetColor ("_BaseColor", Color.Lerp (oceanMat.GetColor ("_BaseColor"), NightWaterColor,speedy));
						oceanMat.SetColor ("_ReflectionColor", Color.Lerp (oceanMat.GetColor ("_ReflectionColor"), NightReflectColor, speedy));
					}
				}
				
				
				
			}

			if (OverrideReflectColor) {
				if (Init) {
					//speedy = 100000;
					//ReflectColor = oceanMat.GetColor ("_ReflectionColor");
				} else {
					oceanMat.SetColor ("_ReflectionColor", ReflectColor);
				}
			}

			//IF UNDERWATER
			float Sm_speed = SkyManager.SPEED + minShiftSpeed;
			float Time_delta = Time.deltaTime;

			if (Init) {
				Sm_speed=10000;
				Time_delta=10000;
			}

			//UNDERWATER
			if (cam_transf.position.y < this_transf.position.y && !DisableUnderwater) {
				//Debug.Log("under");

				if(underWaterType == UnderWaterPreset.Custom){

				}else if(underWaterType == UnderWaterPreset.Fancy){

					oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.17f, 0.27f * Time_delta * Sm_speed));//0.27

					float Fade1 = 0.00002f;
					float Fade2 = 0.000016f;
					float Fade3 = -582f;
					Fade1 =1f;
					Fade2 = 1f;
					Fade3 = -SunShaftsInt;

					float distS = this_transf.position.y - cam_transf.position.y;
					if (cam_transf.position.y < this_transf.position.y-DepthFogSwitch) {
						//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)
						if(is_DayLight
						    & SkyManager.currentWeatherName != Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm) {
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							float change_to = Fade3+(distS-DepthFogSwitch)*35;
							if(change_to > 5){
								change_to=5;
							}
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade1 + ShoreBlendOffset,0.1594f,0.043f,change_to),222.57f * Time_delta * 2));
						}else{
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade1 + ShoreBlendOffset,0.1594f,0.043f,1),22.57f * Time_delta * 2));
						}
					}else{
						//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)
						if(is_DayLight
						    & SkyManager.currentWeatherName != Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm) {
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							//oceanMat.SetVector("_InvFadeParemeter", Fades + new Vector4(0.000016f,0,0,0));
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade2 + ShoreBlendOffset,0.1594f,0.043f,Fade3),225.57f * Time_delta * 2));
						}else{
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							//oceanMat.SetVector("_InvFadeParemeter", new Vector4(0.000016f,Fades.y,Fades.z,Fades.w));
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade2 + ShoreBlendOffset,0.1594f,0.043f,1),222.57f * Time_delta * 2));
						}
					}
					
					Vector4 Distortions = oceanMat.GetVector("_DistortParams");
					//oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(7.82f,0.1f,-0.74f,569.9f),0.27f * Time_delta * Sm_speed));
					oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(7.82f,0.1f,-0.74f,569.9f),0.27f * Time_delta * Sm_speed));
					
					//handle scrren drops
					if(SkyManager.ScreenRainDropsMat != null){
						SkyManager.ScreenRainDropsMat.SetFloat("_Speed",1.1f);
					}

					Vector4 Fades1 = oceanMat.GetVector("_BumpTiling");
					oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));

				}else if(underWaterType == UnderWaterPreset.Turbulent){

					oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.27f * Time_delta * Sm_speed));//0.27
					oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),329f,0.27f * Time_delta * Sm_speed));
					
					float Fade1 = 0.00002f;
					float Fade2 = 0.000016f;
					float Fade3 = -582f;
					Fade1 =1.81f;
					Fade2 = 1.81f;
					Fade3 = -SunShaftsInt-200;
					
					float distS = this_transf.position.y - cam_transf.position.y;
					if (cam_transf.position.y < this_transf.position.y-DepthFogSwitch) {
						//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)
						if(is_DayLight
						    & SkyManager.currentWeatherName != Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm) {
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							float change_to = Fade3+(distS-DepthFogSwitch)*35;
							if(change_to > 5){
								change_to=5;
							}
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade1 + ShoreBlendOffset,0.1594f,0.043f,change_to),222.57f * Time_delta * 2));
						}else{
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade1 + ShoreBlendOffset,0.1594f,0.043f,1),22.57f * Time_delta * 2));
						}
					}else{
						//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)
						if(is_DayLight
						    & SkyManager.currentWeatherName != Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm) {
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							//oceanMat.SetVector("_InvFadeParemeter", Fades + new Vector4(0.000016f,0,0,0));
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade2 + ShoreBlendOffset,0.1594f,0.043f,Fade3),225.57f * Time_delta * 2));
						}else{
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							//oceanMat.SetVector("_InvFadeParemeter", new Vector4(0.000016f,Fades.y,Fades.z,Fades.w));
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade2 + ShoreBlendOffset,0.1594f,0.043f,1),222.57f * Time_delta * 2));
						}
					}
					
					Vector4 Distortions = oceanMat.GetVector("_DistortParams");
					//oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(7.82f,0.1f,-0.74f,569.9f),0.27f * Time_delta * Sm_speed));
					oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(22.17f,0.1f,-0.74f,1.19f),0.27f * Time_delta * Sm_speed));
					
					//handle scrren drops
					if(SkyManager.ScreenRainDropsMat != null){
						SkyManager.ScreenRainDropsMat.SetFloat("_Speed",1.1f);
					}
					
					Vector4 Fades1 = oceanMat.GetVector("_BumpTiling");
					oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));

					///WAVES
					/// 
					Vector4 Fades2 = oceanMat.GetVector("_GAmplitude");
					Vector4 Fades3 = oceanMat.GetVector("_GFrequency");
					Vector4 Fades4 = oceanMat.GetVector("_GSteepness");
					Vector4 Fades5 = oceanMat.GetVector("_GSpeed");
					Vector4 Fades6 = oceanMat.GetVector("_GDirectionAB");
					Vector4 Fades7 = oceanMat.GetVector("_GDirectionCD");
					oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
					oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(2.54f,8.52f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
					oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
					oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 5.71f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
					oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
					oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));

				}else if(underWaterType == UnderWaterPreset.Calm){

					Color tempC = oceanMat.GetColor ("_BaseColor");
					oceanMat.SetColor ("_BaseColor", new Color(tempC.r,tempC.g,tempC.b,0.04f));

					oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.27f * Time_delta * Sm_speed));//0.27
					oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),329f,0.27f * Time_delta * Sm_speed));
					
					float Fade1 = 0.00002f;
					float Fade2 = 0.000016f;
					float Fade3 = 0f;
					Fade1 =1.81f;
					Fade2 = 1.81f;
					//Fade3 = -SunShaftsInt-200;
					
					float distS = this_transf.position.y - cam_transf.position.y;
					if (cam_transf.position.y < this_transf.position.y-DepthFogSwitch) {
						//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)
						if(is_DayLight
						    & SkyManager.currentWeatherName != Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm) {
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							float change_to = Fade3+(distS-DepthFogSwitch)*35;
							if(change_to > 5){
								change_to=5;
							}
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade1 + ShoreBlendOffset,0.1594f,0.043f,Fade3),222.57f * Time_delta * 2));
						}else{
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade1 + ShoreBlendOffset,0.1594f,0.043f,Fade3),22.57f * Time_delta * 2));
						}
					}else{
						//if (SkyManager.Current_Time > (9 + SkyManager.Shift_dawn) & SkyManager.Current_Time <= (21.9f + SkyManager.Shift_dawn)
						if(is_DayLight
						    & SkyManager.currentWeatherName != Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types.HeavyStorm) {
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							//oceanMat.SetVector("_InvFadeParemeter", Fades + new Vector4(0.000016f,0,0,0));
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade2 + ShoreBlendOffset,0.1594f,0.043f,Fade3),225.57f * Time_delta * 2));
						}else{
							Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
							//oceanMat.SetVector("_InvFadeParemeter", new Vector4(0.000016f,Fades.y,Fades.z,Fades.w));
							oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(Fade2 + ShoreBlendOffset,0.1594f,0.043f,Fade3),222.57f * Time_delta * 2));
						}
					}
					
					Vector4 Distortions = oceanMat.GetVector("_DistortParams");
					//oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(7.82f,0.1f,-0.74f,569.9f),0.27f * Time_delta * Sm_speed));
					oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(222.17f,0.011f,1f,-25f),0.27f * Time_delta * Sm_speed));
					
					//handle scrren drops
					if(SkyManager.ScreenRainDropsMat != null){
						SkyManager.ScreenRainDropsMat.SetFloat("_Speed",1.1f);
					}
					
					Vector4 Fades1 = oceanMat.GetVector("_BumpTiling");
					oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.82f+bumpTilingXoffset,0.77f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
					
					///WAVES
					/// 
					Vector4 Fades2 = oceanMat.GetVector("_GAmplitude");
					Vector4 Fades3 = oceanMat.GetVector("_GFrequency");
					Vector4 Fades4 = oceanMat.GetVector("_GSteepness");
					Vector4 Fades5 = oceanMat.GetVector("_GSpeed");
					Vector4 Fades6 = oceanMat.GetVector("_GDirectionAB");
					Vector4 Fades7 = oceanMat.GetVector("_GDirectionCD");
					oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
					oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(2.54f,8.52f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
					oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
					oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
					oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 5.71f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
					oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
					oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));
					
				}
			}else{
				//OVERWATER
				if(1==1){
				//if(prevWaterType != waterType){

					Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
					Vector4 Distortions = oceanMat.GetVector("_DistortParams");

					Vector4 Fades1 = oceanMat.GetVector("_BumpTiling");
					Vector4 Fades2 = oceanMat.GetVector("_GAmplitude");
					Vector4 Fades3 = oceanMat.GetVector("_GFrequency");
					Vector4 Fades4 = oceanMat.GetVector("_GSteepness");
					Vector4 Fades5 = oceanMat.GetVector("_GSpeed");
					Vector4 Fades6 = oceanMat.GetVector("_GDirectionAB");
					Vector4 Fades7 = oceanMat.GetVector("_GDirectionCD");

					if(oceanMat.HasProperty("_GerstnerIntensities")){
						Vector4 Fades71 = oceanMat.GetVector("_GerstnerIntensities");
						Vector4 Fades72 = oceanMat.GetVector("_Gerstnerfactors");
						Vector4 Fades73 = oceanMat.GetVector("_Gerstnerfactors2");
						Vector4 Fades74 = oceanMat.GetVector("_GerstnerfactorsDir");
						Vector4 Fades75 = oceanMat.GetVector("_GerstnerfactorsSteep");

						if(waterType == WaterPreset.Atoll ){
							//oceanMat.SetVector ("_GerstnerIntensities", Vector4.Lerp(Fades71,new Vector4(0.74f,0.5f,0.35f,0),110.27f * Time_delta * 2));
							oceanMat.SetVector ("_GerstnerIntensities", Vector4.Lerp(Fades71,new Vector4(0.13f,0.01f,0.01f,0),110.27f * Time_delta * 2));
							oceanMat.SetVector ("_Gerstnerfactors", Vector4.Lerp(Fades72,new Vector4(2.33f,2.11f,40f,1),110.27f * Time_delta * 2));//freq
							oceanMat.SetVector ("_Gerstnerfactors2", Vector4.Lerp(Fades73,new Vector4(0.09f,1,1.43f,1),110.27f * Time_delta * 2));//amp
							oceanMat.SetVector ("_GerstnerfactorsDir", Vector4.Lerp(Fades74,new Vector4(-4.61f,1,1,1),110.27f * Time_delta * 2));//direction
							oceanMat.SetVector ("_GerstnerfactorsSteep", Vector4.Lerp(Fades75,new Vector4(2,1,2,1),110.27f * Time_delta * 2));//steepness
						}else{
							oceanMat.SetVector ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
							oceanMat.SetVector ("_Gerstnerfactors", Vector4.Lerp(Fades72,ExtraWavesFreqFactor,110.27f * Time_delta * 2));//freq
							oceanMat.SetVector ("_Gerstnerfactors2", Vector4.Lerp(Fades73,ExtraWavesAmpFactor,110.27f * Time_delta * 2));//amp
							oceanMat.SetVector ("_GerstnerfactorsDir", Vector4.Lerp(Fades74,ExtraWavesDirFactor,110.27f * Time_delta * 2));//direction
							oceanMat.SetVector ("_GerstnerfactorsSteep", Vector4.Lerp(Fades75,ExtraWavesSteepFactor,110.27f * Time_delta * 2));//steepness
						}
					}

					if(waterType == WaterPreset.Custom){
						
					}else if(waterType == WaterPreset.Caribbean ){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(7.82f + BumpFocusOffset,0.1f+RefractOffset,-0.74f+ FresnelOffset,6f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),368f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.16f + ShoreBlendOffset,0.1594f,0.043f,4.2f),110.27f * Time_delta * 2));

						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(0.5f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 5.71f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));

						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));

						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.Emerald ){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(9.78f + BumpFocusOffset,0.1f+RefractOffset,-0.74f+ FresnelOffset,6f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),65f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.077f + ShoreBlendOffset,0.1594f,0.043f,4.2f),110.27f * Time_delta * 2));
						
						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.4f+bumpTilingXoffset,0.4f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 8.5f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(1.14f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.85f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,-1.3f,0.06f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 3.73f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), 0f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), 0f, 0.15f *Time_delta * Sm_speed));
						
						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
						
						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.SmallWaves ){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(4.49f + BumpFocusOffset,0.15f+RefractOffset,-0.04f+ FresnelOffset,66.9f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),7,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.11f + ShoreBlendOffset,6.3f,0.76f,-7.4f),110.27f * Time_delta * 2));
						
						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.59f+bumpTilingXoffset,0.97f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), -0.58f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.2f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(1.71f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 15.56f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.188599f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), 0f, 0.15f *Time_delta * Sm_speed));
						
						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
						
						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.Lake ){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(1.8f + BumpFocusOffset,0.27f+RefractOffset,0.19f+ FresnelOffset,6f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),472f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.09f + ShoreBlendOffset,0.1594f,0.043f,4.2f),110.27f * Time_delta * 2));
						
						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.16f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.02f,0f,0.0f,0f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(3f,1.74f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), -1f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), 0f, 0.15f *Time_delta * Sm_speed));
						
						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
						
						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.FocusOcean ){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(4.49f + BumpFocusOffset,0.36f+RefractOffset,-0.06f+ FresnelOffset,73.47f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),188f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.1f + ShoreBlendOffset,0.1594f,0.043f,0.72f),110.27f * Time_delta * 2));
						
						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.59f+bumpTilingXoffset,0.97f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), -0.59f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.32f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(1.71f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 12.17f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.1886f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));
						
						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
						
						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.Muddy ){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(6.37f + BumpFocusOffset,0.1f+RefractOffset,-0.74f+ FresnelOffset,6f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),113f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.07f + ShoreBlendOffset,0.15f,1.29f,4.2f),110.27f * Time_delta * 2));
						
						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(0.5f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 5.71f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));
						
						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
						
						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.River){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(6.59f + BumpFocusOffset,0.1f+RefractOffset,-0.74f+ FresnelOffset,6f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),63f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.16f + ShoreBlendOffset,0.1594f,0.043f,4.2f),110.27f * Time_delta * 2));

						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(0.5f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 5.71f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));

						waterScaleFactor = Vector3.one;
					}else if(waterType == WaterPreset.Ocean  | waterType == WaterPreset.DarkOcean){
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(3.04f + BumpFocusOffset,-1.23f+RefractOffset,1.85f+ FresnelOffset,-0.5f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),93f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.43f, 0.15f *Time_delta * Sm_speed));			//
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.21f + ShoreBlendOffset,0.1594f,0.043f,4.2f),110.27f * Time_delta * 2));		//

						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.02f+bumpTilingXoffset,0.02f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));				//
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 9.47f, 0.15f *Time_delta * Sm_speed));	//
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.14f,1.33f,0.17f,0.222f) + WaveAmpOffset,110.27f * Time_delta * 2));				//
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(0.5f,0.38f,0.59f,0.35f) + WaveFreqOffset,110.27f * Time_delta * 2));				//
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(0f,22f,0f,0f),110.27f * Time_delta * 2));						//
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-3f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));							//
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.46f,0.35f,-0.11f,-0.07f) + WaveDir1Offset,110.27f * Time_delta * 2));				//
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.7f,-0.04f,0.7175f,0.31f) + WaveDir2Offset,110.27f * Time_delta * 2));		//							
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 8.14f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));		//						
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.76f, 0.15f *Time_delta * Sm_speed));	//
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), 0f, 0.15f *Time_delta * Sm_speed)); 	//

						waterScaleFactor = new Vector3(1,6,1);
					}else if(waterType == WaterPreset.Atoll ){

						HeightSpecularCorrect = (cam_transf.position.y - this_transf.position.y)*0.001f;

						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(3.91f + BumpFocusOffset,0.36f+RefractOffset,-0.035f - HeightSpecularCorrect + FresnelOffset,65.85f + FresnelBias),0.27f * Time_delta * Sm_speed*5));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),204,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.29f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.034f + ShoreBlendOffset,0.15f,0.043f,0.72f),110.27f * Time_delta * 2));
						
						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.89f+bumpTilingXoffset,1.27f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 1.16f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.32f,-0.22f,0.0f,0.36f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(1.71f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 12.17f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.188599f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), 0f, 0.15f *Time_delta * Sm_speed));
						
						//oceanMat.SetFloat ("_GerstnerIntensities", Vector4.Lerp(Fades71,ExtraWavesFactor,110.27f * Time_delta * 2));
						
						waterScaleFactor = Vector3.one;
					}else{
						oceanMat.SetVector("_DistortParams", Vector4.Lerp(Distortions,new Vector4(7.82f + BumpFocusOffset,0.1f+RefractOffset,-0.74f+ FresnelOffset,6f+ FresnelBias),0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat("_Shininess", Mathf.Lerp(oceanMat.GetFloat("_Shininess"),368f,0.27f * Time_delta * Sm_speed));
						oceanMat.SetFloat ("_FresnelScale", Mathf.Lerp (oceanMat.GetFloat ("_FresnelScale"), 0.15f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_InvFadeParemeter", Vector4.Lerp(Fades,new Vector4(0.16f + ShoreBlendOffset,0.1594f,0.043f,4.2f),110.27f * Time_delta * 2));

						oceanMat.SetVector("_BumpTiling", Vector4.Lerp(Fades1,new Vector4(0.12f+bumpTilingXoffset,0.07f + bumpTilingYoffset,0.08f,0.06f),110.27f * Time_delta * 2));
						oceanMat.SetFloat ("_GerstnerIntensity", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity"), 18.21f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetVector("_GAmplitude", Vector4.Lerp(Fades2,new Vector4(0.16f,-0.22f,0.0f,-0.11f) + WaveAmpOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GFrequency", Vector4.Lerp(Fades3,new Vector4(0.5f,1.58f,2.45f,0.75f) + WaveFreqOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSteepness", Vector4.Lerp(Fades4,new Vector4(6.96f,2f,5.92f,2f),110.27f * Time_delta * 2));
						oceanMat.SetVector("_GSpeed", Vector4.Lerp(Fades5,new Vector4(-2.53f,2f,1f,3f) + WaveSpeedOffset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionAB", Vector4.Lerp(Fades6,new Vector4(0.1f,-0.41f,-0.2f,0.1f) + WaveDir1Offset,110.27f * Time_delta * 2));
						oceanMat.SetVector("_GDirectionCD", Vector4.Lerp(Fades7,new Vector4(0.52f,-0.6799f,0.7175f,-0.2f) + WaveDir2Offset,110.27f * Time_delta * 2));								
						oceanMat.SetFloat ("_MultiplyEffect", Mathf.Lerp (oceanMat.GetFloat ("_MultiplyEffect"), 5.71f + DepthColorOffset, 0.15f *Time_delta * Sm_speed));							
						oceanMat.SetFloat ("_GerstnerIntensity1", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity1"), -0.18f, 0.15f *Time_delta * Sm_speed));
						oceanMat.SetFloat ("_GerstnerIntensity2", Mathf.Lerp (oceanMat.GetFloat ("_GerstnerIntensity2"), -0.03f, 0.15f *Time_delta * Sm_speed));

						waterScaleFactor = Vector3.one;
					}

					//prevWaterType = waterType;
				}
			}

			this_transf.localScale = Vector3.Lerp(this_transf.localScale,waterScaleFactor  + waterScaleOffset,0.15f *Time_delta * Sm_speed);
	}

	// Update is called once per frame
	void Update () {
		
			//follow camera
			if (Application.isPlaying) {
				
				if(followCamera && Camera.main != null){
					Vector3 Campos = Camera.main.transform.position;
					TerrainDepthCamera.position = new Vector3(Campos.x,TerrainDepthCamera.position.y,Campos.z);
				}
				
			}

			//shore line
			if (TerrainDepthCamera != null && oceanMat != null) {
				oceanMat.SetVector("_DepthCameraPos", new Vector4(TerrainDepthCamera.position.x,TerrainDepthCamera.position.y,TerrainDepthCamera.position.z,1));
			}
			if (oceanMat != null) {
				oceanMat.SetFloat("_ShoreFadeFactor", ShoreWavesFade);
				oceanMat.SetFloat("_TerrainScale", TerrainScale);
			}

			//audio
			if (SeaAudio != null) {
				if (DisableSeaAudio) {
					//fade it out
					SeaAudio.volume = Mathf.Lerp (SeaAudio.volume, 0, Time.deltaTime * SoundFadeSpeed);
				} else {			
					if (SkyManager != null) {
						if (SkyManager.currentWeatherName == SkyMasterManager.Volume_Weather_types.HeavyStorm | SkyManager.currentWeatherName == SkyMasterManager.Volume_Weather_types.HeavyStormDark) {
							SeaAudio.volume = Mathf.Lerp (SeaAudio.volume, 0, Time.deltaTime * SoundFadeSpeed);
						} else {
							SeaAudio.volume = Mathf.Lerp (SeaAudio.volume, maxAudioVolume, Time.deltaTime * SoundFadeSpeed);
						}
					}			
				}
			}


			//caustics
			if (CausticsMat != null) {
				if(BelowWater){
					CausticsMat.SetFloat("_Intensity",Mathf.Lerp(CausticsMat.GetFloat("_Intensity"),CausticIntensity/10, Time.deltaTime*2));
				}else{
					CausticsMat.SetFloat("_Intensity",Mathf.Lerp(CausticsMat.GetFloat("_Intensity"),CausticIntensity, Time.deltaTime*2));
				}
			}
			if (CausticsProjector != null) {
				CausticsProjector.orthographicSize = CausticSize;
			}


			if (UnderwaterBlur == null) {			
				UnderwaterBlur = Camera.main.gameObject.GetComponent<UnderWaterImageEffect>();

				if(UnderwaterBlur == null){
					//Camera.main.gameObject.AddComponent<UnderWaterImageEffect>();
					Debug.Log("Please add 'UnderWaterImageEffect' script to the main camera"); 
				}
				if(UnderwaterBlur != null){
					UnderwaterBlur.enabled = false;
				}
			}

			//VR cameras
			if ((UnderwaterBlurL == null | UnderwaterBlurR == null) & TerrainManager != null && TerrainManager.LeftCam != null && TerrainManager.RightCam != null) {			
				UnderwaterBlurL = TerrainManager.LeftCam.GetComponent<UnderWaterImageEffect>();
				UnderwaterBlurR = TerrainManager.RightCam.GetComponent<UnderWaterImageEffect>();

				UnderwaterBlurL.enabled = false;
				UnderwaterBlurR.enabled = false;				
			}


			if (Application.isPlaying) {
				//v3.0
				if (SkyManager != null && oceanMat != null) {
			
					UpdateWaterParams(false);
			
				}


				//if underwater, change fog preset
				if (TerrainManager != null) {
					if (Camera.main != null) {
						if (cam_transf.position.y < this_transf.position.y & !DisableUnderwater) {
							if (TerrainManager.FogPreset != 9) {

								if (TerrainManager.FogPreset != 10) {
									prev_preset = TerrainManager.FogPreset;
								}

								//handle scrren drops
								if(SkyManager.ScreenRainDropsMat != null){
									SkyManager.ScreenRainDropsMat.SetFloat("_Speed",1.1f);
								}

								//disable cloud renderer
								//if(SkyManager.currentWeather.VolumeCloud !=null){
								//	SkyManager.currentWeather.VolumeCloud.GetComponent<ParticleRenderer>().enabled = false;
								//}

								//WaterBase.waterQuality = WaterQuality.Underwater;
							}
							TerrainManager.FogPreset = 9;

							if (cam_transf.position.y < this_transf.position.y-DepthFogSwitch) {
								TerrainManager.FogPreset = 10;
							}

							if (UnderwaterBlur != null && !UnderwaterBlur.enabled) {
								UnderwaterBlur.enabled = true;
							}
							if (UnderwaterBlurL != null && !UnderwaterBlurL.enabled) {
								UnderwaterBlurL.enabled = true;
							}
							if (UnderwaterBlurR != null && !UnderwaterBlurR.enabled) {
								UnderwaterBlurR.enabled = true;
							}
							RenderSettings.fogMode = FogMode.ExponentialSquared;
							RenderSettings.fogDensity = 0.05f;
							RenderSettings.fogColor = Fog_Color;
							RenderSettings.fogStartDistance = 0;
							RenderSettings.fogEndDistance = 1000; 

							BelowWater = true;

						} else {
							if (TerrainManager.FogPreset == 9) {
								TerrainManager.FogPreset = prev_preset;

								//RESTORE glow when overwater
								Vector4 Fades = oceanMat.GetVector("_InvFadeParemeter");
								oceanMat.SetVector("_InvFadeParemeter", new Vector4(Fades.x,Fades.y,Fades.z,4));

								//handle scrren drops
								if(SkyManager.ScreenRainDropsMat != null){
									SkyManager.ScreenRainDropsMat.SetFloat("_Speed",6.0f);
								}

								//restore cloud renderer
								//if(SkyManager.currentWeather.VolumeCloud !=null){
								//	SkyManager.currentWeather.VolumeCloud.GetComponent<ParticleRenderer>().enabled = true;
								//}

								//restore fog
								if (prev_fog_mode == 0) {
									RenderSettings.fogMode = FogMode.Exponential;
								}
								if (prev_fog_mode == 1) {
									RenderSettings.fogMode = FogMode.ExponentialSquared;
								}
								if (prev_fog_mode == 2) {
									RenderSettings.fogMode = FogMode.Linear;
								}
								RenderSettings.fogDensity = prev_fog_density;
								RenderSettings.fogColor = prev_fog_color;
								RenderSettings.fogStartDistance = PrevStartEndDist.x;
								RenderSettings.fogEndDistance = PrevStartEndDist.y; 

								WaterBase.waterQuality = WaterQuality.High;
							}

							if (UnderwaterBlur != null && UnderwaterBlur.enabled) {
								UnderwaterBlur.enabled = false;
							}
							if (UnderwaterBlurL != null && UnderwaterBlurL.enabled) {
								UnderwaterBlurL.enabled = false;
							}
							if (UnderwaterBlurR != null && UnderwaterBlurR.enabled) {
								UnderwaterBlurR.enabled = false;
							}

							//end restore fog

							BelowWater = false;
						}
					}
				}
			}
	}
}
}
