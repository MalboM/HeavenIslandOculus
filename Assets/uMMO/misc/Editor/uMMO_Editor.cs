using UnityEngine;
using UnityEditor;
using System.Collections;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO custom inspector class.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[CustomEditor (typeof (uMMO), true)]
public class uMMO_Editor : PropertyEditor {
	
	private SerializedProperty showDocumentationInEditorProperty;
	private SerializedProperty initializeSecurityProperty;
	private SerializedProperty authoritativeServerSetupProperty;
	private SerializedProperty playerCharOnAuthoritativeSetupProperty;
	private SerializedProperty playerCharOnNonAuthoritativeSetupProperty;
	private SerializedProperty architectureToCompileProperty;
	private SerializedProperty showDebugHintsProperty;
	
	private SerializedProperty kickPlayerOnTimeoutProperty;
	private SerializedProperty secondsToCheckForTimeoutsProperty;
	private SerializedProperty secondsUntilDisconnectDueToTimeoutProperty;	
	
	private SerializedProperty spawningMethodProperty;
	private SerializedProperty dataTransmissionFilterProperty;
	private SerializedProperty connectionGUIProperty;
	private SerializedProperty customModulesProperty;
	private SerializedProperty showFullVersionLicensingInfoProperty;
	
	protected uMMO instance;
	
	protected static bool showGeneralSettings = true;
	protected static bool showTimeoutSettings = true;
	protected static bool showModules = true;
	
	protected override void Initialize () {
		showDocumentationInEditorProperty = serializedObject.FindProperty("showDocumentationInEditor");
		initializeSecurityProperty = serializedObject.FindProperty("initializeSecurity");
		authoritativeServerSetupProperty = serializedObject.FindProperty("authoritativeServerSetup");
		playerCharOnAuthoritativeSetupProperty = serializedObject.FindProperty("playerCharOnAuthoritativeSetup");
		playerCharOnNonAuthoritativeSetupProperty = serializedObject.FindProperty("playerCharOnNonAuthoritativeSetup");
		architectureToCompileProperty = serializedObject.FindProperty("architectureToCompile");
		showDebugHintsProperty = serializedObject.FindProperty("showDebugHints");
		
		kickPlayerOnTimeoutProperty = serializedObject.FindProperty("kickPlayerOnTimeout");
		secondsToCheckForTimeoutsProperty = serializedObject.FindProperty("secondsToCheckForTimeouts");
		secondsUntilDisconnectDueToTimeoutProperty = serializedObject.FindProperty("secondsUntilDisconnectDueToTimeout");
		
		
		spawningMethodProperty = serializedObject.FindProperty("spawningMethod");
		dataTransmissionFilterProperty = serializedObject.FindProperty("dataTransmissionFilter");
		connectionGUIProperty = serializedObject.FindProperty("connectionGUI");
		
		customModulesProperty = serializedObject.FindProperty("customModules");
		
		showFullVersionLicensingInfoProperty = serializedObject.FindProperty("showFullVersionLicensingInfo");
	}
	
	public override void OnInspectorGUI () {
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);
		
		instance = (uMMO) target;
		
		BeginEdit();
			
			//source: http://answers.unity3d.com/questions/48541/custom-inspector-changing-foldouts-text-color.html
			GUIStyle myFoldoutStyle = new GUIStyle(EditorStyles.foldout);
			myFoldoutStyle.fontStyle = FontStyle.Bold;
			myFoldoutStyle.fontSize = 15;
			Color myStyleColor = Color.cyan;
			if (!UnityEditorInternal.InternalEditorUtility.HasPro()) {
				myStyleColor = Color.blue;
			}
			myFoldoutStyle.normal.textColor = myStyleColor;
			myFoldoutStyle.onNormal.textColor = myStyleColor;
			myFoldoutStyle.hover.textColor = myStyleColor;
			myFoldoutStyle.onHover.textColor = myStyleColor;
			myFoldoutStyle.focused.textColor = myStyleColor;
			myFoldoutStyle.onFocused.textColor = myStyleColor;
			myFoldoutStyle.active.textColor = myStyleColor;
			myFoldoutStyle.onActive.textColor = myStyleColor;
		
			if (instance.showDocumentationInEditor)
					Comment ("uMMO prefab is the GameObject carrying this central uMMO script. You MUST instantiate it (once!) in order to use uMMO functionality. If you want to find out what functionality this script exposes, " +
						"look into the class, or, from the outside of the class type\r\n\r\n" +
						"uMMO.get.\r\n\r\n" +
						"in an IDE supporting code-completion like MonoDevelop (comes with Unity by default).");
		
			PropertyField ("Show documentation/hints in Editor",showDocumentationInEditorProperty);
			if (instance.showDocumentationInEditor)
				Comment ("Switch documentation on this prefab and on prefabs with the script uMMO_NetObject on and off (on recommended). Having the documentation on has the advantage of knowing exactly what an attribute can and should be used for. Once you know it all already, " +
					"you can just switch it off for better overview of the features/options available.");	
		
			if (showFullVersionLicensingInfoProperty != null) {
				PropertyField ("Show licensing info",showFullVersionLicensingInfoProperty);
				if (instance.showDocumentationInEditor)
					Comment ("You are using a free limited copy of uMMO. The server will run for a maximum of 12 hours so that you can do load- and compatibility-testing with your game setup. After 12 hours the server " +
						"will close the connection, and all clients will have to reconnect.\r\n\r\n" +
						"In order to run a server for an unlimited amount of time and to receive full unsealed source code please purchase uMMO in the Asset Store: https://www.assetstore.unity3d.com/#/content/13867 .\r\n\r\n" +
						"With this option you can enable and disable the advertisement watermark.");
			}
		
			showGeneralSettings = EditorGUILayout.Foldout(showGeneralSettings, "General Settings",myFoldoutStyle);
			if (showGeneralSettings) {

				PropertyField (initializeSecurityProperty);	
				if (instance.showDocumentationInEditor)
					Comment ("Use native in-built Unity security (Recommended).");
			
				PropertyField (authoritativeServerSetupProperty);	
		
				if (instance.authoritativeServerSetup) {
					PropertyField (playerCharOnAuthoritativeSetupProperty);
				} else {
					PropertyField (playerCharOnNonAuthoritativeSetupProperty);
				}
		
				if (instance.showDocumentationInEditor)
					Comment ("An authoritative server setup allows for all \"thinking\" and management to be reffered to the server. That includes i.e. collision detection (recommended i.e. to counter out-of-map-glitching). " +
						"Technically if you enable authoritative server setup all objects will be owned by the server (you can ultimately change that in the SpawningMethod module), and objects are only being controlled by input " +
						"being sent to the server and i.e. movement being synchronized back to the client(s).\r\n\r\n" +
						"uMMO gives you the possibility to equip different GameObjects as spawning objects for players depending on whether you choose authoritative or non-authoritative server setup. By switching this option you can test both possibilities and decide what best fits the needs for your game." +
						"Keep in mind, that you have to design the spawn objects according to your choice. I.e. if you use authoritative server setup equip the script which is controlling your character movement only on server side, " +
						"so that the server executes the movement and sends the result of it back to the client(s).\r\n\r\n" +
						"Generally authoritative server setup is very much recommended, because it is much more resistant to cheating/nagging/glitching attacks by clients. Of course only selecting \"authoritative server setup\" does " +
						"not complete the job. You have to commit to the idea of the server owning all objects and only sending synchronization to the client all throughout the development process.");
		
		
				PropertyField (architectureToCompileProperty);
				if (instance.showDocumentationInEditor)
					Comment ("Switch what architecture will be deployed once starting the game (in Unity Editor or as an exported executable). Your choices for production purposes here are \"Server\" and \"Client\".\r\n\r\n" +
						"Additionally, you can choose \"TEST_UnityEditorIsServer_OthersAreClients\" for testing purposes within the editor. What is very convenient about this, is that you can test server AND client without having to compile both or change special settings in order to do that." +
						"I.e. you want to execute a short test of a new RPC function you just build, just export to standalone player, then hit start in the Unity Editor. The standalone player will be your client, the Unity Editor will automatically be your server, just hit connect in the client, and test away ;)" +
						"Same but the other way around goes for \"Test_UnityEditorIsClient_OtherIsServer\", everything you export will be the server, the Unity Editor self will be the client. Keep in mind that these TEST functions are really only for testing," +
						"not for production use."); 

				PropertyField (showDebugHintsProperty);
				if (instance.showDocumentationInEditor)
					Comment ("Enable this to show debug hints of the uMMO plugin in console.");
			}
			
			showTimeoutSettings = EditorGUILayout.Foldout(showTimeoutSettings, "Timeout Settings",myFoldoutStyle);
			if (showTimeoutSettings) {
				PropertyField (kickPlayerOnTimeoutProperty);
				if (instance.kickPlayerOnTimeout) {
					PropertyField (secondsToCheckForTimeoutsProperty);
					PropertyField (secondsUntilDisconnectDueToTimeoutProperty);	
				}
		
				if (instance.showDocumentationInEditor)			
					Comment ("Decide here whether network players will be kicked if they were idle for an amount of seconds to be determined by you. You can also decide how often this is being checked. Checking every second is the most accurate but will definitely " +
					 	"require more processor resources. \r\n\r\n" +
					 	"Enabling timeout does only make sense in authoritative setup up until now, because then the clients sends keystrokes to the server and thus the server knows, that the player is actually really playing. But of course you can extend this functionality. " +
					 	"In uMMO_NetObject just call \r\n\r\n" +
					 	"updateActivityTimestamp();\r\n\r\n" +
					 	"to update the timestamp that is being used on the server to determine, when the last action was performed by the owner of that NetObject.");
		
			}
		
			//BeginSection("Modules");
			showModules = EditorGUILayout.Foldout(showModules, "Modules",myFoldoutStyle);
			if (showModules) {				
				PropertyField (spawningMethodProperty);
				PropertyField (dataTransmissionFilterProperty);		
				PropertyField (connectionGUIProperty);
				if (instance.showDocumentationInEditor)
					Comment ("Modules in uMMO are classes which inherit from certain abstract classes and \"force\" you to implement certain functionality. I.e. if you create a SpawningMethod module, you create a class which inherits from " +
						"uMMO_Main_SpawningMethod_Module (which, like all modules ultimately inherit from abstract class uMMO_Module) and you are then obligated to implement 5 functions: instantiateUMMONetObject, OnPlayerConnected, OnConnectedToServer, OnPlayerDisconnected and OnDisconnectedFromServer. The last 4 are " +
						"Unity event/callback functions, i.e. OnPlayerConnected() is being called on the server once a player connects to it. Read about unity events in the official documentation.\r\n\r\n" +
						"When you implemented a module, just put it on a new and empty GameObject, and drag that GameObject into the project hierarchy, remove it from scene hierarchy, and drag the newly created prefab onto the uMMO prefab instantiated in your " +
						"scene. I.e. in case you created a ConnectionGUI module, just drag in onto the ConnectionGUI attribute of the instantiated uMMO prefab.\r\n\r\n" +
						"To get to know more about a specific module - either in your own scene or in one of the demo scenes included in uMMO - doubleclick on a module slot on this prefab which is not empty/null (usually if a module slot is not set, " +
						"it shows \"None (module type/name)\", in that case nothing happens when you doubleclick it, and it does not have functionality) to view attributes which can be set on that particular module.");
			
				ArrayPropertyField(customModulesProperty);
				if (instance.showDocumentationInEditor)
					Comment ("Custom modules don't aim at specific functionality. You can use custom module slots for any functionality you see fit. To create a custom module create a class which inherits from uMMO_Main_Custom_Module. " +
						"This way you can i.e. extend uMMO without ever having to overwrite native code. Also you are encouraged to publish your extensions! Create a prefab from your custom module so that others can just drag them onto this list. These modules get initialized at the start of the scene ( Awake() )");
			}
			//EndSection();
			
		EndEdit();
	}
	
}
