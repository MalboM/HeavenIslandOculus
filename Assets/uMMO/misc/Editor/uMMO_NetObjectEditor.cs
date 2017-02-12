using UnityEngine;
using UnityEditor;
using System.Collections;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO custom inspector class.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[CanEditMultipleObjects]
[CustomEditor (typeof (uMMO_NetObject),true)]
public class uMMO_NetObjectEditor : PropertyEditor {
	
	//private SerializedProperty showDocumentationInPrefabProperty;
	private SerializedProperty objectTypeProperty;
	private SerializedProperty cameraToActivateOnLocalPlayerProperty;

	private SerializedProperty handleScriptsBasedOnSituationProperty;
	private SerializedProperty clientLocalPlayerScriptsProperty;
	private SerializedProperty clientRemotePlayerScriptsProperty;
	private SerializedProperty serverPlayerScriptsProperty;
	private SerializedProperty serverNPOScriptsProperty;
	private SerializedProperty clientNPOScriptsProperty;

	private SerializedProperty handleScriptsBasedOnRuntimeSituationProperty;
	private SerializedProperty clientLocalPlayerRuntimeScriptsProperty;
	private SerializedProperty clientRemotePlayerRuntimeScriptsProperty;
	private SerializedProperty serverPlayerRuntimeScriptsProperty;
	private SerializedProperty serverNPORuntimeScriptsProperty;
	private SerializedProperty clientNPORuntimeScriptsProperty;
	private SerializedProperty input2CheckProperty;

	private SerializedProperty synchronizeAnimationsProperty;
	private SerializedProperty synchronizePositionProperty;
	private SerializedProperty synchronizeRotationProperty;
	
	private SerializedProperty mecanimIntsProperty; 
	private SerializedProperty mecanimFloatsProperty; 
	private SerializedProperty mecanimBoolsProperty; 
	
	private SerializedProperty addSerializerModsAutomaticallyIfNonePresentAtRuntimeProperty;
	private SerializedProperty networkViewSerializerModsProperty;
	
	private SerializedProperty objectContainingAnimationsProperty;
	private SerializedProperty activateCallbackFunctionsProperty;
	private SerializedProperty callbackFunctionsToExecuteProperty;
	private SerializedProperty customModulesProperty;
	
	
	protected uMMO_NetObject instance;
	
	
	private const string NL = "\r\n";
	private const string DNL = "\r\n\r\n";	
	
	protected bool showObjectTypeSpecificSettings = true;
	protected bool showSynchronizationSettings = true;
	protected bool showOtherSettings = true;
	
	void onEnable() {
			
	}
	
	protected override void Initialize () {
		//showDocumentationInPrefabProperty = serializedObject.FindProperty("showDocumentationInPrefab");
		objectTypeProperty = serializedObject.FindProperty("objectType");
		cameraToActivateOnLocalPlayerProperty = serializedObject.FindProperty("cameraToActivateOnLocalPlayer");

		handleScriptsBasedOnSituationProperty = serializedObject.FindProperty("handelScriptsBasedOnSituation");
		clientLocalPlayerScriptsProperty = serializedObject.FindProperty("clientLocalPlayerScripts");
		clientRemotePlayerScriptsProperty = serializedObject.FindProperty("clientRemotePlayerScripts");
		serverPlayerScriptsProperty = serializedObject.FindProperty("serverPlayerScripts");
		serverNPOScriptsProperty = serializedObject.FindProperty("serverNPOScripts");
		clientNPOScriptsProperty = serializedObject.FindProperty("clientNPOScripts");

		handleScriptsBasedOnRuntimeSituationProperty = serializedObject.FindProperty("handelScriptsBasedOnRuntimeSituation");
		clientLocalPlayerRuntimeScriptsProperty = serializedObject.FindProperty("clientLocalPlayerRuntimeScripts");
		clientRemotePlayerRuntimeScriptsProperty = serializedObject.FindProperty("clientRemotePlayerRuntimeScripts");
		serverPlayerRuntimeScriptsProperty = serializedObject.FindProperty("serverPlayerRuntimeScripts");
		serverNPORuntimeScriptsProperty = serializedObject.FindProperty("serverNPORuntimeScripts");
		clientNPORuntimeScriptsProperty = serializedObject.FindProperty("clientNPORuntimeScripts");
		input2CheckProperty = serializedObject.FindProperty("input2Check");

		synchronizeAnimationsProperty = serializedObject.FindProperty("synchronizeAnimations");
		objectContainingAnimationsProperty = serializedObject.FindProperty("objectContainingAnimations");
		synchronizePositionProperty = serializedObject.FindProperty("synchronizePosition");
		synchronizeRotationProperty = serializedObject.FindProperty("synchronizeRotation");
		
		mecanimFloatsProperty = serializedObject.FindProperty("mecanimFloats");
		mecanimIntsProperty = serializedObject.FindProperty("mecanimInts");
		mecanimBoolsProperty = serializedObject.FindProperty("mecanimBools");		
		
		addSerializerModsAutomaticallyIfNonePresentAtRuntimeProperty = serializedObject.FindProperty("addSerializerModsAutomaticallyIfNonePresentAtRuntime");
		networkViewSerializerModsProperty = serializedObject.FindProperty("networkViewSerializerMods");
		
		activateCallbackFunctionsProperty = serializedObject.FindProperty("activateCallbackFunctions");
		callbackFunctionsToExecuteProperty = serializedObject.FindProperty("callbackFunctionsToExecute");
		
		customModulesProperty = serializedObject.FindProperty("customModules");
		
	}
	
	public override void OnInspectorGUI () {
		
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
		
		
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);
		
		instance = (uMMO_NetObject) target;
		
		BeginEdit();
			/*PropertyField ("Show documentation/hints here",showDocumentationInPrefabProperty);
			if (instance.showDocumentationInPrefab)
				Comment ("Switch documentation on this prefab on and off (on recommended). Having the documentation on has the advantage of knowing exactly what an attribute can and should be used for. Once you know it all already, " +
					"you can just switch it off for beter overview.");		*/
		
			if (uMMO.get.showDocumentationInEditor)
				Comment ("This is the script uMMO_NetObject. Put this script on all characters/buildings/plants/magic rocks/and all other things that you want to send network traffic to, and/or receive from. You can switch this and the comments below off by disabling the option " +
					"\"Show documentation/hints in Editor\" on the uMMO prefab instantiated in your scene.");
		
			//BeginSection("Object-type Specific Settings");
			showObjectTypeSpecificSettings = EditorGUILayout.Foldout(showObjectTypeSpecificSettings, "Object-type Specific Settings",myFoldoutStyle);			
			
			if (showObjectTypeSpecificSettings) {
				PropertyField(objectTypeProperty);
			
				if (uMMO.get.showDocumentationInEditor)			
					Comment ("Object type is a very important attribute, as it defines what the GameObject containing this script represents in your MMO/multiplayer world. It can be a \"Player\" or a \"NonPlayerObject\".\r\n\r\n" +
						"A \"Player\" is a GameObject which absolutely should not be instantiated in design time (at least not for production exports). When this script (uMMO_NetObject) is being dragged on a GameObject it represents a real human network player. It should be made available in project hierarchy as a " +
						"prefab. You should also drag that prefab onto the global uMMO prefab on the attribute \"PlayerCharOnAuthoritativeSetup\" or \"PlayerCharOnNonAuthoritativeSetup\" (depending on which setup you're using).\r\n\r\n" +
						"A \"NonPlayerObject\" is anything else in your MMO world, which is NOT a real networkplayer object representing a human. It's an NPC (Non-Player character, a computer-controlled character, like i.e. a character offering quests) or an NPO (Non-Player Object, like a network-controlled building or a tree or a stone), anything that you want to send network traffic to, and/or receive from, which does not represent a human player.");
			
				string genericObjectTypeComment1 = "";
					
				string genericObjectTypeComment2 = "";
			
				string genericObjectTypeComment3 = "";

				string genericObjectTypeComment4 = "";
				
				if (instance.objectType == uMMO_ObjectType.Player) {
			
					PropertyField(cameraToActivateOnLocalPlayerProperty);
					
					PropertyField(handleScriptsBasedOnSituationProperty);
					if (instance.handelScriptsBasedOnSituation) {
						ArrayPropertyField(clientLocalPlayerScriptsProperty);
						ArrayPropertyField(clientRemotePlayerScriptsProperty);
						ArrayPropertyField(serverPlayerScriptsProperty);
					}

					PropertyField(handleScriptsBasedOnRuntimeSituationProperty);
					if (instance.handelScriptsBasedOnRuntimeSituation) {
						ArrayPropertyField(clientLocalPlayerRuntimeScriptsProperty);
						ArrayPropertyField(clientRemotePlayerRuntimeScriptsProperty);
						ArrayPropertyField(serverPlayerRuntimeScriptsProperty);
					}
			
					ArrayPropertyField(input2CheckProperty);
				
					genericObjectTypeComment1 = "The above attributes are visible, once you chose \"Player\" on the attribute \"Object type\".\r\n\r\nIn many MMO setups every player has it's own camera, which should be switched to, once the player is instantiated. To use a feature like that, drag the camera (the GameObject which \"carries\" the camera actually) which exists in the prefab hierarchy of the player-object onto \"CameraToActivateOnLocalPlayer\"." +
						"This particular camera will be activated, while all other cameras which are possibly existing in the scene are switched off.\r\n\r\n" +
						"\"HandleScriptsBasedOnSituation\" activates a very central feature in uMMO: Being able to differentiate between scripts which should only be available on the prefab if it is instantiated on server or on client side." +
						"Actually the same object gets instantiated independently on whether you are compiling a server or a client, but it is very important to realize (and using this plugin, implement) that not all scripts are necessary (or even helpful) on every side of the game.\r\n\r\n";				
				
					genericObjectTypeComment2 = "So by enabling \"HandleScriptsBasedOnSituation\" on a \"Player\" you have access to 3 different lists: ClientLocalPlayerScripts, ClientRemotePlayerScripts and ServerPlayerScripts. These lists are easy to maintain: " +
						"Just drag and drop a script which is placed on the same GameObject with the mouse on i.e. the list ClientLocalPlayerScripts. That means you want this script to be active on instances of this prefab, when it is instantiated " +
						"on a client, and specifically on the client controlling exactly this instance. Drop it on ClientRemotePlayerScripts, to enable this script only if the instance of this prefab in question is NOT the local human player, but instantiated on a client " +
						"which controls a different instance. But adding a script to both lists, you make sure that this script is active on ANY client. Same goes for ServerPlayerScripts, but if you drop a script there (remember: always drag and drop with the mouse, scripts that already exist on the SAME GameObject)" +
						"this script will be active if the instance of this prefab is being instantiated on the server, so it becomes a serverscript automatically. This is a very powerful system, which allows you to define precisely which script should exist where.\r\n\r\n";
						
					
					genericObjectTypeComment3 = "Enable \"HandleScriptsBasedOnRuntimeSituation\" for the same functionality as \"HandleScriptsBasedOnSituation\" with the exception that you add actual scriptnames to the list," +
						"instead of dragging scripts on there. The difference is that this way you can set up a uMMO character which assembles at runtime (so in case any of the scripts/objects it will be using are not present at design time.\r\n\r\n";
					
					
					genericObjectTypeComment4 = "\"Input2Check\" is another powerful feature of uMMO. It is only needed when you use authoritative server setup, otherwise leave the list empty. It is a list of string which describe what keystrokes are being send to the server. In this list of strings write down the names of the axes of the Unity Input Manager this character uses. I.e. if it uses the axis named \"Horizontal\" " +
						"in your movement script where you would normally use\r\n\r\n" +
						"rotationAmount = Input.GetAxis (\"Horizontal\") * turnSpeed * Time.deltaTime;\r\n\r\n" +
						"you can now just use\r\n\r\n" +
						"rotationAmount = uMMO_NetObject.ME(this).GetAxis (\"Horizontal\") * turnSpeed * Time.deltaTime;\r\n\r\n" +
						"to request the amount of \"Horizontal\" movement. So you don't have to redesign your whole input system to use uMMO, it uses the same system you did earlier. Keep in mind, that, if you use authoritative server setup you'll probably will want to " +
						"put your movement script (with the line above) into the ServerPlayerScripts list, if you don't, put it on ClientLocalPlayerScripts, because in that case your keystrokes are not being send to the server.";
					
					
					
				} else if (instance.objectType == uMMO_ObjectType.NonPlayerObject) {
					PropertyField(handleScriptsBasedOnSituationProperty);
					if (instance.handelScriptsBasedOnSituation) {
						ArrayPropertyField(serverNPOScriptsProperty);
						ArrayPropertyField(clientNPOScriptsProperty);
					}

					PropertyField(handleScriptsBasedOnRuntimeSituationProperty);
					if (instance.handelScriptsBasedOnRuntimeSituation) {
						ArrayPropertyField(serverNPORuntimeScriptsProperty);
						ArrayPropertyField(clientNPORuntimeScriptsProperty);
					}

					genericObjectTypeComment1 = "The above attributes are visible, once you chose \"NonPlayerObject\" on the attribute \"Object type\".\r\n\r\n"+
						"\"HandleScriptsBasedOnSituation\" activates a very central feature in uMMO: Being able to differentiate between scripts which should only be available on the prefab if it is instantiated on server or on client side." +
						"Actually the same object gets instantiated independently on whether you are compiling a server or a client, but it is very important to realize (and using this plugin, implement) that not all scripts are necessary (or even helpful) on every side of the game.\r\n\r\n";								
				
					genericObjectTypeComment2 = "So by enabling \"HandleScriptsBasedOnSituation\" on a \"NonPlayerObject\" you have access to 2 different lists: ServerNPOScripts and ClientNPOScripts. These lists are easy to maintain: " +
						"Just drag and drop a script which is placed on the same GameObject with the mouse on i.e. the list ClientNPOScripts. That means you want this script to be active on instances of this prefab, when it is instantiated " +
						"on a client as NPC or NPO. Drop it on ServerNPOScripts, to enable this script only if the instance of this prefab in question is NOT instantiated on a client, but on the server. So you drop scripts there which only be executed on the server, like NPC AI scripts. " +
						"This is a very powerful system, which allows you to define precisely which script should exist where.\r\n\r\n";
						
					genericObjectTypeComment3 = "Enable \"HandleScriptsBasedOnRuntimeSituation\" for the same functionality as \"HandleScriptsBasedOnSituation\" with the exception that you add actual scriptnames to the list," +
						"instead of dragging scripts on there. The difference is that this way you can set up a uMMO character which assembles at runtime (so in case any of the scripts/objects it will be using are not present at design time.\r\n\r\n";
				
				}
				if (uMMO.get.showDocumentationInEditor)
					Comment (genericObjectTypeComment1 + genericObjectTypeComment2 + genericObjectTypeComment3 + genericObjectTypeComment4);
				
			//EndSection();
			}
			showSynchronizationSettings = EditorGUILayout.Foldout(showSynchronizationSettings, "Synchronization Settings",myFoldoutStyle);
			if (showSynchronizationSettings) {
				PropertyField(synchronizeAnimationsProperty);
				if (instance.synchronizeAnimations) {
					PropertyField(objectContainingAnimationsProperty);
			
					ArrayPropertyField(mecanimBoolsProperty); 
					ArrayPropertyField(mecanimFloatsProperty); 
					ArrayPropertyField(mecanimIntsProperty); 				
								
				}
				if (uMMO.get.showDocumentationInEditor)
					Comment ("If the character this script is representing is animated, and you want these animations to be automatically synchroninzed over the network, you should enable the switch \"SynchronizeAnimations\". " +
						"If you do, an attribute called \"ObjectContainingAnimations\" will appear. If the \"Animation\" component which holds the animations you want to synchronize over the network are NOT on the same GameObject as " +
						"this component drag the corresponding component in \"ObjectContainingAnimations\", so that this component can find the object holding the correct animations." +
						"" + NL + NL +
						"If you use Mecanim animations on this character, the same as above applies for the \"Animator\" component. Furthermore you have to specify in \"MacanimInts\", \"MecanimFloats\" and \"MecanimBools\" the " +
						"corresponding Mecanim variables which should be observed and syncronized by uMMO.");
			
				PropertyField(synchronizePositionProperty);
				PropertyField(synchronizeRotationProperty);
				if (uMMO.get.showDocumentationInEditor)
					Comment ("If you want to synchronize position and/or rotation of this object across the network, activate the according switches above.");
				PropertyField(addSerializerModsAutomaticallyIfNonePresentAtRuntimeProperty);
				ArrayPropertyField(networkViewSerializerModsProperty);
			
			
			
				if (uMMO.get.showDocumentationInEditor)
					Comment ("NetworkViewSerializer modules are pretty handy for synchronizing properties thorughout the network. A couple of ready-to-use NetworkViewSerializer module come with vanilla uMMO. Those are located in " +
						"\"Assets/uMMO/prefabs/modules/NetObject/\" and can quite easily be dragged-and-dropped here onto the \"networkViewSerializerMods\" property. This property resembles a list of modules, which are ALL being executed " +
						"once the function OnSerializeNetworkView is being called by Unity. This way, you can, instead of having to have all synchronization code in one class, you can split them, to have maximum flexibility. "+
						NL + "So when you previously had:"+
						DNL +
						"void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {" + NL +
						"   if (stream.isReading) {"+ NL +
						"  	   //reading" + NL +
						"   } else {" + NL +
						"      //writing" + NL +	
						"   }" + NL +
						"}"+ NL +
						NL + 
						"you can now use:" +DNL+
						"public class uMMO_NetObject_YOURMODNAME : uMMO_NetObject_NetworkViewSerializer_Module {" +NL+
						"   public override void onReadFromNetworkView(BitStream stream, NetworkMessageInfo info) {" +NL+
						"      //reading" +NL+
						"   }"+NL+
						"   public override void onWriteToNetworkView(BitStream stream, NetworkMessageInfo info) {" +NL+
						"      //writing" +NL+
						"   }"+NL+
						"}" +
						DNL +
						"If you want standard network serializer mods to be added by default automatically if none are present at runtime activate the switch above. " +
						"This is activated by default to help beginners set up their scenes quicker"
					);

			}
			showOtherSettings = EditorGUILayout.Foldout(showOtherSettings, "Other Settings",myFoldoutStyle);
			if (showOtherSettings) {
				PropertyField(activateCallbackFunctionsProperty);
				if (instance.activateCallbackFunctions) {
					ArrayPropertyField(callbackFunctionsToExecuteProperty);
				}
			
				if (uMMO.get.showDocumentationInEditor)
					Comment ("If you enable the attribute \"ActivateCallbackFunctions\" a list of preset callback functions appears. All those will be executed in that case, of course in the appropriate situation. I.e. " +
						"\"__uMMO_localPlayer_init\" is being executed once an instance of the GameObject \"carrying\" this component is being instantiated, and it is represented by the character being the local human player AND controlled by the local human player. " +
						"The same happens for \"__uMMO_serverPlayer_init\" i.e. only that this function is executed if this component is being instantiated on a GameObject on the server, representing a human player. These callback functions enable " +
						"you to i.e. declare function\r\n\r\n" +
						"void uMMO_localPlayer_init() {\r\n\r\n" +
						"	//do stuff which should be executed when the local player character is being instantiated.\r\n\r\n"+
						"}\r\n\r\n" +
						"in any script/component, attached to this very same GameObject. It will be executed, if the function is added to the list above.");	
			
				ArrayPropertyField(customModulesProperty);
				if (uMMO.get.showDocumentationInEditor) 
					Comment ("Custom modules don't aim at specific functionality. You can use custom module slots for any functionality you see fit. To create a custom module create a class which inherits from uMMO_NetObject_Custom_Module. " +
						"This way you can i.e. extend uMMO without ever having to overwrite native code. Also you are encouraged to publish your extensions! Create a prefab from your custom module so that others can just drag them onto this list. These modules are being initialized when the NetObject is being instantiated ( Awake() ).");					
			}
		EndEdit();
	}
}
