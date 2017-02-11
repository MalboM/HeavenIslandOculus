﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Artngame.GIPROXY;

namespace Artngame.SKYMASTER {

	[ExecuteInEditMode]
public class SkyMaster : MonoBehaviour {

	//Global Sky master control script
		[SerializeField]
	public SkyMasterManager SkyManager;
	public SeasonalTerrainSKYMASTER TerrainManager;
	public WaterHandlerSM WaterManager;
	public LightCollisionsPDM GIProxyManager;
		public WaterHeightSM WaterHeightHandle;
	//public GameObject SkyDomeSystem;
	//public GameObject SunSystem;//instantiated system
	//public Transform MapCenter;

		public bool LargeWaterPlane = false;//use large plain instead of tiles
		public float LargeWaterPlaneScale = 1;

		//VOLUME CLOUD Performance controls
		public bool OverridePeformance = false;//override default performance settings of the prefab
		public bool DecoupleWind = true;
		public float UpdateInteraval = 0.2f;
		public int SpreadToFrames = 6;

		//Volume cloud renew
		public bool RenewClouds = false;
		public bool OverrideRenewSettings = false;//override default fade parameters
		public float FadeInSpeed = 0.15f;
		public float FadeOutSpeed = 0.3f;
		public float MaxFadeTime = 1.5f;
		public float Boundary = 90000;
		float lastFadeInSpeed;
		float lastFadeOutSpeed;
		float lastMaxFadeTime;
		float lastBoundary;

	//SCALING
	public float TerrainDepthSize = 0;
		public bool PreviewDepthTexture = false;
	//public float WorldScale=1;

	//TERRAIN SETUP
	public List<Terrain> UnityTerrains = new List<Terrain>();
	public List<GameObject> MeshTerrains = new List<GameObject>();//insert all here for shader setup, first ones will go to skymastermanager for seasonal-fog-shafts control


	public bool DontScaleParticleTranf = false;
	public bool DontScaleParticleProps = false;

		public bool SKY_folder;
		public bool water_folder1;
		public bool foliage_folder1;
		public bool weather_folder1;
		public bool cloud_folder1;
		public bool cloud_folder2;
		public bool camera_folder1;
		public bool terrain_folder1;
		public bool scaler_folder1;
		public bool scaler_folder11;

	// Use this for initialization
	void Start () {
			if (this.gameObject.name == "GameObject") {
				this.gameObject.name = "SKY MASTER MANAGER";
			}
	}
	
	// Update is called once per frame
	void Update () {
			//if (OverridePeformance | RenewClouds) {

			if (Application.isPlaying && SkyManager != null) {

				bool Clouds_exist = false;
				if (SkyManager.currentWeather != null && SkyManager.currentWeather.VolumeScript != null) {
					Clouds_exist = true;
				}
				if (OverridePeformance) {
					if (Clouds_exist) {
						SkyManager.currentWeather.VolumeScript.DecoupledWind = DecoupleWind;
						SkyManager.currentWeather.VolumeScript.max_divider = SpreadToFrames;
						SkyManager.currentWeather.VolumeScript.UpdateInterval = UpdateInteraval;
					}
				}
				if (RenewClouds) {
					if (Clouds_exist) {
						SkyManager.currentWeather.VolumeScript.DestroyOnfadeOut = true;
						SkyManager.currentWeather.VolumeScript.Restore_on_bound = true;
						SkyManager.currentWeather.VolumeScript.Disable_on_bound = true;
						SkyManager.currentWeather.VolumeScript.SmoothInRate = FadeInSpeed;
						SkyManager.currentWeather.VolumeScript.SmoothOutRate = FadeOutSpeed;
						SkyManager.currentWeather.VolumeScript.max_fade_speed = MaxFadeTime;
						SkyManager.currentWeather.VolumeScript.Bound = Boundary;
					}
				} else {
					if (Clouds_exist) {

						//disable renew
						SkyManager.currentWeather.VolumeScript.DestroyOnfadeOut = false;
						SkyManager.currentWeather.VolumeScript.Restore_on_bound = false;
						SkyManager.currentWeather.VolumeScript.Disable_on_bound = false;

//					//reload previous values
//					SkyManager.currentWeather.VolumeScript.SmoothInRate = lastFadeInSpeed;
//					SkyManager.currentWeather.VolumeScript.SmoothOutRate = lastFadeOutSpeed;
//					SkyManager.currentWeather.VolumeScript.max_fade_speed = lastMaxFadeTime;
//					SkyManager.currentWeather.VolumeScript.Bound = lastBoundary;
//
//					//save parameters for restore
//					lastFadeInSpeed = SkyManager.currentWeather.VolumeScript.SmoothInRate;
//					lastFadeOutSpeed = SkyManager.currentWeather.VolumeScript.SmoothOutRate;
//					lastMaxFadeTime = SkyManager.currentWeather.VolumeScript.max_fade_speed;
//					lastBoundary = SkyManager.currentWeather.VolumeScript.Bound;
					}
				}
			
				//}
			}
	}



}
}