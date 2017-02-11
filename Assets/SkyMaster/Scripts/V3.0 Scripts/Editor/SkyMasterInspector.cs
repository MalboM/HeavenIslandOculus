using UnityEditor;
using UnityEditor.Macros;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Artngame.GIPROXY;

namespace Artngame.SKYMASTER {

[CustomEditor(typeof(SkyMaster))] 	
public class SkyMasterInspector : Editor {

		//DEPTH CAMERA for TERRAIN
		public Object DepthCameraPREFAB;
		//WaterHeightSM WaterHeightHandle;
		//public bool PreviewDepthTexture ;

		//FOLIAGE
		public Object snowMESH;

		//SPECIAL FX PRESETS
		public Object ABOMB_Prefab;
		public Object ASTEROIDS_Prefab;
		public Object FREEZE_EFFECT_Prefab;
		public Object AURORA_Prefab;
		public Object CHAIN_LIGHTNING_Prefab;
		public Object SAND_STORM_Prefab;
		public Texture2D SpecialFXOptA;
		public Texture2D SpecialFXOptB;
		public Texture2D SpecialFXOptC;
		public Texture2D SpecialFXOptD;
		public Texture2D SpecialFXOptE;
		public Texture2D SpecialFXOptF;
		public Texture2D InfiniGRASS_ICON;

		//WATER PRESETS
		public Texture2D WaterOptA;
		public Texture2D WaterOptB;
		public Texture2D WaterOptC;
		public Texture2D WaterOptD;
		public Texture2D WaterOptE;
		public Texture2D WaterOptF;
		public Texture2D WaterOptG;
		public Texture2D WaterOptH;
		public Texture2D WaterOptI;
		public Texture2D WaterOptJ;

		public Texture2D UnderWaterOptA;
		public Texture2D UnderWaterOptB;

		//SKY OPTIONS
		public Texture2D SkyOptA;
		public Texture2D SkyOptB;
		public Texture2D SkyOptC;

		//MOON - WATER DROPS
		public Material MoonPhasesMat;
		public Material ScreenRainDropsMat;
		public Object RainDropsPlane;

		//WATER
		public Object WaterPREFAB;
		public Object WaterSamplerPREFAB;
		public Object WaterTilePREFAB;
		public Object WaterTileLargePREFAB;
		public Vector2 Water_tiles = new Vector2(4,6);//how many tiles in x-y
		public Vector3 WaterScale = new Vector3(3,1,1);//initial scale of waves
		public float tileSize = 50;//real world tile size, 1,1,1 scale
		public Material WaterMat;

		public Object CausticsPREFAB;

		public Object SunSystemPREFAB;
//		public GameObject SunSystem;//instantiated system
		public Object MapCenter;
		public Object MiddleTerrain;

		//SKY DOME OPTION
		public Object SkyDomePREFAB;
		//public GameObject SkyDomeSystem;
		public float GlobalScale = 10000;//scale of globe, used for the dome system
		public float SunSystemScale = 11f;

		public bool Exclude_children = false;

		//public bool DontScaleParticleTranf = false;
		//public bool DontScaleParticleProps = false;

		public float ParticlePlaybackScaleFactor = 1f;		
		public float ParticleScaleFactor = 3f;
		public float ParticleDelay = 0f;

		//public bool SKY_folder;
		public Material skyMat;
		public Material skyboxMat;
		public Material skyMatDUAL_SUN;

//		//Terrain
		public Object SampleTerrain;
//		public Transform Mesh_terrain; 
		public Object mesh_terrain;
		public Object UnityTerrain;

		public Material UnityTerrainSnowMat;
		public Material MeshTerrainSnowMat;
		public Material SpeedTreeSnowMat;
		public Object UnityTreePrefab;//use this prefab to grab the Unity tree creator material and update it

		public GameObject FogPresetGradient1_5;
		public GameObject FogPresetGradient6;
		public GameObject FogPresetGradient7;
		public GameObject FogPresetGradient8;
		public GameObject FogPresetGradient9;
		public GameObject FogPresetGradient10;
		public GameObject FogPresetGradient11;
		public GameObject FogPresetGradient12;
		public GameObject FogPresetGradient13;
		public GameObject FogPresetGradient14;

		SerializedProperty	UnityTerrains;
		SerializedProperty	MeshTerrains;

		public Texture2D VCLOUD_SETA_ICON;
		//VOLUME CLOUDS SETB - sheet clouds
		public Object HeavyStormVOLUME_CLOUD;
		public Object DustyStormVOLUME_CLOUD;
		public Object DayClearVOLUME_CLOUD;
		public Object SnowStormVOLUME_CLOUD;
		public Object SnowVOLUME_CLOUD;
		public Object RainStormVOLUME_CLOUD;
		public Object RainVOLUME_CLOUD;
		public Object PinkVOLUME_CLOUD;
		public Object LightningVOLUME_CLOUD;


		public Texture2D VCLOUD_SETB_ICON;
		//VOLUME CLOUDS SETA - single dense, best for camera roll
		public Object HeavyStormVOLUME_CLOUD2;
		public Object DustyStormVOLUME_CLOUD2;
		public Object DayClearVOLUME_CLOUD2;
		public Object SnowStormVOLUME_CLOUD2;
		public Object SnowVOLUME_CLOUD2;
		public Object RainStormVOLUME_CLOUD2;
		public Object RainVOLUME_CLOUD2;
		public Object PinkVOLUME_CLOUD2;
		public Object LightningVOLUME_CLOUD2;

		public Texture2D VCLOUD_SETC_ICON;
		//VOLUME CLOUDS SETC - toon clouds
		public Object HeavyStormVOLUME_CLOUD3;
		public Object DustyStormVOLUME_CLOUD3;
		public Object DayClearVOLUME_CLOUD3;
		public Object SnowStormVOLUME_CLOUD3;
		public Object SnowVOLUME_CLOUD3;
		public Object RainStormVOLUME_CLOUD3;
		public Object RainVOLUME_CLOUD3;
		public Object PinkVOLUME_CLOUD3;
		public Object LightningVOLUME_CLOUD3;

		public Texture2D VCLOUD_SETD_ICON;
		//VOLUME CLOUDS SETD - mobile clouds
		public Object HeavyStormVOLUME_CLOUD4;
		public Object DustyStormVOLUME_CLOUD4;
		public Object DayClearVOLUME_CLOUD4;
		public Object SnowStormVOLUME_CLOUD4;
		public Object SnowVOLUME_CLOUD4;
		public Object RainStormVOLUME_CLOUD4;
		public Object RainVOLUME_CLOUD4;
		public Object PinkVOLUME_CLOUD4;
		public Object LightningVOLUME_CLOUD4;

		//Particle Clouds v3.0
		public GameObject VolumeRain_Heavy;
		public GameObject VolumeRain_Mild;
		public GameObject RefractRain_Heavy;
		public GameObject RefractRain_Mild;

		//Particle Clouds
		public GameObject Rain_Heavy;
		public GameObject Rain_Mild;

		public Material cloud_dome_downMaterial;
		public Material star_dome_Material;

		public Material cloud_upMaterial;
		public Material cloud_downMaterial;
		public Material flat_cloud_upMaterial;
		public Material flat_cloud_downMaterial;
		public Material real_cloud_upMaterial;
		public Material real_cloud_downMaterial;
		public Material Surround_Clouds_Mat;
		
		public GameObject Sun_Ray_CloudPREFAB;
		public GameObject Cloud_DomePREFAB;
		public GameObject Star_particlesPREFAB;
		public GameObject Star_domePREFAB;

		public GameObject Cloud_Domev22PREFAB1;
		public GameObject Cloud_Domev22PREFAB2;

		public GameObject Upper_Dynamic_CloudPREFAB;
		public GameObject Lower_Dynamic_CloudPREFAB;
		public GameObject Upper_Cloud_BedPREFAB;
		public GameObject Lower_Cloud_BedPREFAB;
		public GameObject Upper_Cloud_RealPREFAB;
		public GameObject Lower_Cloud_RealPREFAB;
		public GameObject Upper_Static_CloudPREFAB;
		public GameObject Lower_Static_CloudPREFAB;
		public GameObject Surround_CloudsPREFAB;
		public GameObject Surround_Clouds_HeavyPREFAB;



		//Special FX
//		public GameObject SnowStorm_OBJ;
//		public GameObject[] FallingLeaves_OBJ;
//		public GameObject Butterfly_OBJ;
//		public GameObject[] Tornado_OBJs;
//		public GameObject[] Butterfly3D_OBJ;
//		public GameObject Ice_Spread_OBJ;
//		public GameObject Ice_System_OBJ;
//		public GameObject Lightning_System_OBJ;
//		public GameObject Lightning_OBJ;//single lightning to instantiate 
//		public GameObject Star_particles_OBJ;
//		public GameObject[] Volcano_OBJ;
//		public GameObject VolumeFog_OBJ;
		public GameObject SnowStormPREFAB;
		public GameObject FallingLeavesPREFAB;
		public GameObject ButterflyPREFAB;
		public GameObject TornadoPREFAB;
		public GameObject Butterfly3DPREFAB;
		public GameObject Ice_SpreadPREFAB;
		public GameObject Ice_SystemPREFAB;
		public GameObject Lightning_SystemPREFAB;
		public GameObject LightningPREFAB;//single lightning to instantiate 
		public GameObject VolcanoPREFAB;
		public GameObject VolumeFogPREFAB;

//		public bool water_folder1;
//		public bool foliage_folder1;
//		public bool weather_folder1;
//		public bool cloud_folder1;
//		public bool cloud_folder2;
//		public bool camera_folder1;
//		public bool terrain_folder1;
//		public bool scaler_folder1;
//		public bool scaler_folder11;
		public bool Include_inactive = true;
		public Object MainParticle;

		//WEATHER FOLDER
		WeatherEventSM.Volume_Weather_event_types WeatherEvent_Weather_type;
	//	WeatherEventSM.Volume_Weather_event_types FollowUpWeatherEvent_Weather_type;
		public float WeatherEvent_Chance;
		public float WeatherEvent_StartHour;
		public int WeatherEvent_StartDay;
		public int WeatherEvent_StartMonth;
		public float WeatherEvent_EndHour;
		public int WeatherEvent_EndDay;
		public int WeatherEvent_EndMonth;
		public float WeatherEvent_VolCloudHeight;
		public float WeatherEvent_VolCloudsHorScale;


		//ICONS
		public Texture2D MainIcon1;
		public Texture2D MainIcon2;
		public Texture2D MainIcon3;
		public Texture2D MainIcon4;
		public Texture2D MainIcon5;
		public Texture2D MainIcon6;
		public Texture2D MainIcon7;
		public Texture2D MainIcon8;

		//Global Sky master control script
		private SkyMaster script;
		void Awake()
		{
			script = (SkyMaster)target;
			script.gameObject.transform.position = Vector3.zero;
		}

		public GameObject FindInChildren (GameObject gameObject, string name){		
			foreach(Transform transf in gameObject.GetComponentsInChildren<Transform>()){
				if(transf.name == name){
					Debug.Log(transf.name);
					return transf.gameObject;
				}
			}
			return null;		
		}


		public void OnEnable(){
			
			UnityTerrains = serializedObject.FindProperty ("UnityTerrains");
			MeshTerrains = serializedObject.FindProperty ("MeshTerrains");
		}


		public override void  OnInspectorGUI() {

//		}
//
//		public void  OnGUI () {

			serializedObject.Update ();

			if (script != null && script.SkyManager != null) {
				Undo.RecordObject (script.SkyManager, "Sky Variabe Change");
				if(script.TerrainManager != null){
					Undo.RecordObject (script.TerrainManager, "Terrain Variabe Change");
				}
				if(script.GIProxyManager != null){
					Undo.RecordObject (script.GIProxyManager, "GI Proxy Variabe Change");
				}
				if(script.WaterManager != null){
					Undo.RecordObject (script.WaterManager, "Water Variabe Change");
				}
			}
			//Undo.

			////////////////////////////////////////////////////////////
			
			EditorGUILayout.BeginVertical(GUILayout.MaxWidth(180.0f));
			
			//X_offset_left = 200;
			//Y_offset_top = 100;
			
			//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));	
			GUILayout.Label (MainIcon1,GUILayout.MaxWidth(410.0f));
			
			EditorGUILayout.LabelField("Sky Options",EditorStyles.boldLabel);
			
			EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
			script.SKY_folder = EditorGUILayout.Foldout(script.SKY_folder,"Sky options");
			EditorGUILayout.EndHorizontal();
			
			if(script.SKY_folder){


				EditorGUILayout.BeginHorizontal();
				
				//CHECK IF SKY MANAGER EXISTS and grab that instead, also search its components all exist etc
				if(script.SkyManager == null & script.gameObject.GetComponent<SkyMasterManager>() != null){
					script.SkyManager = script.gameObject.GetComponent<SkyMasterManager>();
					if(script.SkyManager.Mesh_terrain != null){
						if(script.TerrainManager == null){
							script.TerrainManager = script.SkyManager.Mesh_terrain.GetComponentsInChildren<SeasonalTerrainSKYMASTER>(true)[0];
						}
						//if still null, check for terrain
						if(script.TerrainManager == null){
							script.TerrainManager = Terrain.activeTerrain.gameObject.GetComponentsInChildren<SeasonalTerrainSKYMASTER>(true)[0];
						}
					}
				}
				EditorGUILayout.EndHorizontal();


				//EditorGUILayout.Separator();
				GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
				//EditorGUILayout.HelpBox("Define map center",MessageType.None);
				EditorGUILayout.HelpBox("Default sun system",MessageType.None);
				
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Sun system",EditorStyles.boldLabel,GUILayout.MaxWidth(95.0f));
				SunSystemPREFAB = EditorGUILayout.ObjectField(SunSystemPREFAB,typeof( GameObject ),true,GUILayout.MaxWidth(150.0f));
				EditorGUILayout.EndHorizontal();
				
				//EditorGUILayout.Separator();
				GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
				//EditorGUILayout.HelpBox("Default sun system",MessageType.None);
				EditorGUILayout.HelpBox("Define map center",MessageType.None);
				
				//SunSystem = EditorGUILayout.ObjectField(SunSystem,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Map center",EditorStyles.boldLabel,GUILayout.MaxWidth(95.0f));
				if(script.SkyManager != null){
					script.SkyManager.MapCenter = EditorGUILayout.ObjectField(script.SkyManager.MapCenter,typeof( Transform ),true,GUILayout.MaxWidth(150.0f)) as Transform;
				}else{
					MapCenter = EditorGUILayout.ObjectField(MapCenter,typeof( GameObject ),true,GUILayout.MaxWidth(150.0f)) as GameObject;
				}
				EditorGUILayout.EndHorizontal();


				//EditorGUILayout.EndVertical();
				//EditorGUILayout.BeginVertical();

				//EditorGUILayout.BeginHorizontal();



				if (GUILayout.Button ("Add Sky")) {
					// --------------- ADD SKY --------------- 
					if(script.SkyManager == null & SunSystemPREFAB != null){

						// --------------- ADD SKY SCRIPT --------------- 
						script.gameObject.AddComponent<SkyMasterManager>();
						script.SkyManager = script.gameObject.GetComponent<SkyMasterManager>();
						script.SkyManager.PlanetScale = GlobalScale;//define for skybox, get from dome in skydome version
						script.SkyManager.DefinePlanetScale = true;
						script.SkyManager.Unity5 = true;

						// --------------- DEFINE MAP CENTER --------------- 
						if(script.SkyManager.MapCenter != null){
							//(MapCenter as GameObject).transform.parent = script.transform;
						}else{
							if(MapCenter == null){//if not defined in configurator inspector
								script.SkyManager.MapCenter = (new GameObject()).transform;
								script.SkyManager.MapCenter.gameObject.name = "Map Center";
								script.SkyManager.MapCenter.parent = script.transform;
								MapCenter = script.SkyManager.MapCenter.gameObject;
							}else{
								script.SkyManager.MapCenter = (MapCenter as GameObject).transform;
								script.SkyManager.MapCenter.gameObject.name = "Map Center";
								script.SkyManager.MapCenter.parent = script.transform;
							}

							//IF UNITY TERRAIN - PUT IN MIDDLE

							//IF MESH TERRAIN - CENTER
						}
			//			GameObject MapCenterOBJ = MapCenter as GameObject;

						// --------------- INSTANTIATE SUN - MOON SYSTEM --------------- 
						script.SkyManager.SunSystem = (GameObject)Instantiate(SunSystemPREFAB);
						script.SkyManager.SunSystem.transform.position = script.SkyManager.MapCenter.transform.position;

						script.SkyManager.Current_Time = 20.5f;

						script.SkyManager.SunSystem.transform.eulerAngles = new Vector3(28.14116f,170,180);
						script.SkyManager.SunSystem.name = "Sun System";
						script.SkyManager.SunSystem.transform.localScale = SunSystemScale*Vector3.one;
						script.SkyManager.SunSystem.transform.parent = script.transform;
				//		script.SkyManager.SunSystem = SunSystem;
						script.SkyManager.MoonPhasesMat = MoonPhasesMat;
						script.SkyManager.MoonPhases = true;

						// --------------- FIND SUN SYSTEM CENTER AND ALIGN TO MAP CENTER --------------- 
						GameObject SunSystemCenter = FindInChildren(script.SkyManager.SunSystem,"Sun Target");
						Vector3 Distance = SunSystemCenter.transform.position - script.SkyManager.MapCenter.transform.position;
						script.SkyManager.SunSystem.transform.position -= Distance;
						script.SkyManager.SunTarget = SunSystemCenter;

						// --------------- ASSIGN SUN - MOON LIGHTS TO SKY MANAGER --------------- 
						script.SkyManager.SunObj = FindInChildren(script.SkyManager.SunSystem,"SunBody");
						script.SkyManager.MoonObj = FindInChildren(script.SkyManager.SunSystem,"MoonBody");
						script.SkyManager.SUN_LIGHT = script.SkyManager.SunObj.transform.parent.gameObject;
						script.SkyManager.SUPPORT_LIGHT = FindInChildren(script.SkyManager.SUN_LIGHT,"MoonLight");
						script.SkyManager.MOON_LIGHT = FindInChildren(FindInChildren(script.SkyManager.SunSystem,"MoonBody"),"MoonLight");

						//ADD CLOUDS
						script.SkyManager.Upper_Dynamic_Cloud = (GameObject)Instantiate(Upper_Dynamic_CloudPREFAB); script.SkyManager.Upper_Dynamic_Cloud.transform.parent = script.transform;
						script.SkyManager.Lower_Dynamic_Cloud = (GameObject)Instantiate(Lower_Dynamic_CloudPREFAB); script.SkyManager.Lower_Dynamic_Cloud.transform.parent = script.transform;
						script.SkyManager.Upper_Cloud_Bed = (GameObject)Instantiate(Upper_Cloud_BedPREFAB);			script.SkyManager.Upper_Cloud_Bed.transform.parent = script.transform;
						script.SkyManager.Lower_Cloud_Bed = (GameObject)Instantiate(Lower_Cloud_BedPREFAB);			script.SkyManager.Lower_Cloud_Bed.transform.parent = script.transform;
						script.SkyManager.Upper_Cloud_Real = (GameObject)Instantiate(Upper_Cloud_RealPREFAB);		script.SkyManager.Upper_Cloud_Real.transform.parent = script.transform;
						script.SkyManager.Lower_Cloud_Real = (GameObject)Instantiate(Lower_Cloud_RealPREFAB);		script.SkyManager.Lower_Cloud_Real.transform.parent = script.transform;
						script.SkyManager.Upper_Static_Cloud = (GameObject)Instantiate(Upper_Static_CloudPREFAB);	script.SkyManager.Upper_Static_Cloud.transform.parent = script.transform;
						script.SkyManager.Lower_Static_Cloud = (GameObject)Instantiate(Lower_Static_CloudPREFAB);	script.SkyManager.Lower_Static_Cloud.transform.parent = script.transform;
						script.SkyManager.Surround_Clouds = (GameObject)Instantiate(Surround_CloudsPREFAB);			script.SkyManager.Surround_Clouds.transform.parent = script.transform;
						script.SkyManager.Surround_Clouds_Heavy = (GameObject)Instantiate(Surround_Clouds_HeavyPREFAB);	script.SkyManager.Surround_Clouds_Heavy.transform.parent = script.transform;

						script.SkyManager.cloud_upMaterial 			=  script.SkyManager.Upper_Dynamic_Cloud.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;
						script.SkyManager.cloud_downMaterial 		=  script.SkyManager.Upper_Dynamic_Cloud.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;
						script.SkyManager.flat_cloud_upMaterial 	=  script.SkyManager.Upper_Cloud_Bed.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;
						script.SkyManager.flat_cloud_downMaterial 	=  script.SkyManager.Lower_Cloud_Bed.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;
						script.SkyManager.Surround_Clouds_Mat 		=  script.SkyManager.Surround_Clouds.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;
						script.SkyManager.real_cloud_upMaterial 	=  script.SkyManager.Upper_Cloud_Real.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;
						script.SkyManager.real_cloud_downMaterial 	=  script.SkyManager.Lower_Cloud_Real.GetComponentsInChildren<ParticleSystemRenderer>(true)[0].sharedMaterial;

						script.SkyManager.Sun_Ray_Cloud = (GameObject)Instantiate(Sun_Ray_CloudPREFAB); script.SkyManager.Sun_Ray_Cloud.transform.parent = script.transform;
						script.SkyManager.Cloud_Dome = (GameObject)Instantiate(Cloud_DomePREFAB); 	script.SkyManager.Cloud_Dome.transform.parent = script.transform;
						script.SkyManager.Star_particles_OBJ = (GameObject)Instantiate(Star_particlesPREFAB); script.SkyManager.Star_particles_OBJ.transform.parent = script.transform;
						script.SkyManager.Star_particles_OBJ.transform.position = script.SkyManager.MapCenter.position;

						script.SkyManager.StarDome = (GameObject)Instantiate(Star_domePREFAB); 	script.SkyManager.StarDome.transform.parent = script.transform;
						script.SkyManager.CloudDomeL1 = (GameObject)Instantiate(Cloud_Domev22PREFAB1); 	script.SkyManager.CloudDomeL1.transform.parent = script.transform;
						script.SkyManager.CloudDomeL2 = (GameObject)Instantiate(Cloud_Domev22PREFAB2); 	script.SkyManager.CloudDomeL2.transform.parent = script.transform;

						script.SkyManager.cloud_dome_downMaterial = script.SkyManager.Cloud_Dome.GetComponentsInChildren<Renderer>(true)[0].sharedMaterial;
						script.SkyManager.star_dome_Material = script.SkyManager.StarDome.GetComponentsInChildren<Renderer>(true)[0].sharedMaterial;
						//script.SkyManager.star = script.SkyManager.Lower_Cloud_Real.GetComponentsInChildren<Renderer>(true)[0].sharedMaterial;

						script.SkyManager.CloudDomeL1Mat = script.SkyManager.CloudDomeL1.GetComponentsInChildren<Renderer>(true)[0].sharedMaterial;
						script.SkyManager.CloudDomeL2Mat = script.SkyManager.CloudDomeL2.GetComponentsInChildren<Renderer>(true)[0].sharedMaterial;

						//DUAL SUN HANDLE

						//FOGS
						script.SkyManager.VFogsPerVWeather.Add(0);//sunny--
						script.SkyManager.VFogsPerVWeather.Add(13);//foggy
						script.SkyManager.VFogsPerVWeather.Add(14);//heavy fog
						script.SkyManager.VFogsPerVWeather.Add(0);//tornado
						script.SkyManager.VFogsPerVWeather.Add(7);//snow storm--

						script.SkyManager.VFogsPerVWeather.Add(7);//freeze storm
						script.SkyManager.VFogsPerVWeather.Add(0);//flat clouds
						script.SkyManager.VFogsPerVWeather.Add(0);//lightning storm
						script.SkyManager.VFogsPerVWeather.Add(7);//heavy storm--
						script.SkyManager.VFogsPerVWeather.Add(7);//heavy storm dark--

						script.SkyManager.VFogsPerVWeather.Add(12);//cloudy--
						script.SkyManager.VFogsPerVWeather.Add(0);//rolling fog
						script.SkyManager.VFogsPerVWeather.Add(0);//volcano erupt
						script.SkyManager.VFogsPerVWeather.Add(14);//rain

						//LOCALIZE EFFECTS
						script.SkyManager.Snow_local = true;
						script.SkyManager.Mild_rain_local = true;
						script.SkyManager.Heavy_rain_local = true;
						script.SkyManager.Fog_local = true;
						script.SkyManager.Butterflies_local = true;

						//ADD RAIN
						script.SkyManager.SnowStorm_OBJ	=(GameObject)Instantiate(SnowStormPREFAB); script.SkyManager.SnowStorm_OBJ.transform.parent = script.transform;
						script.SkyManager.Butterfly_OBJ	=(GameObject)Instantiate(ButterflyPREFAB); script.SkyManager.Butterfly_OBJ.transform.parent = script.transform;

						//Parent ice spread to snow storm system and assign pool particle to collision manager
						//v.3.0.3	script.SkyManager.Ice_System_OBJ=(GameObject)Instantiate(Ice_SystemPREFAB); script.SkyManager.Ice_System_OBJ.transform.parent = script.transform;
						//v.3.0.3	script.SkyManager.Ice_Spread_OBJ=(GameObject)Instantiate(Ice_SpreadPREFAB); script.SkyManager.Ice_Spread_OBJ.transform.parent = script.SkyManager.SnowStorm_OBJ.transform;
						//v.3.0.3	script.SkyManager.Ice_Spread_OBJ.GetComponentsInChildren<ParticleCollisionsSKYMASTER>(true)[0].ParticlePOOL = script.SkyManager.Ice_System_OBJ.GetComponentsInChildren<ParticlePropagationSKYMASTER>(true)[0].gameObject;


						script.SkyManager.Lightning_OBJ	=(GameObject)Instantiate(LightningPREFAB); script.SkyManager.Lightning_OBJ.transform.parent = script.transform;
								
						script.SkyManager.Lightning_System_OBJ	=(GameObject)Instantiate(Lightning_SystemPREFAB); script.SkyManager.Lightning_System_OBJ.transform.parent = script.transform;
						script.SkyManager.VolumeFog_OBJ			=(GameObject)Instantiate(VolumeFogPREFAB); script.SkyManager.VolumeFog_OBJ.transform.parent = script.transform;
						script.SkyManager.Rain_Heavy			=(GameObject)Instantiate(Rain_Heavy); script.SkyManager.Rain_Heavy.transform.parent = script.transform;
						script.SkyManager.Rain_Mild				=(GameObject)Instantiate(Rain_Mild); script.SkyManager.Rain_Mild.transform.parent = script.transform;
						script.SkyManager.VolumeRain_Heavy	=(GameObject)Instantiate(VolumeRain_Heavy); script.SkyManager.VolumeRain_Heavy.transform.parent = script.transform;
						script.SkyManager.VolumeRain_Mild	=(GameObject)Instantiate(VolumeRain_Mild); script.SkyManager.VolumeRain_Mild.transform.parent = script.transform;
								
						//init arrays
						script.SkyManager.FallingLeaves_OBJ = new GameObject[1];
						script.SkyManager.Tornado_OBJs = new GameObject[1];
						//v.3.0.3 script.SkyManager.Butterfly3D_OBJ = new GameObject[1];
						script.SkyManager.Volcano_OBJ = new GameObject[1];

						script.SkyManager.FallingLeaves_OBJ[0]	=	(GameObject)Instantiate(FallingLeavesPREFAB); script.SkyManager.FallingLeaves_OBJ[0].transform.parent = script.transform;					
						script.SkyManager.Tornado_OBJs[0]		=	(GameObject)Instantiate(TornadoPREFAB); script.SkyManager.Tornado_OBJs[0].transform.parent = script.transform;				
	//v.3.0.3			script.SkyManager.Butterfly3D_OBJ[0]	=(GameObject)Instantiate(Butterfly3DPREFAB); script.SkyManager.Butterfly3D_OBJ[0].transform.parent = script.transform;						
						script.SkyManager.Volcano_OBJ[0]	=(GameObject)Instantiate(VolcanoPREFAB); script.SkyManager.Volcano_OBJ[0].transform.parent = script.transform;

						script.SkyManager.RefractRain_Mild 	= (GameObject)Instantiate(RefractRain_Mild); script.SkyManager.RefractRain_Mild.transform.parent = script.transform;
						script.SkyManager.RefractRain_Heavy = (GameObject)Instantiate(RefractRain_Heavy); script.SkyManager.RefractRain_Heavy.transform.parent = script.transform;

						//SETUP GI PROXY for sun
						//SET PLAYER
						if(!script.SkyManager.Tag_based_player){
							script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Tag_based_player = false;
							if(script.SkyManager.Hero != null){
								script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().HERO = script.SkyManager.Hero;
							}else{
								Debug.Log("Note: Hero has not been defined. The Main Camera will be used as the player. For a specific player usage, define a hero in 'hero' parameters in Sky Master Manager and LightColliionsPDM scripts.");
								Debug.Log("The 'tag based player' option may also be used to define player by tag (default tag is 'Player')");
							}
						}else{
							script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Tag_based_player = true;
						}

						//DEFINE START PRESET
						script.SkyManager.Preset = 9;//v3.0 day time - no red sun
						script.SkyManager.Auto_Cycle_Sky = true;
						script.SkyManager.SPEED = 1;
						script.SkyManager.currentWeatherName = SkyMasterManager.Volume_Weather_types.Cloudy;

						//ASSIGN SKY MATERIALS
						if(script.SkyManager.SunObj2 != null){
							script.SkyManager.skyboxMat = skyMatDUAL_SUN;
						}else{
							script.SkyManager.skyboxMat = skyboxMat;
						}
						RenderSettings.skybox = script.SkyManager.skyboxMat;
						//Debug.Log(script.SkyManager.skyMat.name);
						//Debug.Log(RenderSettings.skybox.name);
						script.SkyManager.skyMat = skyMat;

					}else{
						if(script.SkyManager != null){
							Debug.Log ("Sky Manager exists");
						}
						if(SunSystemPREFAB != null){
							Debug.Log ("Please add a sun system");
						}
					}

					//SETUP CAMERA
					Camera.main.farClipPlane = 30000;
					Camera.main.transform.Translate(0,10,0);
					Camera.main.hdr = true;
					Camera.main.renderingPath = RenderingPath.DeferredShading;
				}


				//EditorGUILayout.EndHorizontal();


				//EditorGUILayout.EndVertical();
				//EditorGUILayout.BeginVertical();


				//EditorGUILayout.BeginHorizontal();

				if(script.SkyManager != null){


					//GI PROXY
					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));
					EditorGUILayout.HelpBox("Setup GI Proxy (real time GI approximation)",MessageType.None);
					if (GUILayout.Button ("Setup GI Proxy on Sun")) {
						GameObject GIProxyPool = new GameObject();
						GIProxyPool.name = "GI Proxy Sun Bounce Lights";
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().LightPool = GIProxyPool;
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().enabled = true;
						//add cut height based on terrain or map center
						float cut_height = 0;
						if(script.SkyManager.MapCenter != null){
							cut_height = script.SkyManager.MapCenter.position.y;
						}
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Cut_height = cut_height;
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().extend_X = 2* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().extend_Y = 6* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().PointLight_Radius = 70* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().PointLight_Radius_2ond = 70* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Hero_offset = new Vector3(2* (script.SkyManager.WorldScale/20),0,40* (script.SkyManager.WorldScale/20));
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Bounce_Range = 64* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().min_dist_to_last_light = 5* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().min_dist_to_source =1000* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().min_density_dist = 9* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Follow_dist = 5* (script.SkyManager.WorldScale/20);

						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().min_dist_to_camera = 65* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().max_hit_light_dist = 6* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().placement_offset = 6* (script.SkyManager.WorldScale/20);
						script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Cut_off_height = 9* (script.SkyManager.WorldScale/20);
					}

					//PLAYER

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));
					EditorGUILayout.HelpBox("Define player",MessageType.None);
					script.SkyManager.Tag_based_player = EditorGUILayout.Toggle("Define player by tag", script.SkyManager.Tag_based_player,GUILayout.MaxWidth(180.0f));
					
					if(!script.SkyManager.Tag_based_player){
						
						//EditorGUILayout.LabelField("Define player object",EditorStyles.boldLabel);
						script.SkyManager.Hero = EditorGUILayout.ObjectField (script.SkyManager.Hero, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
					}

					if (GUILayout.Button ("Define player")) {
						//Scale sun system after initial addition
						//SET PLAYER
						if(!script.SkyManager.Tag_based_player){
							script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Tag_based_player = false;
							if(script.SkyManager.Hero != null){
								script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().HERO = script.SkyManager.Hero;
							}else{
								Debug.Log("Note: Hero has not been defined. The Main Camera will be used as the player. For a specific player usage, define a hero in 'hero' parameters in Sky Master Manager and LightColliionsPDM scripts.");
								Debug.Log("The 'tag based player' option may also be used to define player by tag (default tag is 'Player')");
							}
						}else{
							script.SkyManager.SUN_LIGHT.GetComponent<LightCollisionsPDM>().Tag_based_player = true;
						}
					}


					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));
					//DUAL SUN
					EditorGUILayout.HelpBox("Define 2ond sun",MessageType.None);
					//EditorGUILayout.LabelField("Define 2ond sun",EditorStyles.boldLabel);
					script.SkyManager.SunObj2 = EditorGUILayout.ObjectField (script.SkyManager.SunObj2, typeof(GameObject), true, GUILayout.MaxWidth (180.0f)) as GameObject;
					//script.SkyManager.DualSunsFactors

				
				
					if (GUILayout.Button ("Add Second Sun")) {
						// --------------- ASSIGN PROPER MATERIAL --------------- 
						if(script.SkyManager.SunObj2 != null){
							script.SkyManager.skyboxMat = skyMatDUAL_SUN;
						}else{
							Debug.Log("Please enter a Gameobject to define the 2ond sun position first");
						}
						// --------------- ADD SUN OBJECT --------------- 

						// --------------- REGISTER OBJECT IN SKY MANAGER --------------- 
					}
					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));



					//ADD WINDZONE

					GUILayout.Label ("Windzone");
					
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Add windzone"), GUILayout.Width (120))) {
						if (script.SkyManager.windZone == null) {
							GameObject WindZone = new GameObject ();
							//script.SkyManager.windZone.gameObject = WindZone;
							WindZone.AddComponent<WindZone> ();
							script.SkyManager.windZone = WindZone.GetComponent<WindZone> ();
							WindZone.name = "Sky Master windzone";
						} else {
							Debug.Log ("Windzone exists");
						}
					}
					//Handle external zone
//					if(script.SkyManager.windZone==null){
//						if(script.SkyManager.windZone.gameObject!=null){
//							script.SkyManager.windZone = script.SkyManager.windZone.gameObject.GetComponent<WindZone>();
//						}
//					}
					
					//Wind_Zone = EditorGUILayout.ObjectField (Wind_Zone, typeof(Transform), true, GUILayout.MaxWidth (180.0f)) as Transform;
					//script.windzone = Wind_Zone;
					script.SkyManager.windZone = EditorGUILayout.ObjectField (script.SkyManager.windZone, typeof(WindZone), true, GUILayout.MaxWidth (180.0f)) as WindZone;


					EditorGUILayout.EndHorizontal ();	

					if(script.SkyManager.windZone != null){
						GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
						EditorGUILayout.HelpBox("Wind intensity",MessageType.None);
						EditorGUILayout.BeginHorizontal();
						script.SkyManager.windZone.windMain = EditorGUILayout.Slider(script.SkyManager.windZone.windMain,0,24,GUILayout.MaxWidth(195.0f));
						EditorGUILayout.EndHorizontal();

						GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
						EditorGUILayout.HelpBox("Wind direction",MessageType.None);
						EditorGUILayout.BeginHorizontal();
						float aurlerY = EditorGUILayout.Slider(script.SkyManager.windZone.transform.eulerAngles.y,0,360,GUILayout.MaxWidth(195.0f));
						Vector3 angles = script.SkyManager.windZone.gameObject.transform.eulerAngles;
						script.SkyManager.windZone.gameObject.transform.eulerAngles = new Vector3(angles.x,aurlerY,angles.z);
						EditorGUILayout.EndHorizontal();

						GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
						EditorGUILayout.HelpBox("Wind Turbulence",MessageType.None);
						EditorGUILayout.BeginHorizontal();
						script.SkyManager.windZone.windTurbulence = EditorGUILayout.Slider(script.SkyManager.windZone.windTurbulence,-50,50,GUILayout.MaxWidth(195.0f));
						EditorGUILayout.EndHorizontal();
					}




				}

				GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));

				if (GUILayout.Button ("Add Sky Dome")) {
					//Use a dome instead of skybox, if required
					script.SkyManager.SkyDomeSystem = ((GameObject)Instantiate(SkyDomePREFAB)).transform;
					GameObject SkyDomeSystemCenter = FindInChildren(script.SkyManager.SkyDomeSystem.gameObject,"Sun Target");
					Vector3 Distance = SkyDomeSystemCenter.transform.position - script.SkyManager.MapCenter.position;// - (MapCenter as GameObject).transform.position;
					script.SkyManager.SkyDomeSystem.transform.position -= Distance;
					//DEFINE PROPER PRESET
					script.SkyManager.Preset = 5;
				}
				
				//EditorGUILayout.EndHorizontal();
				
				EditorGUIUtility.wideMode = false;


//				GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
//				//EditorGUILayout.HelpBox("Define map center",MessageType.None);
//				EditorGUILayout.HelpBox("Scale sun-moon system",MessageType.None);
//				//script.SkyManager.SunSystem.transform.localScale = EditorGUILayout.FloatField(script.SkyManager.SunSystem.transform.localScale.x,GUILayout.MaxWidth(95.0f))*Vector3.one;
//				EditorGUILayout.BeginHorizontal();
//				script.SkyManager.SunSystem.transform.localScale = EditorGUILayout.Slider(script.SkyManager.SunSystem.transform.localScale.x,1,20,GUILayout.MaxWidth(195.0f))*Vector3.one;
//				EditorGUILayout.EndHorizontal();

				if(script.SkyManager != null){

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Season",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.Season = (int)EditorGUILayout.Slider(script.SkyManager.Season,0,4,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Time of Day",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.Current_Time = EditorGUILayout.Slider(script.SkyManager.Current_Time,0,24,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.HelpBox("Shift dawn",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.Shift_dawn = EditorGUILayout.Slider(script.SkyManager.Shift_dawn,-1.5f,0,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					script.SkyManager.AutoSunPosition = EditorGUILayout.Toggle("LatLonTOD", script.SkyManager.AutoSunPosition,GUILayout.MaxWidth(380.0f));
					EditorGUILayout.EndHorizontal();

					if(script.SkyManager.AutoSunPosition){
						EditorGUILayout.HelpBox("Latitude",MessageType.None);
						EditorGUILayout.BeginHorizontal();
						script.SkyManager.Latitude = EditorGUILayout.Slider(script.SkyManager.Latitude,-89.99f,89.99f,GUILayout.MaxWidth(195.0f));
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.HelpBox("Longitude",MessageType.None);
						EditorGUILayout.BeginHorizontal();
						script.SkyManager.Longitude = EditorGUILayout.Slider(script.SkyManager.Longitude,-180,180,GUILayout.MaxWidth(195.0f));
						EditorGUILayout.EndHorizontal();
					}
					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));

					EditorGUILayout.HelpBox("Sun speed - Use to adjust day time length",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.SPEED = EditorGUILayout.Slider(script.SkyManager.SPEED,0.1f,6,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Horizontal Sun Position",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.Rot_Sun_Y = EditorGUILayout.Slider(script.SkyManager.Rot_Sun_Y,-360,360,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Midday sun intensity",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.Max_sun_intensity = EditorGUILayout.Slider(script.SkyManager.Max_sun_intensity,0.3f,5,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Moon light intensity",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.max_moon_intensity = EditorGUILayout.Slider(script.SkyManager.max_moon_intensity,0.1f,3,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					//v3.0.2
					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Sky Coloration intensity",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.SkyColorationOffset = EditorGUILayout.Slider(script.SkyManager.SkyColorationOffset,-0.25f,0.5f,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();

					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					EditorGUILayout.HelpBox("Moon halo intensity",MessageType.None);
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.Moon_glow = EditorGUILayout.Slider(script.SkyManager.Moon_glow,0.1f,20,GUILayout.MaxWidth(195.0f));
					EditorGUILayout.EndHorizontal();


					GUILayout.Box("",GUILayout.Height(2),GUILayout.Width(410));	
					//EditorGUILayout.HelpBox("Define map center",MessageType.None);
					EditorGUILayout.HelpBox("World scale",MessageType.None);
					//script.SkyManager.SunSystem.transform.localScale = EditorGUILayout.FloatField(script.SkyManager.SunSystem.transform.localScale.x,GUILayout.MaxWidth(95.0f))*Vector3.one;
					EditorGUILayout.BeginHorizontal();
					script.SkyManager.WorldScale = EditorGUILayout.Slider(script.SkyManager.WorldScale,1,50,GUILayout.MaxWidth(195.0f));
					script.SkyManager.SunSystem.transform.localScale = (script.SkyManager.WorldScale/2)*Vector3.one;
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginHorizontal();
					script.DontScaleParticleProps = EditorGUILayout.Toggle("No particle size scaling", script.DontScaleParticleProps,GUILayout.MaxWidth(380.0f));
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					script.DontScaleParticleTranf = EditorGUILayout.Toggle("No particles bounding box scaling", script.DontScaleParticleTranf,GUILayout.MaxWidth(380.0f));
					EditorGUILayout.EndHorizontal();

					//SCALE shader based cloud dome


					//SCALE PARTICLE CLOUDS
					if(script.SkyManager.WorldScale != script.SkyManager.prevWorldScale){

						//scale volume cloud positioning
						script.SkyManager.VolCloudsHorScale = 1000 * (script.SkyManager.WorldScale/20);
						script.SkyManager.VolCloudHeight = 650 * (script.SkyManager.WorldScale/20);

						//scale stars
						float prev_scale = script.SkyManager.prevWorldScale;
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Star_particles_OBJ, true);

						//SCALE shader based cloud dome
						script.SkyManager.CloudDomeL1.transform.localScale = new Vector3(35404,33044,35405)*(script.SkyManager.WorldScale/20);
						script.SkyManager.CloudDomeL2.transform.localScale = new Vector3(35404,21190,35405)*(script.SkyManager.WorldScale/20);

						//scale clouds
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Sun_Ray_Cloud, true);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Star_particles_OBJ, false);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Upper_Dynamic_Cloud, true);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Lower_Dynamic_Cloud, true);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Upper_Cloud_Bed, true);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Lower_Cloud_Bed, true);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Lower_Cloud_Real, true);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Upper_Cloud_Real, true);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Upper_Static_Cloud, true);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Lower_Static_Cloud, true);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Surround_Clouds, true);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Surround_Clouds_Heavy, true);

						//WEATHER PARTICLES
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.SnowStorm_OBJ, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Butterfly_OBJ, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Ice_Spread_OBJ, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Ice_System_OBJ, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Lightning_OBJ, false);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Lightning_System_OBJ, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.VolumeFog_OBJ, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Rain_Heavy, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Rain_Mild, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.VolumeRain_Heavy, false);

						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.VolumeRain_Mild, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.RefractRain_Heavy, false);
						ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.RefractRain_Mild, false);

						for(int i = 0;i<script.SkyManager.FallingLeaves_OBJ.Length;i++){
							ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.FallingLeaves_OBJ[i], false);
						}
						for(int i = 0;i<script.SkyManager.Tornado_OBJs.Length;i++){
							ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Tornado_OBJs[i], false);
						}
						for(int i = 0;i<script.SkyManager.Butterfly3D_OBJ.Length;i++){
							ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Butterfly3D_OBJ[i], false);
						}
						for(int i = 0;i<script.SkyManager.Volcano_OBJ.Length;i++){
							ScaleMe((script.SkyManager.WorldScale/prev_scale), script.SkyManager.Volcano_OBJ[i], false);
						}

						//SKY DOME
						if(script.SkyManager.SkyDomeSystem!=null){
							script.SkyManager.SkyDomeSystem.localScale = new Vector3(11936.62f,11936.62f,11936.62f)*(script.SkyManager.WorldScale/20);
						}

						//RAIN DROPS PLANE
						if(script.SkyManager.RainDropsPlane!=null){
							script.SkyManager.RainDropsPlane.transform.localScale = new Vector3(0.6486322f,0.6486322f,0.6486322f)*(script.SkyManager.WorldScale/20);
						}

						//WATER
						if(script.SkyManager.water!=null){
							script.SkyManager.water.localScale = new Vector3(3f,1f,1f)*(script.SkyManager.WorldScale/20);
						}

						script.SkyManager.prevWorldScale = script.SkyManager.WorldScale;
					}

					//SCALE WATER
				


					//REFINE SKY
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.HelpBox("Setup Sky & Fog(Realistic) - Add & Setup terrain first",MessageType.None);
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					if(GUILayout.Button(SkyOptA,GUILayout.MaxWidth(380.0f),GUILayout.MaxHeight(120.0f))){
						script.SkyManager.Preset = 11;
						if(script.TerrainManager != null){
							script.TerrainManager.FogPreset = 11;
						}else{
							Debug.Log("Please add a terrain and set it up with SkyMaster in order to set the volumetric fog preset in SeasonalTerrainSKYMASTER script");
						}
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.HelpBox("Setup Sky & Fog (Fantasy) - Add & Setup terrain first",MessageType.None);
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					if(GUILayout.Button(SkyOptB,GUILayout.MaxWidth(380.0f),GUILayout.MaxHeight(120.0f))){
						script.SkyManager.Preset = 0;
						if(script.TerrainManager != null){
							script.TerrainManager.FogPreset = 0;
						}else{
							Debug.Log("Please add a terrain and set it up with SkyMaster in order to set the volumetric fog preset in SeasonalTerrainSKYMASTER script");
						}
					}
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.HelpBox("Setup Sky & Fog (Mild) - Add & Setup terrain first",MessageType.None);
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.BeginHorizontal();
					if(GUILayout.Button(SkyOptC,GUILayout.MaxWidth(380.0f),GUILayout.MaxHeight(120.0f))){
						script.SkyManager.Preset = 9;
						if(script.TerrainManager != null){
							script.TerrainManager.FogPreset = 0;
						}else{
							Debug.Log("Please add a terrain and set it up with SkyMaster in order to set the volumetric fog preset in SeasonalTerrainSKYMASTER script");
						}
					}
					EditorGUILayout.EndHorizontal();
				}
//				EditorGUILayout.BeginHorizontal();
//				
//				ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
//				ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
//				EditorGUILayout.EndHorizontal();
				
				//	MainParticle =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
				
			//	Exclude_children = EditorGUILayout.Toggle("Exclude children", Exclude_children,GUILayout.MaxWidth(180.0f));
			//	Include_inactive = EditorGUILayout.Toggle("Include inactive", Include_inactive,GUILayout.MaxWidth(180.0f));


				
			}
			EditorGUILayout.EndVertical();
			
			////////////////////////////////////////////////////////////





			if (script.SkyManager != null) {



				/////////////////////////////////////////////////////////////////////////////// VOLUMETRIC CLOUDS /////////////////////////////////
				EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));			
				//X_offset_left = 200;
				//Y_offset_top = 100;			



			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				GUILayout.Label (MainIcon2, GUILayout.MaxWidth (410.0f));
			
				EditorGUILayout.LabelField ("Volumetric & Shader based Clouds", EditorStyles.boldLabel);		

				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.cloud_folder2 = EditorGUILayout.Foldout (script.cloud_folder2, "Shader based Cloud dome");
				EditorGUILayout.EndHorizontal ();
			
				if (script.cloud_folder2) {
					//control L1 dome
					EditorGUILayout.HelpBox ("Cloud shift - density control", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.L1CloudDensOffset = EditorGUILayout.Slider (script.SkyManager.L1CloudDensOffset, 0, 10, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud lower layer size", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.L1CloudSize = EditorGUILayout.Slider (script.SkyManager.L1CloudSize, 0.1f, 10, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud coverage offset", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.L1CloudCoverOffset = EditorGUILayout.Slider (script.SkyManager.L1CloudCoverOffset, 0, 0.1f, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud ambience", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.L1Ambience = EditorGUILayout.Slider (script.SkyManager.L1Ambience, 0.1f, 1.5f, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();
				}


				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));

				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.cloud_folder1 = EditorGUILayout.Foldout (script.cloud_folder1, "Volumetric Clouds");
				EditorGUILayout.EndHorizontal ();
			
				if (script.cloud_folder1) {

					EditorGUILayout.HelpBox ("Parameters activated with the next volumetric cloud bed creation", MessageType.None);
					GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));


					EditorGUILayout.HelpBox ("Cloud bed width", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.VolCloudsHorScale = EditorGUILayout.Slider (script.SkyManager.VolCloudsHorScale, -2000, 2000, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud bed height", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.VolCloudHeight = EditorGUILayout.Slider (script.SkyManager.VolCloudHeight, 10, 2000, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud particles size", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.VCloudSizeFac = EditorGUILayout.Slider (script.SkyManager.VCloudSizeFac, 0.5f, 10, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud size", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.VCloudCSizeFac = EditorGUILayout.Slider (script.SkyManager.VCloudCSizeFac, 1f, 50, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.HelpBox ("Cloud centers multiplier", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.VCloudCoverFac = EditorGUILayout.Slider (script.SkyManager.VCloudCoverFac, 0.1f, 10, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));

					EditorGUILayout.BeginHorizontal ();
					script.OverridePeformance = EditorGUILayout.Toggle ("Override Performance", script.OverridePeformance, GUILayout.MaxWidth (380.0f));
					EditorGUILayout.EndHorizontal ();
					if (script.OverridePeformance) {

						EditorGUILayout.HelpBox ("Override default performance settings of current volume cloud bed. Use Smooth cloud motion for decoupling the cloud motion from the performance" +
							" settings.", MessageType.None);

						EditorGUILayout.BeginHorizontal ();
						script.DecoupleWind = EditorGUILayout.Toggle ("Smooth cloud motion", script.DecoupleWind, GUILayout.MaxWidth (380.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Update interval", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.UpdateInteraval = EditorGUILayout.Slider (script.UpdateInteraval, 0.01f, 0.5f, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Spread calcs to frames", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SpreadToFrames = EditorGUILayout.IntSlider (script.SpreadToFrames, 0, 15, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
					}

					GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));

					EditorGUILayout.BeginHorizontal ();
					script.RenewClouds = EditorGUILayout.Toggle ("Renew clouds", script.RenewClouds, GUILayout.MaxWidth (380.0f));
					EditorGUILayout.EndHorizontal ();
					if (script.RenewClouds) {
					
						EditorGUILayout.HelpBox ("Renew the volumetric cloud bed with an instance of the current clouds with reset in " +
							"position.", MessageType.None);
					
						EditorGUILayout.BeginHorizontal ();
						script.OverrideRenewSettings = EditorGUILayout.Toggle ("Override Fade-Boundary", script.OverrideRenewSettings, GUILayout.MaxWidth (380.0f));
						EditorGUILayout.EndHorizontal ();
						if (script.OverrideRenewSettings) {

							EditorGUILayout.HelpBox ("Boundary where clouds are renewed", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.Boundary = EditorGUILayout.Slider (script.Boundary, 500f, 150000f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();

							EditorGUILayout.HelpBox ("Fade in speed", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.FadeInSpeed = EditorGUILayout.Slider (script.FadeInSpeed, 0.1f, 0.9f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();

							EditorGUILayout.HelpBox ("Fade out speed", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.FadeOutSpeed = EditorGUILayout.Slider (script.FadeOutSpeed, 0.1f, 0.9f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
						
							EditorGUILayout.HelpBox ("Fade out time", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.MaxFadeTime = EditorGUILayout.Slider (script.MaxFadeTime, 0.5f, 2.5f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
						}
					}
				
					GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));

					//MODIFIERS - real time
					if (script.SkyManager.currentWeather != null && script.SkyManager.currentWeather.VolumeScript != null) {

						////

						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.DifferentialMotion = EditorGUILayout.Toggle ("Differential motion", script.SkyManager.currentWeather.VolumeScript.DifferentialMotion, GUILayout.MaxWidth (380.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Differential speed", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.MaxDiffSpeed = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.MaxDiffSpeed, -20, 20, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Differential factor", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.MaxDiffOffset = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.MaxDiffOffset, -2000, 2000, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						////

						EditorGUILayout.HelpBox ("Glow modifier", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.GlowShaderModifier = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.GlowShaderModifier, -20, 20, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Min light modifier", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.minLightShaderModifier = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.minLightShaderModifier, -20, 20, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Light intensity modifier", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.LightShaderModifier = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.LightShaderModifier, -20, 20, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Sun intensity modifier", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.IntensityShaderModifier = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.IntensityShaderModifier, -2, 2, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Modifiers speed", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.ModifierApplMinSpeed = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.ModifierApplMinSpeed, 0, 20, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Cloud dusk color", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.Dusk_sun_col = EditorGUILayout.ColorField (script.SkyManager.currentWeather.VolumeScript.Dusk_sun_col, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Cloud base dusk color", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.Dusk_base_col = EditorGUILayout.ColorField (script.SkyManager.currentWeather.VolumeScript.Dusk_base_col, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Cloud speed multiplier", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.currentWeather.VolumeScript.speed = EditorGUILayout.Slider (script.SkyManager.currentWeather.VolumeScript.speed, 0, 50, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

					}



					EditorGUILayout.BeginHorizontal ();
					//EditorGUILayout.LabelField("Setup Volumetric clouds (Realistic)",EditorStyles.boldLabel);	
					EditorGUILayout.HelpBox ("Setup Volumetric clouds (Realistic)", MessageType.None);
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					//if(GUILayout.Button(new GUIContent("Setup Volumetric clouds (Realistic)"),GUILayout.Width(220))){		//SET A	
					if (GUILayout.Button (VCLOUD_SETA_ICON, GUILayout.MaxWidth (380.0f), GUILayout.MaxHeight (130.0f))) {
					
						script.SkyManager.HeavyStormVolumeClouds = HeavyStormVOLUME_CLOUD2 as GameObject;
						script.SkyManager.DayClearVolumeClouds = DayClearVOLUME_CLOUD2 as GameObject;
						script.SkyManager.SnowVolumeClouds = SnowVOLUME_CLOUD2 as GameObject;
						script.SkyManager.RainStormVolumeClouds = RainStormVOLUME_CLOUD2 as GameObject;
						script.SkyManager.SnowStormVolumeClouds = SnowStormVOLUME_CLOUD2 as GameObject;
						script.SkyManager.RainVolumeClouds = RainVOLUME_CLOUD2 as GameObject;
						script.SkyManager.PinkVolumeClouds = PinkVOLUME_CLOUD2 as GameObject;
						script.SkyManager.DustyStormVolumeClouds = DustyStormVOLUME_CLOUD2 as GameObject;
						script.SkyManager.LightningVolumeClouds = LightningVOLUME_CLOUD2 as GameObject;
					}
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.HelpBox ("Setup Volumetric clouds (Fantasy)", MessageType.None);
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					//if(GUILayout.Button(new GUIContent("Setup Volumetric clouds (Fantasy)"),GUILayout.Width(220))){			//SET B
					if (GUILayout.Button (VCLOUD_SETB_ICON, GUILayout.MaxWidth (380.0f), GUILayout.MaxHeight (130.0f))) {
						script.SkyManager.HeavyStormVolumeClouds = HeavyStormVOLUME_CLOUD as GameObject;
						script.SkyManager.DayClearVolumeClouds = DayClearVOLUME_CLOUD as GameObject;
						script.SkyManager.SnowVolumeClouds = SnowVOLUME_CLOUD as GameObject;
						script.SkyManager.RainStormVolumeClouds = RainStormVOLUME_CLOUD as GameObject;
						script.SkyManager.SnowStormVolumeClouds = SnowStormVOLUME_CLOUD as GameObject;
						script.SkyManager.RainVolumeClouds = RainVOLUME_CLOUD as GameObject;
						script.SkyManager.PinkVolumeClouds = PinkVOLUME_CLOUD as GameObject;
						script.SkyManager.DustyStormVolumeClouds = DustyStormVOLUME_CLOUD as GameObject;
						script.SkyManager.LightningVolumeClouds = LightningVOLUME_CLOUD as GameObject;
					}
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.HelpBox ("Setup Volumetric clouds (Toon)", MessageType.None);
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					//if(GUILayout.Button(new GUIContent("Setup Volumetric clouds (Fantasy)"),GUILayout.Width(220))){			//SET B
					if (GUILayout.Button (VCLOUD_SETC_ICON, GUILayout.MaxWidth (380.0f), GUILayout.MaxHeight (130.0f))) {
						script.SkyManager.HeavyStormVolumeClouds = HeavyStormVOLUME_CLOUD3 as GameObject;
						script.SkyManager.DayClearVolumeClouds = DayClearVOLUME_CLOUD3 as GameObject;
						script.SkyManager.SnowVolumeClouds = SnowVOLUME_CLOUD3 as GameObject;
						script.SkyManager.RainStormVolumeClouds = RainStormVOLUME_CLOUD3 as GameObject;
						script.SkyManager.SnowStormVolumeClouds = SnowStormVOLUME_CLOUD3 as GameObject;
						script.SkyManager.RainVolumeClouds = RainVOLUME_CLOUD3 as GameObject;
						script.SkyManager.PinkVolumeClouds = PinkVOLUME_CLOUD3 as GameObject;
						script.SkyManager.DustyStormVolumeClouds = DustyStormVOLUME_CLOUD3 as GameObject;
						script.SkyManager.LightningVolumeClouds = LightningVOLUME_CLOUD3 as GameObject;
					}
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.HelpBox ("Setup Volumetric clouds (No scatter)", MessageType.None);
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.BeginHorizontal ();
					//if(GUILayout.Button(new GUIContent("Setup Volumetric clouds (Fantasy)"),GUILayout.Width(220))){			//SET B
					if (GUILayout.Button (VCLOUD_SETD_ICON, GUILayout.MaxWidth (380.0f), GUILayout.MaxHeight (130.0f))) {
						script.SkyManager.HeavyStormVolumeClouds = HeavyStormVOLUME_CLOUD4 as GameObject;
						script.SkyManager.DayClearVolumeClouds = DayClearVOLUME_CLOUD4 as GameObject;
						script.SkyManager.SnowVolumeClouds = SnowVOLUME_CLOUD4 as GameObject;
						script.SkyManager.RainStormVolumeClouds = RainStormVOLUME_CLOUD4 as GameObject;
						script.SkyManager.SnowStormVolumeClouds = SnowStormVOLUME_CLOUD4 as GameObject;
						script.SkyManager.RainVolumeClouds = RainVOLUME_CLOUD4 as GameObject;
						script.SkyManager.PinkVolumeClouds = PinkVOLUME_CLOUD4 as GameObject;
						script.SkyManager.DustyStormVolumeClouds = DustyStormVOLUME_CLOUD4 as GameObject;
						script.SkyManager.LightningVolumeClouds = LightningVOLUME_CLOUD4 as GameObject;
					}

					//UnityTerrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
					EditorGUILayout.EndHorizontal ();
				
				
					//EditorGUILayout.BeginHorizontal();
					//if(GUILayout.Button(new GUIContent("Scale Volumetric clouds"),GUILayout.Width(220))){			
//					if(mesh_terrain != null){
//						
//					}else{
//						Debug.Log ("Add a mesh terrain first");
//					}
//					
					//}
//				//mesh_terrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
//				
//				
					//EditorGUILayout.EndHorizontal();	

					EditorGUIUtility.wideMode = false;
				
					EditorGUILayout.BeginHorizontal ();
				
					//ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
					//ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
					EditorGUILayout.EndHorizontal ();
				
				
				
				
				}
				EditorGUILayout.EndVertical ();
				///////////////////////////////////////////////////////////////////////////









				/////////////////////////////////////////////////////////////////////////////// TERRAIN ///////////////////////////////////////
				EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));			
				//X_offset_left = 200;
				//Y_offset_top = 100;			
				//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));

				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	

				GUILayout.Label (MainIcon3, GUILayout.MaxWidth (410.0f));

				EditorGUILayout.LabelField ("Terrain", EditorStyles.boldLabel);			
				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.terrain_folder1 = EditorGUILayout.Foldout (script.terrain_folder1, "Terrain");
				EditorGUILayout.EndHorizontal ();
			
				if (script.terrain_folder1) {


					EditorGUILayout.PropertyField (UnityTerrains, true);
					EditorGUILayout.PropertyField (MeshTerrains, true);


					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Add sample Unity terrain"), GUILayout.Width (150))) {		
				
						GameObject SampleTerrainOBJ = (GameObject)Instantiate (SampleTerrain, script.SkyManager.MapCenter.position + new Vector3 (-100, 0, 0), Quaternion.identity);
						UnityTerrain = SampleTerrainOBJ;//assign to terrain
						script.SkyManager.Unity_terrain = SampleTerrainOBJ.transform;

						script.UnityTerrains.Add (SampleTerrainOBJ.GetComponent<Terrain> ());//add to Unitty terrains list
					}
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Setup Unity terrain(s)"), GUILayout.Width (150))) {

						if (UnityTerrain != null | script.SkyManager.Unity_terrain != null | script.UnityTerrains.Count > 0) {//if terrain defined in editor or in Sky manager, set it up
							if (script.TerrainManager == null) {

								//v3.0.1
//							if(script.SkyManager.Unity_terrain == null){
//								script.SkyManager.Unity_terrain = (UnityTerrain as GameObject).transform;
//							}

								//Add material to terrain
								//script.SkyManager.Unity_terrain.gameObject.GetComponent<Terrain>().materialType = Terrain.MaterialType.Custom;
								//script.SkyManager.Unity_terrain.gameObject.GetComponent<Terrain>().materialTemplate = UnityTerrainSnowMat;

								for (int i = 0; i<script.UnityTerrains.Count; i++) {
									script.UnityTerrains [i].materialType = Terrain.MaterialType.Custom;
									script.UnityTerrains [i].materialTemplate = UnityTerrainSnowMat;
								}
								script.SkyManager.Unity_terrain = script.UnityTerrains [0].transform;

								//Add terrain handler and configure (trees are configured in foliage section, volume fog in camera FX section)
								script.SkyManager.Unity_terrain.gameObject.AddComponent<SeasonalTerrainSKYMASTER> ();
								script.TerrainManager = script.SkyManager.Unity_terrain.gameObject.GetComponent<SeasonalTerrainSKYMASTER> ();
								script.TerrainManager.TerrainMat = UnityTerrainSnowMat; 

								script.SkyManager.SnowMat = MeshTerrainSnowMat;
								script.SkyManager.SnowMatTerrain = UnityTerrainSnowMat;

								script.TerrainManager.TreePefabs.Add (UnityTreePrefab as GameObject);
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient6.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient7.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient8.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient9.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient10.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient11.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient12.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient13.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient14.GetComponent<GlobalFogSkyMaster> ());

								script.TerrainManager.Mesh_moon = true;
								script.TerrainManager.Glow_moon = true;
								script.TerrainManager.Enable_trasition = true;
								script.TerrainManager.Fog_Sky_Update = true;
								script.TerrainManager.Foggy_Terrain = true;
								script.TerrainManager.Use_both_fogs = true;
								//script.TerrainManager.ImageEffectFog = true;
								//script.TerrainManager.ImageEffectShafts = true;
								script.TerrainManager.SkyManager = script.SkyManager;
							}
						} else {
							Debug.Log ("Add Unity terrain first");
						}

					}

					//UnityTerrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
					EditorGUILayout.EndHorizontal ();


					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Setup mesh terrain"), GUILayout.Width (150))) {			
						if (mesh_terrain != null | script.MeshTerrains.Count > 0) {
							if (script.TerrainManager == null) {

								//Add material to mesh terrain
								//(mesh_terrain as GameObject).GetComponent<Renderer>().material = MeshTerrainSnowMat; //v3.0.1

								for (int i = 0; i<script.MeshTerrains.Count; i++) {
									script.MeshTerrains [i].GetComponent<Renderer> ().material = MeshTerrainSnowMat;
								}

								script.SkyManager.Mesh_terrain = script.MeshTerrains [0].transform;

								//(mesh_terrain as GameObject).AddComponent<SeasonalTerrainSKYMASTER>();
								script.SkyManager.Mesh_terrain.gameObject.AddComponent<SeasonalTerrainSKYMASTER> ();
								script.TerrainManager = script.SkyManager.Mesh_terrain.gameObject.GetComponent<SeasonalTerrainSKYMASTER> ();
								script.TerrainManager.TerrainMat = MeshTerrainSnowMat; 

								script.SkyManager.SnowMat = MeshTerrainSnowMat;
								script.SkyManager.SnowMatTerrain = UnityTerrainSnowMat;

								script.TerrainManager.TreePefabs.Add (UnityTreePrefab as GameObject);
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient1_5.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient6.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient7.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient8.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient9.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient10.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient11.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient12.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient13.GetComponent<GlobalFogSkyMaster> ());
								script.TerrainManager.GradientHolders.Add (FogPresetGradient14.GetComponent<GlobalFogSkyMaster> ());
							
								script.TerrainManager.Mesh_moon = true;
								script.TerrainManager.Glow_moon = true;
								script.TerrainManager.Enable_trasition = true;
								script.TerrainManager.Fog_Sky_Update = true;
								script.TerrainManager.Foggy_Terrain = true;
								script.TerrainManager.Use_both_fogs = true;
								//script.TerrainManager.ImageEffectFog = true;
								//script.TerrainManager.ImageEffectShafts = true;
								script.TerrainManager.SkyManager = script.SkyManager;
								script.TerrainManager.Mesh_Terrain = true;
								//script.SkyManager.Mesh_terrain = (mesh_terrain as GameObject).transform;//v3.0.1
							}
						} else {
							Debug.Log ("Add a mesh terrain first and make sure a Unity terrain has not been setup already");
						}
					
					}
					//mesh_terrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
				
			
					EditorGUILayout.EndHorizontal ();				
					EditorGUIUtility.wideMode = false;
				
					EditorGUILayout.BeginHorizontal ();
				
					//ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
					//ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
					EditorGUILayout.EndHorizontal ();
				


				
				}
				EditorGUILayout.EndVertical ();
				///////////////////////////////////////////////////////////////////////////












				/////////////////////////////////////////////////////////////////////////////// CAMERA FX - VOLUMETRIC FOG - SUN SHAFTS /////////////////
				EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));			
				//X_offset_left = 200;
				//Y_offset_top = 100;			
				//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));
			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				GUILayout.Label (MainIcon4, GUILayout.MaxWidth (410.0f));
			
				EditorGUILayout.LabelField ("Camera FX", EditorStyles.boldLabel);			
				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.camera_folder1 = EditorGUILayout.Foldout (script.camera_folder1, "Volume fog, Sun shafts");
				EditorGUILayout.EndHorizontal ();
			
				if (script.camera_folder1) {	

					if (script.TerrainManager != null) {
						EditorGUILayout.HelpBox ("Sun Shaft size", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.TerrainManager.Shafts_intensity = EditorGUILayout.Slider (script.TerrainManager.Shafts_intensity, 0, 100, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Moon Shaft size", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.TerrainManager.Moon_Shafts_intensity = EditorGUILayout.Slider (script.TerrainManager.Moon_Shafts_intensity, 0, 100, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Sun Shaft length", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.TerrainManager.ShaftBlurRadiusOffset = EditorGUILayout.Slider (script.TerrainManager.ShaftBlurRadiusOffset, 0, 100, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
					}

					//splashes control
					if (script.SkyManager.RainDropsPlane != null) {
						EditorGUILayout.HelpBox ("Water splash amount", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.MaxWater = EditorGUILayout.Slider (script.SkyManager.MaxWater, 0, 15, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Water refraction", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.MaxRefract = EditorGUILayout.Slider (script.SkyManager.MaxRefract, 0, 15, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Water Freeze speed", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.FreezeSpeed = EditorGUILayout.Slider (script.SkyManager.FreezeSpeed, 0, 15, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();


						EditorGUILayout.HelpBox ("Enable water freeze", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.ScreenFreezeFX = EditorGUILayout.Toggle (script.SkyManager.ScreenFreezeFX, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Freeze inwards", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.SkyManager.FreezeInwards = EditorGUILayout.Toggle (script.SkyManager.FreezeInwards, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
					}

					//TONE MAP CONTROL
					if (Camera.main != null) {
						TonemappingSM ToneMapper = Camera.main.GetComponent<TonemappingSM> () as TonemappingSM;
						if (ToneMapper != null) {
							EditorGUILayout.HelpBox ("Tone mapper brightness", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							ToneMapper.exposureAdjustment = EditorGUILayout.Slider (ToneMapper.exposureAdjustment, 0.5f, 5, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
						}
					}

					//DUAL CAMERA SETUP
					EditorGUILayout.HelpBox ("VR Cameras - Left/Right - Define to add effects - Terrain Script is required", MessageType.None);
					if (script.TerrainManager != null) {
						script.TerrainManager.LeftCam = EditorGUILayout.ObjectField (script.TerrainManager.LeftCam, typeof(GameObject), true, GUILayout.MaxWidth (180.0f)) as GameObject;
						script.TerrainManager.RightCam = EditorGUILayout.ObjectField (script.TerrainManager.RightCam, typeof(GameObject), true, GUILayout.MaxWidth (180.0f)) as GameObject;
					}

					if (script.TerrainManager != null) {

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Add volumetric fog"), GUILayout.Width (150))) {			
							if (Camera.main != null && Camera.main.gameObject.GetComponent<GlobalFogSkyMaster> () == null) {
								Camera.main.gameObject.AddComponent<GlobalFogSkyMaster> ();
								Camera.main.gameObject.GetComponent<GlobalFogSkyMaster> ().SkyManager = script.SkyManager;
								Camera.main.gameObject.GetComponent<GlobalFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
								//Debug.Log("CAMERA FOUND");
								script.TerrainManager.Lerp_gradient = true;
								script.TerrainManager.ImageEffectFog = true;
								script.TerrainManager.FogHeightByTerrain = true;
							} else {
								if (Camera.main == null) {
									Debug.Log ("Add a main camera first");
								}
								if (Camera.main.gameObject.GetComponent<GlobalFogSkyMaster> () != null) {
									//setup existing
									Camera.main.gameObject.GetComponent<GlobalFogSkyMaster> ().SkyManager = script.SkyManager;
									Camera.main.gameObject.GetComponent<GlobalFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
									script.TerrainManager.Lerp_gradient = true;
									script.TerrainManager.ImageEffectFog = true;
									script.TerrainManager.FogHeightByTerrain = true;
								}
							}		

							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<GlobalFogSkyMaster> () == null) {
									script.TerrainManager.LeftCam.AddComponent<GlobalFogSkyMaster> ();
									script.TerrainManager.LeftCam.GetComponent<GlobalFogSkyMaster> ().SkyManager = script.SkyManager;
									script.TerrainManager.LeftCam.GetComponent<GlobalFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
									script.TerrainManager.Lerp_gradient = true;
									script.TerrainManager.ImageEffectFog = true;
									script.TerrainManager.FogHeightByTerrain = true;
								} else {
									if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<GlobalFogSkyMaster> () != null) {
										//setup existing
										script.TerrainManager.LeftCam.GetComponent<GlobalFogSkyMaster> ().SkyManager = script.SkyManager;
										script.TerrainManager.LeftCam.GetComponent<GlobalFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
										script.TerrainManager.Lerp_gradient = true;
										script.TerrainManager.ImageEffectFog = true;
										script.TerrainManager.FogHeightByTerrain = true;
									}
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<GlobalFogSkyMaster> () == null) {
									script.TerrainManager.RightCam.AddComponent<GlobalFogSkyMaster> ();
									script.TerrainManager.RightCam.GetComponent<GlobalFogSkyMaster> ().SkyManager = script.SkyManager;
									script.TerrainManager.RightCam.GetComponent<GlobalFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
									script.TerrainManager.Lerp_gradient = true;
									script.TerrainManager.ImageEffectFog = true;
									script.TerrainManager.FogHeightByTerrain = true;
								} else {
									if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<GlobalFogSkyMaster> () != null) {
										//setup existing
										script.TerrainManager.RightCam.GetComponent<GlobalFogSkyMaster> ().SkyManager = script.SkyManager;
										script.TerrainManager.RightCam.GetComponent<GlobalFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
										script.TerrainManager.Lerp_gradient = true;
										script.TerrainManager.ImageEffectFog = true;
										script.TerrainManager.FogHeightByTerrain = true;
									}
								}	
							}
						}
						if (GUILayout.Button (new GUIContent ("Add transparent v.fog"), GUILayout.Width (150))) {			
							if (Camera.main != null && Camera.main.gameObject.GetComponent<GlobalTranspFogSkyMaster> () == null) {
								Camera.main.gameObject.AddComponent<GlobalTranspFogSkyMaster> ();
								Camera.main.gameObject.GetComponent<GlobalTranspFogSkyMaster> ().SkyManager = script.SkyManager;
								Camera.main.gameObject.GetComponent<GlobalTranspFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
								//Debug.Log("CAMERA FOUND");
								script.TerrainManager.Lerp_gradient = true;
								script.TerrainManager.ImageEffectFog = true;
								script.TerrainManager.FogHeightByTerrain = true;
							} else {
								if (Camera.main == null) {
									Debug.Log ("Add a main camera first");
								}
								if (Camera.main.gameObject.GetComponent<GlobalTranspFogSkyMaster> () != null) {
									//setup existing
									Camera.main.gameObject.GetComponent<GlobalTranspFogSkyMaster> ().SkyManager = script.SkyManager;
									Camera.main.gameObject.GetComponent<GlobalTranspFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
									script.TerrainManager.Lerp_gradient = true;
									script.TerrainManager.ImageEffectFog = true;
									script.TerrainManager.FogHeightByTerrain = true;
								}
							}

							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<GlobalTranspFogSkyMaster> () == null) {
									script.TerrainManager.LeftCam.AddComponent<GlobalTranspFogSkyMaster> ();
									script.TerrainManager.LeftCam.GetComponent<GlobalTranspFogSkyMaster> ().SkyManager = script.SkyManager;
									script.TerrainManager.LeftCam.GetComponent<GlobalTranspFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
									script.TerrainManager.Lerp_gradient = true;
									script.TerrainManager.ImageEffectFog = true;
									script.TerrainManager.FogHeightByTerrain = true;
								} else {
									if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<GlobalTranspFogSkyMaster> () != null) {
										//setup existing
										script.TerrainManager.LeftCam.GetComponent<GlobalTranspFogSkyMaster> ().SkyManager = script.SkyManager;
										script.TerrainManager.LeftCam.GetComponent<GlobalTranspFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
										script.TerrainManager.Lerp_gradient = true;
										script.TerrainManager.ImageEffectFog = true;
										script.TerrainManager.FogHeightByTerrain = true;
									}
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<GlobalTranspFogSkyMaster> () == null) {
									script.TerrainManager.RightCam.AddComponent<GlobalTranspFogSkyMaster> ();
									script.TerrainManager.RightCam.GetComponent<GlobalTranspFogSkyMaster> ().SkyManager = script.SkyManager;
									script.TerrainManager.RightCam.GetComponent<GlobalTranspFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
									script.TerrainManager.Lerp_gradient = true;
									script.TerrainManager.ImageEffectFog = true;
									script.TerrainManager.FogHeightByTerrain = true;
								} else {
									if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<GlobalTranspFogSkyMaster> () != null) {
										//setup existing
										script.TerrainManager.RightCam.GetComponent<GlobalTranspFogSkyMaster> ().SkyManager = script.SkyManager;
										script.TerrainManager.RightCam.GetComponent<GlobalTranspFogSkyMaster> ().Sun = script.SkyManager.SUN_LIGHT.transform;
										script.TerrainManager.Lerp_gradient = true;
										script.TerrainManager.ImageEffectFog = true;
										script.TerrainManager.FogHeightByTerrain = true;
									}
								}	
							}
						}

						EditorGUILayout.EndVertical ();
						EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));

						if (GUILayout.Button (new GUIContent ("Add sun shafts"), GUILayout.Width (150))) {			
							if (Camera.main != null && Camera.main.gameObject.GetComponent<SunShaftsSkyMaster> () == null) {
								Camera.main.gameObject.AddComponent<SunShaftsSkyMaster> ();
								Camera.main.gameObject.GetComponent<SunShaftsSkyMaster> ().sunTransform = script.SkyManager.SunObj.transform;
								//Debug.Log("CAMERA FOUND");
								script.TerrainManager.ImageEffectShafts = true;
							} else {
								if (Camera.main == null) {
									Debug.Log ("Add a main camera first");
								}
								if (Camera.main.gameObject.GetComponent<SunShaftsSkyMaster> () != null) {
									Camera.main.gameObject.GetComponent<SunShaftsSkyMaster> ().sunTransform = script.SkyManager.SunObj.transform;
									script.TerrainManager.ImageEffectShafts = true;
								}
							}		
							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<SunShaftsSkyMaster> () == null) {
									script.TerrainManager.LeftCam.AddComponent<SunShaftsSkyMaster> ();
									script.TerrainManager.LeftCam.GetComponent<SunShaftsSkyMaster> ().sunTransform = script.SkyManager.SunObj.transform;
									script.TerrainManager.ImageEffectShafts = true;
								} else {
									if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<SunShaftsSkyMaster> () != null) {
										script.TerrainManager.LeftCam.GetComponent<SunShaftsSkyMaster> ().sunTransform = script.SkyManager.SunObj.transform;
										script.TerrainManager.ImageEffectShafts = true;
									}
								}	
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<SunShaftsSkyMaster> () == null) {
									script.TerrainManager.RightCam.AddComponent<SunShaftsSkyMaster> ();
									script.TerrainManager.RightCam.GetComponent<SunShaftsSkyMaster> ().sunTransform = script.SkyManager.SunObj.transform;
									script.TerrainManager.ImageEffectShafts = true;
								} else {
									if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<SunShaftsSkyMaster> () != null) {
										script.TerrainManager.RightCam.GetComponent<SunShaftsSkyMaster> ().sunTransform = script.SkyManager.SunObj.transform;
										script.TerrainManager.ImageEffectShafts = true;
									}
								}	
							}
						}
						if (GUILayout.Button (new GUIContent ("Add bloom"), GUILayout.Width (150))) {			
							if (Camera.main != null && Camera.main.gameObject.GetComponent<BloomSkyMaster> () == null) {
								Camera.main.gameObject.AddComponent<BloomSkyMaster> ();
							} else {
								Debug.Log ("Add a main camera first");
							}

							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<BloomSkyMaster> () == null) {
									script.TerrainManager.LeftCam.AddComponent<BloomSkyMaster> ();
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<BloomSkyMaster> () == null) {
									script.TerrainManager.RightCam.AddComponent<BloomSkyMaster> ();
								}
							}
						}
						if (GUILayout.Button (new GUIContent ("Add Underwater blur"), GUILayout.Width (150))) {			
							if (Camera.main != null && Camera.main.gameObject.GetComponent<UnderWaterImageEffect> () == null) {
								Camera.main.gameObject.AddComponent<UnderWaterImageEffect> ();
							} else {
								Debug.Log ("Add a main camera first");
							}

							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<UnderWaterImageEffect> () == null) {
									script.TerrainManager.LeftCam.AddComponent<UnderWaterImageEffect> ();
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<UnderWaterImageEffect> () == null) {
									script.TerrainManager.RightCam.AddComponent<UnderWaterImageEffect> ();
								}
							}
						}

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Add Water Drops"), GUILayout.Width (150))) {	
							script.SkyManager.RainDropsPlane = (Instantiate (RainDropsPlane) as GameObject).transform;
							script.SkyManager.RainDropsPlane.parent = Camera.main.transform;
							script.SkyManager.RainDropsPlane.localPosition = Vector3.zero;
							script.SkyManager.ScreenRainDrops = true;
							script.SkyManager.ScreenRainDropsMat = ScreenRainDropsMat;

							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null) {
									GameObject DropPlainL = Instantiate (RainDropsPlane) as GameObject;
									DropPlainL.transform.parent = script.TerrainManager.LeftCam.transform;
									DropPlainL.transform.localPosition = Vector3.zero;
								}
								if (script.TerrainManager.RightCam != null) {
									GameObject DropPlainL = Instantiate (RainDropsPlane) as GameObject;
									DropPlainL.transform.parent = script.TerrainManager.RightCam.transform;
									DropPlainL.transform.localPosition = Vector3.zero;
								}
							}
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Add Aberrarion"), GUILayout.Width (150))) {	
							if (Camera.main != null && Camera.main.gameObject.GetComponent<VignetteAndChromaticAberrationSM> () == null) {
								Camera.main.gameObject.AddComponent<VignetteAndChromaticAberrationSM> ();
							} else {
								Debug.Log ("Add a main camera first");
							}	
							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<VignetteAndChromaticAberrationSM> () == null) {
									script.TerrainManager.LeftCam.AddComponent<VignetteAndChromaticAberrationSM> ();
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<VignetteAndChromaticAberrationSM> () == null) {
									script.TerrainManager.RightCam.AddComponent<VignetteAndChromaticAberrationSM> ();
								}
							}
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Add Tone Mapping"), GUILayout.Width (150))) {	
							if (Camera.main != null && Camera.main.gameObject.GetComponent<TonemappingSM> () == null) {
								Camera.main.gameObject.AddComponent<TonemappingSM> ();
								Camera.main.gameObject.GetComponent<TonemappingSM> ().exposureAdjustment = 2.2f;
							} else {
								Debug.Log ("Add a main camera first");
							}		
							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<TonemappingSM> () == null) {
									script.TerrainManager.LeftCam.AddComponent<TonemappingSM> ();
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<TonemappingSM> () == null) {
									script.TerrainManager.RightCam.AddComponent<TonemappingSM> ();
								}
							}
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Add Mouse Look"), GUILayout.Width (150))) {	
							if (Camera.main != null && Camera.main.gameObject.GetComponent<MouseLookSKYMASTER> () == null) {
								Camera.main.gameObject.AddComponent<MouseLookSKYMASTER> ();
							} else {
								Debug.Log ("Add a main camera first");
							}
							//LEFT-RIGHT VR CAMERAS
							if (script.TerrainManager != null) {
								if (script.TerrainManager.LeftCam != null && script.TerrainManager.LeftCam.GetComponent<MouseLookSKYMASTER> () == null) {
									script.TerrainManager.LeftCam.AddComponent<MouseLookSKYMASTER> ();
								}
								if (script.TerrainManager.RightCam != null && script.TerrainManager.RightCam.GetComponent<MouseLookSKYMASTER> () == null) {
									script.TerrainManager.RightCam.AddComponent<MouseLookSKYMASTER> ();
								}
							}
						}
						EditorGUILayout.EndHorizontal ();


//				if(GUILayout.Button(new GUIContent("Add Depth of Field"),GUILayout.Width(120))){			
//					if(Camera.main != null && Camera.main.gameObject.GetComponent<>() == null){
//						Camera.main.gameObject.AddComponent<SunShaftsSkyMaster>();
//					}else{
//						Debug.Log ("Add a main camera first");
//					}					
//				}
				
				
						EditorGUILayout.EndHorizontal ();				
						EditorGUIUtility.wideMode = false;
				
						EditorGUILayout.BeginHorizontal ();
				
						//ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
						//ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
						EditorGUILayout.EndHorizontal ();
				
				
				
					} else {

						EditorGUILayout.HelpBox ("Please configure a terrain first, before adding fog, sun shafts and other filters to the Main Camera and VR Cameras", MessageType.Warning);

						//Debug.Log("Please configure a terrain first, before adding fog, sun shafts and other filters to the Main Camera and VR Cameras");
					}
				}
				EditorGUILayout.EndVertical ();
				///////////////////////////////////////////////////////////////////////////







				/////////////////////////////////////////////////////////////////////////////// WEATHER - EVENTS
				EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));			
				//X_offset_left = 200;
				//Y_offset_top = 100;			
				//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));
			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				GUILayout.Label (MainIcon5, GUILayout.MaxWidth (410.0f));
			
				EditorGUILayout.LabelField ("Weather", EditorStyles.boldLabel);			
				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.weather_folder1 = EditorGUILayout.Foldout (script.weather_folder1, "Weather events");
				EditorGUILayout.EndHorizontal ();
			
				if (script.weather_folder1) {

					//choose weather
					GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
					EditorGUILayout.HelpBox ("Current weather", MessageType.None);
					if (script.SkyManager != null) {
						script.SkyManager.currentWeatherName = (Artngame.SKYMASTER.SkyMasterManager.Volume_Weather_types)EditorGUILayout.EnumPopup (script.SkyManager.currentWeatherName, GUILayout.Width (120));
					}

					GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
					EditorGUILayout.HelpBox ("Snow coverage", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.MaxSnowCoverage = EditorGUILayout.Slider (script.SkyManager.MaxSnowCoverage, 1f, 8, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();
					EditorGUILayout.HelpBox ("Snow coverage speed (terrain)", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.SnowTerrRateFactor = EditorGUILayout.Slider (script.SkyManager.SnowTerrRateFactor, 1f, 1000, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
					EditorGUILayout.HelpBox ("Enable Unity fog (Editor use)", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.Use_fog = EditorGUILayout.Toggle (script.SkyManager.Use_fog, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					if (script.TerrainManager != null) {
						GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
						EditorGUILayout.HelpBox ("Use Transparent Volume Fog (Volume Cloud interaction)", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.TerrainManager.UseTranspVFog = EditorGUILayout.Toggle (script.TerrainManager.UseTranspVFog, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
					}

					//SELECT UNITY FOG DENSITY
					GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
					EditorGUILayout.HelpBox ("Unity fog density", MessageType.None);
					EditorGUILayout.BeginHorizontal ();
					script.SkyManager.Fog_Density_Mult = EditorGUILayout.Slider (script.SkyManager.Fog_Density_Mult, 0.1f, 100, GUILayout.MaxWidth (195.0f));
					EditorGUILayout.EndHorizontal ();

					if (script.TerrainManager != null) {
						// OFFSET VOLUME FOG DENSITY and HEIGHT
						GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
						EditorGUILayout.HelpBox ("Volume fog density", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.TerrainManager.AddFogDensityOffset = EditorGUILayout.Slider (script.TerrainManager.AddFogDensityOffset, -2, 10, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						GUILayout.Box ("", GUILayout.Height (2), GUILayout.Width (410));	
						EditorGUILayout.HelpBox ("Volume fog height", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.TerrainManager.AddFogHeightOffset = EditorGUILayout.Slider (script.TerrainManager.AddFogHeightOffset, -200, 200, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
					}



					//RANDOM WEATHER EVENTS ADDTION !!! Also decide whether there will be automatic seasonal changes
					GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	

					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Add event"), GUILayout.Width (120))) {			
//					if(UnityTerrain != null){
//						
//					}else{
//						Debug.Log ("Add Unity terrain first");
//					}
						WeatherEventSM WeatherEvent = new WeatherEventSM ();
						WeatherEvent.Chance = WeatherEvent_Chance;
						WeatherEvent.EventStartHour = WeatherEvent_StartHour;
						WeatherEvent.EventStartDay = WeatherEvent_StartDay;
						WeatherEvent.EventStartMonth = WeatherEvent_StartMonth;
						WeatherEvent.EventEndHour = WeatherEvent_EndHour;
						WeatherEvent.EventEndDay = WeatherEvent_EndDay;
						WeatherEvent.EventEndMonth = WeatherEvent_EndMonth;
						WeatherEvent.SkyManager = script.SkyManager;
						WeatherEvent.VolCloudHeight = WeatherEvent_VolCloudHeight;
						WeatherEvent.VolCloudsHorScale = WeatherEvent_VolCloudsHorScale;
						//WeatherEvent.VolumeCloudsPREFAB = WeatherEvent_VolumeCloudsPREFAB;
						WeatherEvent.Weather_type = WeatherEvent_Weather_type;//WeatherEventSM.Volume_Weather_event_types.Cloudy;

						//WeatherEvent.FollowUpWeather = FollowUpWeatherEvent_Weather_type;

						script.SkyManager.WeatherEvents.Add (WeatherEvent);
					}
				

					EditorGUILayout.EndHorizontal ();


					EditorGUILayout.BeginHorizontal ();
				
					//EditorGUILayout.LabelField("Chance:"+script.SkyManager.WeatherEvents[i].Chance,EditorStyles.boldLabel,GUILayout.MaxWidth(125.0f));
					//					EditorGUILayout.HelpBox("Chance",MessageType.None);
					//					EditorGUILayout.HelpBox("Time span",MessageType.None);
					//					EditorGUILayout.HelpBox("Day span",MessageType.None);
					//					EditorGUILayout.HelpBox("Month span",MessageType.None);
					//					EditorGUILayout.HelpBox("Weather type",MessageType.None);
					EditorGUILayout.TextField ("Chance", GUILayout.MaxWidth (75.0f));
					EditorGUILayout.TextField ("Time span", GUILayout.MaxWidth (75.0f));
					EditorGUILayout.TextField ("Day span", GUILayout.MaxWidth (75.0f));
					EditorGUILayout.TextField ("Month span", GUILayout.MaxWidth (75.0f));
					EditorGUILayout.TextField ("Weather type", GUILayout.MaxWidth (75.0f));
					EditorGUILayout.EndHorizontal ();
					//EditorGUILayout.BeginVertical();
					for (int i=0; i<script.SkyManager.WeatherEvents.Count; i++) {




						//REMOVE EVENT BUTTON
						EditorGUILayout.BeginHorizontal ();
						
						//EditorGUILayout.LabelField("Chance:"+script.SkyManager.WeatherEvents[i].Chance,EditorStyles.boldLabel,GUILayout.MaxWidth(125.0f));
						EditorGUILayout.TextField (script.SkyManager.WeatherEvents [i].Chance.ToString (), GUILayout.MaxWidth (95.0f));
						EditorGUILayout.TextField (script.SkyManager.WeatherEvents [i].EventStartHour + "-" + script.SkyManager.WeatherEvents [i].EventEndHour, GUILayout.MaxWidth (95.0f));
						EditorGUILayout.TextField (script.SkyManager.WeatherEvents [i].EventStartDay + "-" + script.SkyManager.WeatherEvents [i].EventEndDay, GUILayout.MaxWidth (95.0f));
						EditorGUILayout.TextField (script.SkyManager.WeatherEvents [i].EventStartMonth + "-" + script.SkyManager.WeatherEvents [i].EventEndMonth, GUILayout.MaxWidth (95.0f));
						EditorGUILayout.TextField (script.SkyManager.WeatherEvents [i].Weather_type.ToString (), GUILayout.MaxWidth (95.0f));
//					EditorGUILayout.HelpBox("Chance:"+script.SkyManager.WeatherEvents[i].Chance,MessageType.None);
//					EditorGUILayout.HelpBox("Time span:"+script.SkyManager.WeatherEvents[i].EventStartHour+"-"+script.SkyManager.WeatherEvents[i].EventEndHour,MessageType.None);
//					EditorGUILayout.HelpBox("Day span:"+script.SkyManager.WeatherEvents[i].EventStartDay+"-"+script.SkyManager.WeatherEvents[i].EventEndDay,MessageType.None);
//					EditorGUILayout.HelpBox("Month span:"+script.SkyManager.WeatherEvents[i].EventStartMonth+"-"+script.SkyManager.WeatherEvents[i].EventEndMonth,MessageType.None);
//					EditorGUILayout.HelpBox("Weather type:"+script.SkyManager.WeatherEvents[i].Weather_type,MessageType.None);
						if (GUILayout.Button (new GUIContent ("-"), GUILayout.Width (20))) {
							//if(GUILayout.Button(new GUIContent("Remove event"),GUILayout.Width(120))){
							script.SkyManager.WeatherEvents.RemoveAt (i);
						}
						EditorGUILayout.EndHorizontal ();
					}
					//EditorGUILayout.EndVertical();


					EditorGUIUtility.wideMode = false;
				
					EditorGUILayout.BeginHorizontal ();				
					//ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
					//ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Weather type:", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_Weather_type = (WeatherEventSM.Volume_Weather_event_types)EditorGUILayout.EnumPopup (WeatherEvent_Weather_type);
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Weather chance:", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_Chance = EditorGUILayout.FloatField (WeatherEvent_Chance, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Start hour (1-24):", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_StartHour = EditorGUILayout.FloatField (WeatherEvent_StartHour, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Start day (1-30):", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_StartDay = EditorGUILayout.IntField (WeatherEvent_StartDay, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Start month (1-4):", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_StartMonth = EditorGUILayout.IntField (WeatherEvent_StartMonth, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("End hour (1-24):", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_EndHour = EditorGUILayout.FloatField (WeatherEvent_EndHour, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("End day (1-30):", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_EndDay = EditorGUILayout.IntField (WeatherEvent_EndDay, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("End month (1-4):", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_EndMonth = EditorGUILayout.IntField (WeatherEvent_EndMonth, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Volume cloud height:", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_VolCloudHeight = EditorGUILayout.FloatField (WeatherEvent_VolCloudHeight, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					EditorGUILayout.LabelField ("Volume cloud span:", EditorStyles.boldLabel, GUILayout.MaxWidth (125.0f));
					WeatherEvent_VolCloudsHorScale = EditorGUILayout.FloatField (WeatherEvent_VolCloudsHorScale, GUILayout.MaxWidth (95.0f));
					EditorGUILayout.EndHorizontal ();
				}
				EditorGUILayout.EndVertical ();
				///////////////////////////////////////////////////////////////////////////








				/////////////////////////////////////////////////////////////////////////////// FOLIAGE
				EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));			
				//X_offset_left = 200;
				//Y_offset_top = 100;			
				//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));
			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				GUILayout.Label (MainIcon6, GUILayout.MaxWidth (410.0f));
			
				EditorGUILayout.LabelField ("Foliage", EditorStyles.boldLabel);			
				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.foliage_folder1 = EditorGUILayout.Foldout (script.foliage_folder1, "Foliage");
				EditorGUILayout.EndHorizontal ();
			
				if (script.foliage_folder1) {
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Setup snow on mesh"), GUILayout.Width (150))) {			
						//add material to mesh
						(snowMESH as GameObject).GetComponent<MeshRenderer> ().material = MeshTerrainSnowMat;
					}
					snowMESH = EditorGUILayout.ObjectField (snowMESH, typeof(GameObject), true, GUILayout.MaxWidth (150.0f)) as GameObject;
					//UnityTerrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
					EditorGUILayout.EndHorizontal ();
				
					EditorGUILayout.HelpBox ("For Unity terrain trees & grass, please extract the 'Unity_terrain_foliage_SM3_overrides.unitypackage' file located in the V3.0 assets, foliage folder", MessageType.Warning);
					
					EditorGUILayout.HelpBox ("For SpeedTree trees, please extract the 'SpeedTree_SM3.unitypackage' file that can be downloaded from the Unity Forum thread.", MessageType.Warning);
					if (GUILayout.Button ("Download SpeedTree snow shaders", GUILayout.MaxWidth (405.0f), GUILayout.MaxHeight (210.0f))) {
						Application.OpenURL ("http://forum.unity3d.com/threads/discount-until-v3-sky-master-2-the-one-draw-call-3d-volume-cloud-fog-physically-based-rendering.280612/");
					}

					EditorGUILayout.HelpBox ("Sky Master gradual snow growth can be directly used with InfiniGRASS asset grass & foliage and is activated in snow conditions." +
						"The water module is compatible with the InfiniGRASS shaders. For more information on InfiniGRASS, press below:", MessageType.Warning);
					if (GUILayout.Button (InfiniGRASS_ICON, GUILayout.MaxWidth (405.0f), GUILayout.MaxHeight (210.0f))) {
						Application.OpenURL ("http://u3d.as/jiM");
					}


					EditorGUIUtility.wideMode = false;
				
					EditorGUILayout.BeginHorizontal ();
				
					//ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
					//ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
					EditorGUILayout.EndHorizontal ();
				
				
				
				
				}
				EditorGUILayout.EndVertical ();
				///////////////////////////////////////////////////////////////////////////









				/////////////////////////////////////////////////////////////////////////////// WATER
				EditorGUILayout.BeginVertical (GUILayout.MaxWidth (180.0f));			
				//X_offset_left = 200;
				//Y_offset_top = 100;			
				//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));
			
				GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
			
				GUILayout.Label (MainIcon7, GUILayout.MaxWidth (410.0f));
			
				EditorGUILayout.LabelField ("Water", EditorStyles.boldLabel);			
				EditorGUILayout.BeginHorizontal (GUILayout.Width (200));
				script.water_folder1 = EditorGUILayout.Foldout (script.water_folder1, "Water");
				EditorGUILayout.EndHorizontal ();
			
				if (script.water_folder1) {

					if (!script.LargeWaterPlane) {
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Tiles X", GUILayout.Width (50));
						Water_tiles.x = EditorGUILayout.FloatField (Water_tiles.x, GUILayout.Width (50));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Tiles Y", GUILayout.Width (50));
						Water_tiles.y = EditorGUILayout.FloatField (Water_tiles.y, GUILayout.Width (50));
						EditorGUILayout.EndHorizontal ();
					} else {
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Label ("Plane scale", GUILayout.Width (120));
						script.LargeWaterPlaneScale = EditorGUILayout.FloatField (script.LargeWaterPlaneScale, GUILayout.MaxWidth (95.0f));
						EditorGUILayout.EndHorizontal ();
					}

					//if(script.WaterManager != null){
					EditorGUILayout.BeginHorizontal ();
					script.LargeWaterPlane = EditorGUILayout.Toggle ("Radial large plane", script.LargeWaterPlane, GUILayout.MaxWidth (280.0f));
					EditorGUILayout.EndHorizontal ();
					//}

					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button (new GUIContent ("Add Water"), GUILayout.Width (120))) {			
						//add water in start point, add tiles from prefab and pass water size
						//control fog based on collider or position

						if (Camera.main != null && Camera.main.gameObject.GetComponent<UnderWaterImageEffect> () == null) {
							Camera.main.gameObject.AddComponent<UnderWaterImageEffect> ();
						} else {
							Debug.Log ("Add a main camera and then re-add water for Underwater blur image effect");
						}

						//pass SeasonalTerrain script to WaterHandler to change fog if underwater
						Vector3 MapcenterPos = script.SkyManager.MapCenter.position;

						GameObject Water = null;

						if (script.SkyManager.water != null) {
							Water = script.SkyManager.water.gameObject;
						}

						float Height = MapcenterPos.y;
						if (script.TerrainManager != null) {
							Height = script.TerrainManager.gameObject.transform.position.y;
						} else
					if (Terrain.activeTerrain != null) {
							Height = Terrain.activeTerrain.gameObject.transform.position.y;
						} else if (script.SkyManager.Mesh_terrain != null) {
							Height = script.SkyManager.Mesh_terrain.position.y;
						}
					
						//float tiles_count = Water_tiles.x * Water_tiles.y;
						//Vector3 Tilepos = Vector3.zero;
						//for (int i=0;i<tiles_count;i++){
						Vector3 Start_pos = MapcenterPos - new Vector3 ((Water_tiles.x * tileSize) / 2, Height + 3, (Water_tiles.y * tileSize) / 2);

						if (Water == null) {
							Water = (GameObject)Instantiate (WaterPREFAB, MapcenterPos + new Vector3 (0, 3, 0), Quaternion.identity);
							Water.AddComponent<WaterHandlerSM> ().Water_start = MapcenterPos;
							Water.GetComponent<WaterHandlerSM> ().Water_size = Water_tiles;
							Water.GetComponent<WaterHandlerSM> ().TerrainManager = script.TerrainManager;
							Water.GetComponent<WaterHandlerSM> ().SkyManager = script.SkyManager;
							Water.GetComponent<WaterHandlerSM> ().oceanMat = WaterMat;
							Water.GetComponent<WaterHandlerSM> ().SpecularSource = Water.GetComponent<SpecularLightingSM> ();
							Water.GetComponent<WaterHandlerSM> ().WaterBase = Water.GetComponent<WaterBaseSM> ();

							Water.GetComponent<WaterHandlerSM> ().SeaAudio = Water.GetComponent<AudioSource> () as AudioSource;

							Water.GetComponent<SpecularLightingSM> ().specularLight = script.SkyManager.SunObj.transform;




							Water.transform.localScale = WaterScale;

							//add caustics
							GameObject WaterCaustics = (GameObject)Instantiate (CausticsPREFAB, MapcenterPos + new Vector3 (0, 3, 0), Quaternion.identity);
							//pass projector and material to water controller

							//add to skymanager
							script.SkyManager.water = (Water as GameObject).transform;
							script.WaterManager = Water.GetComponent<WaterHandlerSM> ();

							//pass caustic properties
							script.WaterManager.CausticsProjector = WaterCaustics.GetComponentsInChildren<Projector> () [0];
							script.WaterManager.CausticsMat = script.WaterManager.CausticsProjector.material;
						}

						//add central - WaterTileLargePREFAB
						if (script.LargeWaterPlane) {
						
							Vector3 Start_pos1 = Vector3.zero;
						
							GameObject WaterTile1 = (GameObject)Instantiate (WaterTileLargePREFAB);
							Vector3 StartScale = WaterTile1.transform.localScale;
							WaterTile1.transform.parent = Water.transform;
							WaterTile1.transform.position = new Vector3 (Start_pos.x, Height + 3, Start_pos.z);
							WaterTile1.transform.localScale = new Vector3 (StartScale.x * script.LargeWaterPlaneScale, StartScale.y, StartScale.z * script.LargeWaterPlaneScale);
							WaterTile1.transform.localPosition = Start_pos1;
						
							WaterTile1 = (GameObject)Instantiate (WaterTileLargePREFAB);
							WaterTile1.transform.parent = Water.transform;
							WaterTile1.transform.position = new Vector3 (Start_pos.x, Height + 3, Start_pos.z);
							WaterTile1.transform.localScale = new Vector3 (-StartScale.x * script.LargeWaterPlaneScale, StartScale.y, StartScale.z * script.LargeWaterPlaneScale);
							WaterTile1.transform.localPosition = Start_pos1 + new Vector3 (0.002f, 0, 0);
						
							WaterTile1 = (GameObject)Instantiate (WaterTileLargePREFAB);
							WaterTile1.transform.parent = Water.transform;
							WaterTile1.transform.position = new Vector3 (Start_pos.x, Height + 3, Start_pos.z);
							WaterTile1.transform.localScale = new Vector3 (StartScale.x * script.LargeWaterPlaneScale, StartScale.y, -StartScale.z * script.LargeWaterPlaneScale);
							WaterTile1.transform.localPosition = Start_pos1 + new Vector3 (0, 0, 0.002f);
						
							WaterTile1 = (GameObject)Instantiate (WaterTileLargePREFAB);
							WaterTile1.transform.parent = Water.transform;
							WaterTile1.transform.position = new Vector3 (Start_pos.x, Height + 3, Start_pos.z);
							WaterTile1.transform.localScale = new Vector3 (-StartScale.x * script.LargeWaterPlaneScale, StartScale.y, -StartScale.z * script.LargeWaterPlaneScale);
							WaterTile1.transform.localPosition = Start_pos1 + new Vector3 (0.002f, 0, 0.002f);
						
						} else {
							//add tiles
							for (int i=0; i<Water_tiles.x; i++) {
								for (int j=0; j<Water_tiles.y; j++) {
									GameObject WaterTile = (GameObject)Instantiate (WaterTilePREFAB);
									WaterTile.transform.parent = Water.transform;
									WaterTile.transform.position = new Vector3 (Start_pos.x + (i * tileSize) + tileSize / 2, Height + 3, Start_pos.z + (j * tileSize) + tileSize / 2);
								}
							}
						}


					}
					EditorGUILayout.EndHorizontal ();

					if (script.SkyManager.water != null) {

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Remove water colliders"), GUILayout.Width (200))) {
							script.WaterManager.DisableColliders ();
						}
						if (GUILayout.Button (new GUIContent ("Restore water colliders"), GUILayout.Width (200))) {
							script.WaterManager.EnableColliders ();
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (new GUIContent ("Add Height Sampler"), GUILayout.Width (150))) {
							Vector3 MapcenterPos = script.SkyManager.MapCenter.position;
							GameObject WaterSampler = (GameObject)Instantiate (WaterSamplerPREFAB, MapcenterPos + new Vector3 (0, 3, 20), Quaternion.identity); 
							script.SkyManager.water.gameObject.AddComponent<WaterHeightSM> ().SampleCube = WaterSampler.transform;
							script.SkyManager.water.gameObject.GetComponent<WaterHeightSM> ().Waterhandler = script.WaterManager;
							script.SkyManager.water.gameObject.GetComponent<WaterHeightSM> ().WaterMaterial = script.WaterManager.oceanMat;
							script.SkyManager.water.gameObject.GetComponent<WaterHeightSM> ().LerpMotion = true;
						}
						EditorGUILayout.EndHorizontal ();

						//parent camera
						//WaterHeightSM WaterHeightHandle = script.SkyManager.water.gameObject.GetComponent<WaterHeightSM>();
						if (script.WaterHeightHandle == null) {
							script.WaterHeightHandle = script.SkyManager.water.gameObject.GetComponent<WaterHeightSM> ();
						}

						if (script.WaterHeightHandle != null && script.WaterHeightHandle.SampleCube != null) {
							EditorGUILayout.BeginHorizontal ();
							if (GUILayout.Button (new GUIContent ("Board boat"), GUILayout.Width (120))) {
								if (Camera.main != null) {
									Camera.main.transform.parent = script.WaterHeightHandle.SampleCube;
									Camera.main.transform.localPosition = new Vector3 (-0.5f, 1.65f, -2.4f);
									script.WaterHeightHandle.controlBoat = true;
									script.WaterHeightHandle.LerpMotion = true;
								}
							}


							if (GUILayout.Button (new GUIContent ("Enable thrower"), GUILayout.Width (120))) {
								script.WaterHeightHandle.ThrowItem = script.WaterHeightHandle.SampleCube.Find ("Sphere").gameObject;
							}
							EditorGUILayout.EndHorizontal ();


						}

						//EditorGUILayout.EndHorizontal(); 
						if (script.WaterHeightHandle != null && script.WaterHeightHandle.SampleCube != null) {
							EditorGUILayout.BeginHorizontal ();
							script.WaterHeightHandle.followCamera = EditorGUILayout.Toggle ("Water follows player", script.WaterHeightHandle.followCamera, GUILayout.MaxWidth (280.0f));
							EditorGUILayout.EndHorizontal ();
						}

						//SHORE LINE
						GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));
						EditorGUILayout.HelpBox ("Shore line configuration", MessageType.None);

						// Check "TerrainSM" layer exists 
						if (LayerMask.NameToLayer ("TerrainSM") == -1) {
							//Debug.Log(LayerMask.NameToLayer("TerrainSM"));
							EditorGUILayout.HelpBox ("Please add the layer 'TerrainSM' to the layer list & to all Terrains that will be depth tested for use in shore line effect", MessageType.None);
						} else {

							if (MiddleTerrain != null) {
								if ((MiddleTerrain as GameObject).layer == LayerMask.NameToLayer ("TerrainSM")) {

									EditorGUILayout.BeginHorizontal ();
									GUILayout.Label ("Terrain size", GUILayout.Width (120));
									script.TerrainDepthSize = EditorGUILayout.FloatField (script.TerrainDepthSize, GUILayout.MaxWidth (95.0f));
									EditorGUILayout.EndHorizontal ();

									EditorGUILayout.BeginHorizontal ();
									if (GUILayout.Button (new GUIContent ("Auto calculate middle terrain size (single terrain)"), GUILayout.Width (320))) {
										Terrain TempTerrain = (MiddleTerrain as GameObject).GetComponent<Terrain> ();
										if (TempTerrain != null) {
											script.TerrainDepthSize = Mathf.Max (TempTerrain.terrainData.size.x, TempTerrain.terrainData.size.z);
										}
									}

									EditorGUILayout.EndHorizontal ();
									EditorGUILayout.BeginHorizontal ();
									if (GUILayout.Button (new GUIContent ("Add Shore Line effect"), GUILayout.Width (220))) {
										if (script.TerrainDepthSize > 0 && script.WaterManager != null) {
											Debug.Log ("Adding terrain depth render camera & script");
											float HalfSize = script.TerrainDepthSize / 2;
											//GameObject DepthSampler = (GameObject)Instantiate(DepthCameraPREFAB, (MiddleTerrain as GameObject).transform.position + new Vector3(HalfSize,HalfSize/0.57735026918f,HalfSize),Quaternion.identity); 
											GameObject DepthSampler = (GameObject)Instantiate (DepthCameraPREFAB); 
											DepthSampler.transform.position = (MiddleTerrain as GameObject).transform.position + new Vector3 (HalfSize, HalfSize / 0.57735026918f, HalfSize);
											Debug.Log ("Adding depth camera to WaterHandler");
											script.WaterManager.TerrainDepthCamera = DepthSampler.GetComponent<Camera> ().transform;
											//DepthSampler.GetComponent<Camera>().cullingMask = LayerMask.NameToLayer("TerrainSM");
											DepthSampler.GetComponent<Camera> ().cullingMask = (1 << LayerMask.NameToLayer ("TerrainSM"));

											DepthSampler.GetComponent<Camera> ().farClipPlane = DepthSampler.GetComponent<Camera> ().transform.position.y * 2;

											script.WaterManager.DepthRenderController = DepthSampler.GetComponent<TerrainDepthSM> ();
											script.WaterManager.TerrainScale = script.TerrainDepthSize;
										} else {
											Debug.Log ("Please define the size of the terrain(s) to be depth tested");
										}
									}
									EditorGUILayout.EndHorizontal ();

								} else {
									EditorGUILayout.BeginHorizontal ();
									EditorGUILayout.HelpBox ("Please add the tag 'TerrainSM' to all Terrains that will be depth tested for use in shore line effect", MessageType.None);
									EditorGUILayout.EndHorizontal ();
									//EditorGUILayout.EndHorizontal();
									//GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));

									EditorGUILayout.BeginHorizontal ();
									if (GUILayout.Button (new GUIContent ("Auto assign layer (single terrain)"), GUILayout.Width (320))) {
										(MiddleTerrain as GameObject).layer = LayerMask.NameToLayer ("TerrainSM");

										//v3.0.4a - fix sun light to include TerrainSM layer
										script.SkyManager.SUN_LIGHT.GetComponent<Light> ().cullingMask = ~(0);
									}
									EditorGUILayout.EndHorizontal ();
								}
							} else {
								EditorGUILayout.BeginHorizontal ();
								EditorGUILayout.HelpBox ("Please add the middle Terrain to apply the shore line to", MessageType.None);
								EditorGUILayout.EndHorizontal ();
							}

							EditorGUILayout.BeginHorizontal ();
							MiddleTerrain = EditorGUILayout.ObjectField (MiddleTerrain, typeof(GameObject), true, GUILayout.MaxWidth (150.0f)) as GameObject;
							EditorGUILayout.EndHorizontal ();
						}

						if (script.WaterManager != null && script.WaterManager.TerrainDepthCamera != null) {
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.followCamera = EditorGUILayout.Toggle ("Depth camera follows player", script.WaterManager.followCamera, GUILayout.MaxWidth (280.0f));
							EditorGUILayout.EndHorizontal ();
						}


						if (script.WaterManager != null && script.WaterManager.DepthRenderController != null) {
							//add depth render controls

							EditorGUILayout.HelpBox ("Shore waves fade", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.ShoreWavesFade = EditorGUILayout.Slider (script.WaterManager.ShoreWavesFade, 0, 20f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
							EditorGUILayout.HelpBox ("Depth camera clipping distance", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.TerrainDepthCamera.GetComponent<Camera> ().farClipPlane = EditorGUILayout.Slider (script.WaterManager.TerrainDepthCamera.GetComponent<Camera> ().farClipPlane, 0, 12000f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
							EditorGUILayout.HelpBox ("Depth camera Field of View", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.TerrainDepthCamera.GetComponent<Camera> ().fieldOfView = EditorGUILayout.Slider (script.WaterManager.TerrainDepthCamera.GetComponent<Camera> ().fieldOfView, 10, 170f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();

							EditorGUILayout.HelpBox ("Depth cutoff", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.DepthRenderController.heightCutoff = EditorGUILayout.Slider (script.WaterManager.DepthRenderController.heightCutoff, 0, 1.1f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
							EditorGUILayout.HelpBox ("Height factor", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.DepthRenderController.heightFactor = EditorGUILayout.Slider (script.WaterManager.DepthRenderController.heightFactor, 0, 15f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
							EditorGUILayout.HelpBox ("Contrast factor", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.WaterManager.DepthRenderController.contrast = EditorGUILayout.Slider (script.WaterManager.DepthRenderController.contrast, 0.01f, 1.15f, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();

							//preview terrain depth texture render
							script.PreviewDepthTexture = EditorGUILayout.Toggle ("Preview depth texture", script.PreviewDepthTexture, GUILayout.MaxWidth (180.0f));
							if (script.PreviewDepthTexture) {
								GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));
								EditorGUILayout.BeginHorizontal ();
								GUILayout.Label (script.WaterManager.TerrainDepthCamera.transform.GetComponent<Camera> ().targetTexture, GUILayout.MaxWidth (410.0f), GUILayout.MaxHeight (410.0f));
								EditorGUILayout.EndHorizontal ();
								GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));	
							}
						}

						// add middle terrain and terrain size - make sure it has the layer assigned
						// instantiate (in terrain center and height = terrain_width/tan(30) for the 60 field of view 
						// adjust DepthCameraPREFAB with terrain layer
						// add camera in waterhandler
						GUILayout.Box ("", GUILayout.Height (3), GUILayout.Width (410));
					}

					//UnityTerrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
					//EditorGUILayout.EndHorizontal();

					if (script.WaterManager != null) {

						EditorGUILayout.HelpBox ("Waves direction", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.WaveDir1Offset.y = EditorGUILayout.Slider (script.WaterManager.WaveDir1Offset.y, -1f, 1, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Waves direction", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.WaveDir1Offset.z = EditorGUILayout.Slider (script.WaterManager.WaveDir1Offset.z, -0.5f, 0.5f, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Extra waves factor X", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.ExtraWavesFactor.x = EditorGUILayout.Slider (script.WaterManager.ExtraWavesFactor.x, -10f, 10, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Extra waves factor Y", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.ExtraWavesFactor.y = EditorGUILayout.Slider (script.WaterManager.ExtraWavesFactor.y, -10f, 10, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Extra waves direction", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.ExtraWavesDirFactor.y = EditorGUILayout.Slider (script.WaterManager.ExtraWavesDirFactor.y, -10f, 10, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						///////

						EditorGUILayout.HelpBox ("Foam", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.FoamOffset = EditorGUILayout.Slider (script.WaterManager.FoamOffset, -10f, 10, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.HelpBox ("Foam cutoff", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.FoamCutoff = EditorGUILayout.Slider (script.WaterManager.FoamCutoff, -10f, 10, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						////

						EditorGUILayout.HelpBox ("Shore blend offset", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.ShoreBlendOffset = EditorGUILayout.Slider (script.WaterManager.ShoreBlendOffset, -0.2f, 2, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Depth Color Offset", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.DepthColorOffset = EditorGUILayout.Slider (script.WaterManager.DepthColorOffset, -140f, 12, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("BumpFocusOffset", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.BumpFocusOffset = EditorGUILayout.Slider (script.WaterManager.BumpFocusOffset, -5f, 5, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("FresnelOffset", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.FresnelOffset = EditorGUILayout.Slider (script.WaterManager.FresnelOffset, -2f, 3, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("FresnelBias", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.FresnelBias = EditorGUILayout.Slider (script.WaterManager.FresnelBias, -140f, 240, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Water height offset", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.waterScaleOffset.y = EditorGUILayout.Slider (script.WaterManager.waterScaleOffset.y, 0.1f, 20, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						///

						script.WaterManager.OverrideReflectColor = EditorGUILayout.Toggle ("Override reflection color", script.WaterManager.OverrideReflectColor, GUILayout.MaxWidth (180.0f));

						EditorGUILayout.HelpBox ("Reflection color", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.ReflectColor = EditorGUILayout.ColorField (script.WaterManager.ReflectColor, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						///

						EditorGUILayout.HelpBox ("Caustic intensity", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.CausticIntensity = EditorGUILayout.Slider (script.WaterManager.CausticIntensity, 0, 200, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Caustic size", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.CausticSize = EditorGUILayout.Slider (script.WaterManager.CausticSize, 0, 500, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.HelpBox ("Shaft size", MessageType.None);
						EditorGUILayout.BeginHorizontal ();
						script.WaterManager.SunShaftsInt = EditorGUILayout.Slider (script.WaterManager.SunShaftsInt, 0, 500, GUILayout.MaxWidth (195.0f));
						EditorGUILayout.EndHorizontal ();

						if (script.TerrainManager != null) {
							EditorGUILayout.HelpBox ("Shaft length", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.TerrainManager.AddShaftsSizeUnder = EditorGUILayout.Slider (script.TerrainManager.AddShaftsSizeUnder, 0, 50, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();

							EditorGUILayout.HelpBox ("Shafts intensity", MessageType.None);
							EditorGUILayout.BeginHorizontal ();
							script.TerrainManager.AddShaftsIntensityUnder = EditorGUILayout.Slider (script.TerrainManager.AddShaftsIntensityUnder, 0, 5, GUILayout.MaxWidth (195.0f));
							EditorGUILayout.EndHorizontal ();
						}
					}


					if (script.WaterManager != null) {
						script.WaterManager.waterType = (Artngame.SKYMASTER.WaterHandlerSM.WaterPreset)EditorGUILayout.EnumPopup (script.WaterManager.waterType, GUILayout.Width (120));

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Caribbean Water", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptA, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.Caribbean;
						}
						EditorGUILayout.EndHorizontal ();
					
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Dark Ocean", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptB, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.DarkOcean;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Emerald shores", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptC, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.Emerald;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Focus Specular Ocean", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptD, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.FocusOcean;
						}
						EditorGUILayout.EndHorizontal ();
					
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Muddy Ocean", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptE, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.Muddy;
						}
						EditorGUILayout.EndHorizontal ();
					
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Ocean", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptF, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.Ocean;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("River", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptG, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.River;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Small Waves", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptH, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.SmallWaves;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Lake", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptI, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.Lake;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Atoll", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (WaterOptJ, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.waterType = WaterHandlerSM.WaterPreset.Atoll;
						}
						EditorGUILayout.EndHorizontal ();
					}
					if (script.WaterManager != null) {
						script.WaterManager.underWaterType = (Artngame.SKYMASTER.WaterHandlerSM.UnderWaterPreset)EditorGUILayout.EnumPopup (script.WaterManager.underWaterType, GUILayout.Width (120));

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Calm Underwater", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (UnderWaterOptA, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.underWaterType = WaterHandlerSM.UnderWaterPreset.Fancy;
						}
						EditorGUILayout.EndHorizontal ();

						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.HelpBox ("Turbulent Underwater", MessageType.None);
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.BeginHorizontal ();
						if (GUILayout.Button (UnderWaterOptB, GUILayout.MaxWidth (300.0f), GUILayout.MaxHeight (80.0f))) {
							script.WaterManager.underWaterType = WaterHandlerSM.UnderWaterPreset.Turbulent;
						}
						EditorGUILayout.EndHorizontal ();
					}

//				EditorGUILayout.BeginHorizontal();
//				if(GUILayout.Button(new GUIContent("Setup mesh terrain"),GUILayout.Width(120))){			
//					if(mesh_terrain != null){
//						
//					}else{
//						Debug.Log ("Add a mesh terrain first");
//					}					
//				}
//				mesh_terrain =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
//				
//				
//				EditorGUILayout.EndHorizontal();				
					EditorGUIUtility.wideMode = false;
				
					EditorGUILayout.BeginHorizontal ();
				
					//ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
					//ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
					EditorGUILayout.EndHorizontal ();					
				}
				EditorGUILayout.EndVertical ();
				///////////////////////////////////////////////////////////////////////////

			}//END CHECK SKYMANAGER EXISTS








			///////////////////////////////////////////////////////////////////////////////
			EditorGUILayout.BeginVertical(GUILayout.MaxWidth(180.0f));			
			//X_offset_left = 200;
			//Y_offset_top = 100;			
			GUILayout.Box("",GUILayout.Height(3),GUILayout.Width(410));	
			GUILayout.Label (MainIcon8,GUILayout.MaxWidth(410.0f));
			EditorGUILayout.LabelField("Particle tools & Special FX",EditorStyles.boldLabel);			
			EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
			script.scaler_folder1 = EditorGUILayout.Foldout(script.scaler_folder1,"Scale size and speed");
			EditorGUILayout.EndHorizontal();
			
			if(script.scaler_folder1){
				EditorGUILayout.BeginHorizontal();
				if(GUILayout.Button(new GUIContent("Scale particles","Scale particle systems"),GUILayout.Width(95))){			
					
					if(Selection.activeGameObject != null){
						ScaleMe(ParticleScaleFactor, Selection.activeGameObject, Exclude_children);
					}else{
						Debug.Log ("Please select a particle system to scale");
					}
				}
				
				if(GUILayout.Button(new GUIContent("Scale speed","Scale playback speed"),GUILayout.Width(95))){
					
					if(Selection.activeGameObject != null){
						ParticleSystem[] PSystems = Selection.activeGameObject.GetComponentsInChildren<ParticleSystem>(Include_inactive);
						
						Undo.RecordObjects(PSystems,"speed scale");
						
						if(PSystems!=null){
							if(PSystems.Length >0){
								for(int i=0;i<PSystems.Length;i++){
									PSystems[i].playbackSpeed = ParticlePlaybackScaleFactor;						
								}
							}
						}
					}else{
						Debug.Log ("Please select a particle system to scale");
					}
				}
				EditorGUILayout.EndHorizontal();				
				EditorGUIUtility.wideMode = false;

				EditorGUILayout.BeginHorizontal();
				
				ParticleScaleFactor = EditorGUILayout.FloatField(ParticleScaleFactor,GUILayout.MaxWidth(95.0f));
				ParticlePlaybackScaleFactor = EditorGUILayout.FloatField(ParticlePlaybackScaleFactor,GUILayout.MaxWidth(95.0f));
				EditorGUILayout.EndHorizontal();
				
				MainParticle =  EditorGUILayout.ObjectField(null,typeof( GameObject ),true,GUILayout.MaxWidth(180.0f));
				
				Exclude_children = EditorGUILayout.Toggle("Exclude children", Exclude_children,GUILayout.MaxWidth(180.0f));
				Include_inactive = EditorGUILayout.Toggle("Include inactive", Include_inactive,GUILayout.MaxWidth(180.0f));
								
			}


			//SPECIAL FX
			EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
			script.scaler_folder11 = EditorGUILayout.Foldout(script.scaler_folder11,"Special FX");
			EditorGUILayout.EndHorizontal();
			
			if (script.scaler_folder11) {

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.HelpBox ("Atomic Bomb", MessageType.None);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (SpecialFXOptA, GUILayout.MaxWidth (260.0f), GUILayout.MaxHeight (80.0f))) {
					GameObject ABOMB = Instantiate (ABOMB_Prefab) as GameObject;
					ABOMB.name = "Atomic Bomb Effect";
					if(Camera.main != null){
						ABOMB.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 50;
					}
					if(script.SkyManager != null){
						for (int i=0;i<ABOMB.GetComponentsInChildren<VolumeParticleShadePDM>(true).Length;i++){
							ABOMB.GetComponentsInChildren<VolumeParticleShadePDM>(true)[i].Sun = script.SkyManager.SUN_LIGHT.GetComponent<Light>();
						}
					}
				}
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.HelpBox ("Asteroid field", MessageType.None);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (SpecialFXOptB, GUILayout.MaxWidth (260.0f), GUILayout.MaxHeight (80.0f))) {
					GameObject ABOMB1 = Instantiate (ASTEROIDS_Prefab) as GameObject;
					ABOMB1.name = "Asteroid field";
					if(Camera.main != null){
						ABOMB1.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 50 + new Vector3(-2000,2500,7200);
					}
				}
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.HelpBox ("Freeze effect - Ice decals", MessageType.None);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (SpecialFXOptC, GUILayout.MaxWidth (260.0f), GUILayout.MaxHeight (80.0f))) {
					GameObject ABOMB2 = Instantiate (FREEZE_EFFECT_Prefab) as GameObject;
					ABOMB2.name = "Freeze & Ice decals";
					if(Camera.main != null){
						ABOMB2.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0 + new Vector3(5,2,10);
					}
				}
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.HelpBox ("Aurora", MessageType.None);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (SpecialFXOptD, GUILayout.MaxWidth (260.0f), GUILayout.MaxHeight (80.0f))) {
					GameObject ABOMB3 = Instantiate (AURORA_Prefab) as GameObject;
					ABOMB3.name = "Aurora effect";
					if(Camera.main != null){
						ABOMB3.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 50 + new Vector3(-95,70,130);
					}
				}
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.HelpBox ("Chain Lightning", MessageType.None);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (SpecialFXOptE, GUILayout.MaxWidth (260.0f), GUILayout.MaxHeight (80.0f))) {
					GameObject ABOMB4 = Instantiate (CHAIN_LIGHTNING_Prefab) as GameObject;
					ABOMB4.name = "Chain Lightning";
					if(Camera.main != null){
						ABOMB4.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1 + new Vector3(0,0,10);
					}
				}
				EditorGUILayout.EndHorizontal ();

				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.HelpBox ("Sand storm", MessageType.None);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				if (GUILayout.Button (SpecialFXOptF, GUILayout.MaxWidth (260.0f), GUILayout.MaxHeight (80.0f))) {
					GameObject ABOMB5 = Instantiate (SAND_STORM_Prefab) as GameObject;
					ABOMB5.name = "Sand storm";
					if(Camera.main != null){
						ABOMB5.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 550;
					}
				}
				EditorGUILayout.EndHorizontal ();
			}

			EditorGUILayout.EndVertical();
			///////////////////////////////////////////////////////////////////////////




			serializedObject.ApplyModifiedProperties ();


//			Handles.color = Color.red;
//			Event cur = Event.current;
//			
//			if (cur.type == EventType.MouseDown && cur.button == 1) {
//				Ray ray = HandleUtility.GUIPointToWorldRay(cur.mousePosition);
//				
//				RaycastHit hit = new RaycastHit();
//				if (Physics.Raycast(ray, out hit, Mathf.Infinity))					
//				{
//					//Handles.SphereCap(i,FIND_moved_pos,Quaternion.identity,script.Marker_size);
//				}
//			}

			Repaint ();
			//Undo.RecordObject (script,"undo");
			EditorUtility.SetDirty (script);
			//if (script.SkyManager != null) {
			//	EditorUtility.SetDirty (script.SkyManager);
			//}
		}


		private void SceneGUI(SceneView sceneview)
		{
			
		}
		


		void ScaleMe(float ParticleScaleFactor, GameObject ParticleHolder, bool Exclude_children){
			
			if(1==1)
			{
				//GameObject ParticleHolder = Selection.activeGameObject;
				//scale parent object
				
				if(Exclude_children){
					
					
					
					ParticleSystem ParticleParent = ParticleHolder.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
					
					if(ParticleParent != null){
						Object[] ToUndo = new Object[2];
						ToUndo[0] = ParticleParent as Object;
						ToUndo[1] = Selection.activeGameObject.transform as Object;
						
						Undo.RecordObjects(ToUndo,"scale");

						if(!script.DontScaleParticleTranf){
							ParticleHolder.transform.localScale = ParticleHolder.transform.localScale * ParticleScaleFactor;
						}
					}
					
					if(ParticleParent!=null && !script.DontScaleParticleProps){
						
						ParticleParent.startSize = ParticleParent.startSize * ParticleScaleFactor;
						
						ParticleParent.startSpeed = ParticleParent.startSpeed * ParticleScaleFactor;				
						
						SerializedObject SerializedParticle = new SerializedObject(ParticleParent);				
						
						if(SerializedParticle.FindProperty("VelocityModule.enabled").boolValue)
						{
							SerializedParticle.FindProperty("VelocityModule.x.scalar").floatValue *= ParticleScaleFactor;
							SerializedParticle.FindProperty("VelocityModule.y.scalar").floatValue *= ParticleScaleFactor;
							SerializedParticle.FindProperty("VelocityModule.z.scalar").floatValue *= ParticleScaleFactor;
							
							Scale_inner(SerializedParticle.FindProperty("VelocityModule.x.minCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("VelocityModule.x.maxCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("VelocityModule.y.minCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("VelocityModule.y.maxCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("VelocityModule.z.minCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("VelocityModule.z.maxCurve").animationCurveValue);
						}
						
						if(SerializedParticle.FindProperty("ForceModule.enabled").boolValue)
						{
							SerializedParticle.FindProperty("ForceModule.x.scalar").floatValue *= ParticleScaleFactor;
							SerializedParticle.FindProperty("ForceModule.y.scalar").floatValue *= ParticleScaleFactor;
							SerializedParticle.FindProperty("ForceModule.z.scalar").floatValue *= ParticleScaleFactor;
							
							Scale_inner(SerializedParticle.FindProperty("ForceModule.x.minCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("ForceModule.x.maxCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("ForceModule.y.minCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("ForceModule.y.maxCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("ForceModule.z.minCurve").animationCurveValue);
							Scale_inner(SerializedParticle.FindProperty("ForceModule.z.maxCurve").animationCurveValue);
						}
						
						SerializedParticle.ApplyModifiedProperties();
					}	
				}
				
				if(!Exclude_children){
					
					ParticleSystem[] ParticleParents = ParticleHolder.GetComponentsInChildren<ParticleSystem>(true);
					
					if(ParticleParents != null){
						Object[] ParticleParentsOBJ = new Object[ParticleParents.Length+1];
						for(int i=0;i<ParticleParents.Length;i++){
							ParticleParentsOBJ[i] = ParticleParents[i] as Object;
						}
						ParticleParentsOBJ[ParticleParentsOBJ.Length-1] = Selection.activeGameObject.transform as Object;
						
						Undo.RecordObjects(ParticleParentsOBJ,"scale");

						if(!script.DontScaleParticleTranf){
							ParticleHolder.transform.localScale = ParticleHolder.transform.localScale * ParticleScaleFactor;
						}
					}

					if(!script.DontScaleParticleProps){
						foreach(ParticleSystem ParticlesA in ParticleHolder.GetComponentsInChildren<ParticleSystem>(true))
						{
							
							ParticlesA.startSize = ParticlesA.startSize * ParticleScaleFactor;
							
							ParticlesA.startSpeed = ParticlesA.startSpeed * ParticleScaleFactor;					
							
							SerializedObject SerializedParticle = new SerializedObject(ParticlesA);
							
							if(SerializedParticle.FindProperty("VelocityModule.enabled").boolValue)
							{
								SerializedParticle.FindProperty("VelocityModule.x.scalar").floatValue *= ParticleScaleFactor;
								SerializedParticle.FindProperty("VelocityModule.y.scalar").floatValue *= ParticleScaleFactor;
								SerializedParticle.FindProperty("VelocityModule.z.scalar").floatValue *= ParticleScaleFactor;
								
								Scale_inner(SerializedParticle.FindProperty("VelocityModule.x.minCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("VelocityModule.x.maxCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("VelocityModule.y.minCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("VelocityModule.y.maxCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("VelocityModule.z.minCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("VelocityModule.z.maxCurve").animationCurveValue);
							}
							
							if(SerializedParticle.FindProperty("ForceModule.enabled").boolValue)
							{
								SerializedParticle.FindProperty("ForceModule.x.scalar").floatValue *= ParticleScaleFactor;
								SerializedParticle.FindProperty("ForceModule.y.scalar").floatValue *= ParticleScaleFactor;
								SerializedParticle.FindProperty("ForceModule.z.scalar").floatValue *= ParticleScaleFactor;
								
								Scale_inner(SerializedParticle.FindProperty("ForceModule.x.minCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("ForceModule.x.maxCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("ForceModule.y.minCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("ForceModule.y.maxCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("ForceModule.z.minCurve").animationCurveValue);
								Scale_inner(SerializedParticle.FindProperty("ForceModule.z.maxCurve").animationCurveValue);
							}
							
							SerializedParticle.ApplyModifiedProperties();
						}	
					}
				}
			}
		}

		void Scale_inner(AnimationCurve AnimCurve){
			
			for(int i = 0; i < AnimCurve.keys.Length; i++)
			{
				AnimCurve.keys[i].value = AnimCurve.keys[i].value * ParticleScaleFactor;
			}
		}



}
}