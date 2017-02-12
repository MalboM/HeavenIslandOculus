using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;

/*
 * @author SoftRare - www.softrare.eu
 * This class represents an object which is being synchronized across the network. It can be a network player, an AI (NPC) or a building/other object (NPO). 
 * Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
[RequireComponent (typeof (NetworkView))]
#if !IS_UNLICENSED
public class uMMO_NetObject : MonoBehaviour {
#else
public sealed class uMMO_NetObject : MonoBehaviour {
#endif
	
	/*<-- read up on the following attributes on in-Editor documentation: */
	//####
	public uMMO_ObjectType objectType;
	
	public GameObject objectContainingAnimations;
	
	public bool activateCallbackFunctions;
	public List<string> callbackFunctionsToExecute = 
		new List<string>{"__uMMO_localPlayer_init","__uMMO_remotePlayer_init","__uMMO_serverPlayer_init","__uMMO_clientNPO_init","__uMMO_serverNPO_init"};
	
	public Camera cameraToActivateOnLocalPlayer;
	
	public bool handelScriptsBasedOnSituation;
	public List<Component> clientLocalPlayerScripts; //movement/behaviour scripts for local players ( executed on client side )
	public List<Component> clientRemotePlayerScripts; //aim on/click on/mark scripts for remote players ( executed on client side )
	public List<Component> serverPlayerScripts; //collision scripts for players ( executed on server side )
	public List<Component> serverNPOScripts; //movement/behaviour scripts for Non-Player-Objects ( executed on server side )
	public List<Component> clientNPOScripts; //aim on/click on/mark scripts for Non-Player-Objects ( executed on client side )

	//equivalent of lists above, only scripts are detected at runtime
	public bool handelScriptsBasedOnRuntimeSituation;
	public List<string> clientLocalPlayerRuntimeScripts; 
	public List<string> clientRemotePlayerRuntimeScripts; 
	public List<string> serverPlayerRuntimeScripts; 
	public List<string> serverNPORuntimeScripts; 
	public List<string> clientNPORuntimeScripts; 
	
	[HideInInspector]
	public bool isLocalPlayer = false;
	public NetworkPlayer nplayer;
	
	public bool synchronizeAnimations = true;
	
	public bool synchronizePosition = true;
	
	public bool synchronizeRotation = true;
		
	public List<string> mecanimFloats; 
	
	public List<string> mecanimInts; 

	public List<string> mecanimBools; 		
	
	public List<string> input2Check;
		
	public bool addSerializerModsAutomaticallyIfNonePresentAtRuntime = true;
		
	public List<uMMO_NetObject_NetworkViewSerializer_Module> networkViewSerializerMods;	
		
	public List<uMMO_NetObject_Custom_Module> customModules;
	/* #### -->*/
		
	protected uMMO_NetObject_Module[] allModules;
	
	/* Whether the local client is pressing a button */	
	[HideInInspector]
	public bool isLocalClientGeneratingInput = false;		
	
	/* contains the script lists, which determines which script will be removed in certain situations ( look at clientLocalPlayerScripts, clientRemotePlayerScripts, serverPlayerScripts, serverNPOScripts, clientNPOScripts) */
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		List<List<Component>> scriptLists = new List<List<Component>>();	
		List<List<string>> runtimeScriptLists = new List<List<string>>();

#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,float> inputAxis = new Dictionary<string, float>();
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,float> lastInputAxis = new Dictionary<string, float>();	
	
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,float> inputAxisRaw = new Dictionary<string, float>();
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,float> lastInputAxisRaw = new Dictionary<string, float>();
	
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,bool> inputButton = new Dictionary<string, bool>();
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,bool> lastInputButton = new Dictionary<string, bool>();
	
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,bool> inputButtonUp = new Dictionary<string, bool>();
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,bool> lastInputButtonUp = new Dictionary<string, bool>();
	
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,bool> inputButtonDown = new Dictionary<string, bool>();
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 Dictionary<string,bool> lastInputButtonDown = new Dictionary<string, bool>();
	
	//those are not implemented at the moment:
	/*protected Dictionary<string,bool> inputKey = new Dictionary<string, bool>();
	protected Dictionary<string,bool> lastInputKey = new Dictionary<string, bool>();	
	
	protected Dictionary<string,bool> inputKeyUp = new Dictionary<string, bool>();
	protected Dictionary<string,bool> lastInputKeyUp = new Dictionary<string, bool>();	
	
	protected Dictionary<string,bool> inputKeyDown = new Dictionary<string, bool>();
	protected Dictionary<string,bool> lastInputKeyDown = new Dictionary<string, bool>();*/		
	/* --> */
	
	/* a static variable which returns the uMMO_NetObject of the GameObject which is under control by the current network player */
	public static uMMO_NetObject LOCAL_PLAYER = null;
	
	/* a static variable which returns the uMMO_NetObject of the GameObject which is under control by the current network player.
	  Use this to access input values. To read up on how to use this, please refer to the in-Editor documentation on the attribute "input2Check" of this component. */
	public static uMMO_NetObject ME(Component c) {
		return c.GetComponent<uMMO_NetObject>();
	}
	
	/* class initialization */
	protected void Awake() {
		
		GetComponent<NetworkView>().observed = this;
		
		if (objectContainingAnimations == null) {
			objectContainingAnimations = gameObject;	
		}	
		
		scriptLists.Add (clientLocalPlayerScripts);
		scriptLists.Add (clientRemotePlayerScripts);
		scriptLists.Add (serverPlayerScripts);
		scriptLists.Add (serverNPOScripts);
		scriptLists.Add (clientNPOScripts);

		runtimeScriptLists.Add (clientLocalPlayerRuntimeScripts);
		runtimeScriptLists.Add (clientRemotePlayerRuntimeScripts);
		runtimeScriptLists.Add (serverPlayerRuntimeScripts);
		runtimeScriptLists.Add (serverNPORuntimeScripts);
		runtimeScriptLists.Add (clientNPORuntimeScripts);
		
		foreach(string input in input2Check) {
			
			inputAxis.Add (input,0f);
			lastInputAxis.Add (input,0f);			
			
			inputAxisRaw.Add(input,0f);
			lastInputAxisRaw.Add(input,0f);
			
			inputButton.Add (input, false);
			lastInputButton.Add (input, false);
			
			inputButtonUp.Add (input, false);
			lastInputButtonUp.Add (input, false);
			
			inputButtonDown.Add (input, false);
			lastInputButtonDown.Add (input, false);			
			
			/*inputKey.Add (input, false);
			lastInputKey.Add (input, false);	
			
			inputKeyUp.Add (input, false);
			lastInputKeyUp.Add (input, false);		
			
			inputKeyDown.Add (input, false);
			lastInputKeyDown.Add (input, false);*/				
			
			
		}	
		
		if (synchronizeAnimations) {
			
			if (objectContainingAnimations.GetComponent<Animation>() != null)
				objectContainingAnimations.GetComponent<Animation>().cullingType = AnimationCullingType.AlwaysAnimate;
				
			if (objectContainingAnimations.GetComponent<Animator>() != null)
				objectContainingAnimations.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;				
			
		}
			
		//help for newbies (adding default NetworkSerializer mods automatically if none are added manually):
		GameObject goNIT = null;
		GameObject goSyMec = null;
		GameObject goSyLeg = null;
			
		if (addSerializerModsAutomaticallyIfNonePresentAtRuntime) {
			int countNVsMods = 0;
			foreach(uMMO_NetObject_NetworkViewSerializer_Module serializerMod in networkViewSerializerMods) {
				countNVsMods++;	
			}
				
			if (countNVsMods < 1) {
				if (synchronizePosition || synchronizeRotation) {
					goNIT = (GameObject)GameObject.Instantiate(Resources.Load ("modules/NetObject/ModNetObject_NetworkInterpolatedTransform_Generic"));
					networkViewSerializerMods.Add (goNIT.GetComponent<uMMO_NetObject_NetworkViewSerializer_NetworkInterpolatedTransform>());
				}
					
				if (synchronizeAnimations) {
					if (objectContainingAnimations.GetComponent<Animator>() != null) {
						//mecanim:
						goSyMec = (GameObject)GameObject.Instantiate(Resources.Load ("modules/NetObject/ModNetObject_SyncMecanimVars_Generic"));
						networkViewSerializerMods.Add (goSyMec.GetComponent<uMMO_NetObject_NetworkViewSerializer_SyncMecanimVars>());		
					} else if (objectContainingAnimations.GetComponent<Animation>() != null) {
						//legacy:
						goSyLeg = (GameObject)GameObject.Instantiate(Resources.Load ("modules/NetObject/ModNetObject_SyncLegacyAnims_Generic"));
						networkViewSerializerMods.Add (goSyLeg.GetComponent<uMMO_NetObject_NetworkViewSerializer_SyncLegacyAnims>());
						
					}
				}
			}
		}
			
		//Initialize NetworkSerializer mods:
		List<uMMO_NetObject_NetworkViewSerializer_Module> newSerializerMods = new List<uMMO_NetObject_NetworkViewSerializer_Module>();
		foreach(uMMO_NetObject_NetworkViewSerializer_Module serializerMod in networkViewSerializerMods) {
			serializerMod.enabled = false;
			uMMO_NetObject_NetworkViewSerializer_Module mod = (uMMO_NetObject_NetworkViewSerializer_Module) uMMO_NetObject_Module.initialize( serializerMod, this );
			mod.enabled = true;
			newSerializerMods.Add (mod);
		}
		networkViewSerializerMods = newSerializerMods;
			
		//destroy default networkViewSerializerMods
		if (goNIT != null)
			Destroy(goNIT);
		if (goSyMec != null)
			Destroy(goSyMec);
		if (goSyMec != null)
			Destroy(goSyLeg);
			
		//custom module initialization:
		List<uMMO_NetObject_Custom_Module> newCustomMods = new List<uMMO_NetObject_Custom_Module>();
		foreach(uMMO_NetObject_Custom_Module customMod in customModules) {
			customMod.enabled = false;
			uMMO_NetObject_Custom_Module mod = (uMMO_NetObject_Custom_Module) uMMO_NetObject_Module.initialize( customMod, this );
			mod.enabled = true;
			newCustomMods.Add (mod);
		}
		customModules = newCustomMods;			
			
		allModules = gameObject.GetComponentsInChildren<uMMO_NetObject_Module>();
	}
	
	/* RPC dummy */
	[RPC]
    [System.Reflection.Obfuscation]
	public void updateActivityTimestamp(NetworkPlayer np) {
	}
	
	/* update the current activity timestamp to avoid timeout kick. Call from server OR client */
	public void updateActivityTimestamp() {
		
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			uMMO.get.updateActivityTimestamp(nplayer);
		} else {
			uMMO.get.GetComponent<NetworkView>().RPC ("updateActivityTimestamp",RPCMode.Server,nplayer);
		}
	}
	
	/* on authoritative server setup, keystrokes are transmitted to server using this RPC. Call on server only. */
	[RPC]
    [System.Reflection.Obfuscation]
	protected void ReceiveInputFromClient(string inputMode, string inputName, string inputValue) {
		//called on server:
		
		updateActivityTimestamp();
		
		if (inputMode == "inputAxisRaw") {
			inputAxisRaw[inputName]	= float.Parse(inputValue);
		} else if (inputMode == "inputAxis") {
			inputAxis[inputName]	= float.Parse(inputValue);
		} else if (inputMode == "inputButton") {
			inputButton[inputName]	= bool.Parse(inputValue);
		} else if (inputMode == "inputButtonUp") {
			inputButtonUp[inputName]	= bool.Parse(inputValue);
		} else if (inputMode == "inputButtonDown") {
			inputButtonDown[inputName]	= bool.Parse(inputValue);
		} /*else if (inputMode == "inputKey") {
			inputKey[inputName]	= bool.Parse(inputValue);
		} else if (inputMode == "inputKeyUp") {
			inputKeyUp[inputName]	= bool.Parse(inputValue);
		} else if (inputMode == "inputKeyDown") {
			inputKeyDown[inputName]	= bool.Parse(inputValue);
		}*/
	}
	
	/* <-- These functions can be called on server and on client to replace the functions normally used to request values from InputManager (i.e. Input.GetAxis(), Input.GetButton(), ect.) */
	public float GetAxis(string key) {
		if (inputAxis.ContainsKey(key)) {
			return inputAxis[key];
		} else {
			return 0f;
		}
	}
	
	public float GetAxisRaw(string key) {
		if (inputAxisRaw.ContainsKey(key)) {
			return inputAxisRaw[key];
		} else {
			return 0f;	
		}
	}
	
	public bool GetButton(string key) {
		if (inputButton.ContainsKey(key)) {
			return inputButton[key];
		} else {
			return false;
		}
	}
	
	public bool GetButtonUp(string key) {
		if (inputButtonUp.ContainsKey(key)) {
			return inputButtonUp[key];
		} else {
			return false;	
		}
	}
	
	public bool GetButtonDown(string key) {
		if (inputButtonDown.ContainsKey(key)) {
			return inputButtonDown[key];
		} else {
			return false;	
		}
	}	
	
	/*public bool GetKey(string key) {
		if (inputKey.ContainsKey(key)) {
			return inputKey[key];
		} else {
			return false;	
		}
	}	
	
	public bool GetKeyUp(string key) {
		if (inputKeyUp.ContainsKey(key)) {
			return inputKeyUp[key];
		} else {
			return false;	
		}
	}	
	
	
	public bool GetKeyDown(string key) {
		if (inputKeyDown.ContainsKey(key)) {
			return inputKeyDown[key];
		} else {
			return false;	
		}
	}*/	
	
	/* --> */
	
	/* update key strokes and write them into the appropriate container */
	protected void Update() {
		
		if (Network.isClient && isLocalPlayer) {
			
			foreach(string inputName in input2Check) {

				float axis = Input.GetAxis(inputName);
				if (axis != lastInputAxis[inputName]) {
					if (uMMO.get.authoritativeServerSetup)
						GetComponent<NetworkView>().RPC ("ReceiveInputFromClient",RPCMode.Server,"inputAxis",inputName,axis.ToString());
					inputAxis[inputName] = axis;
					lastInputAxis[inputName] = axis;
				}
				if (axis != 0f) {
					isLocalClientGeneratingInput = true;		
				}
				
				float axisRaw = Input.GetAxisRaw(inputName);
				if (axisRaw != lastInputAxisRaw[inputName]) {
					if (uMMO.get.authoritativeServerSetup)
						GetComponent<NetworkView>().RPC ("ReceiveInputFromClient",RPCMode.Server,"inputAxisRaw",inputName,axisRaw.ToString());
					inputAxisRaw[inputName] = axisRaw;
					lastInputAxisRaw[inputName] = axisRaw;
				}
				if (axisRaw != 0f) {
					isLocalClientGeneratingInput = true;		
				}					
				
				bool button = Input.GetButton(inputName);
				if (button != lastInputButton[inputName]) {
					if (uMMO.get.authoritativeServerSetup)
						GetComponent<NetworkView>().RPC ("ReceiveInputFromClient",RPCMode.Server,"inputButton",inputName,button.ToString());
					inputButton[inputName] = button;
					lastInputButton[inputName] = button;
				}	
				if (button != false) {
					isLocalClientGeneratingInput = true;		
				}					
				
				bool buttonUp = Input.GetButtonUp(inputName);
				if (buttonUp != lastInputButtonUp[inputName]) {
					if (uMMO.get.authoritativeServerSetup)
						GetComponent<NetworkView>().RPC ("ReceiveInputFromClient",RPCMode.Server,"inputButtonUp",inputName,buttonUp.ToString());
					inputButtonUp[inputName] = buttonUp;
					lastInputButtonUp[inputName] = buttonUp;
				}
				if (buttonUp != false) {
					isLocalClientGeneratingInput = true;		
				}					
				
				bool buttonDown = Input.GetButtonDown(inputName);
				if (buttonDown != lastInputButtonDown[inputName]) {
					if (uMMO.get.authoritativeServerSetup)
						GetComponent<NetworkView>().RPC ("ReceiveInputFromClient",RPCMode.Server,"inputButtonDown",inputName,buttonDown.ToString());
					inputButtonDown[inputName] = buttonDown;
					lastInputButtonDown[inputName] = buttonDown;
				}	
				if (buttonDown != false) {
					isLocalClientGeneratingInput = true;		
				}					
				
				/*bool key = Input.GetKey(inputName);
				if (key != lastInputKey[inputName]) {
					if (uMMO.singleton.authoritativeServerSetup)
						networkView.RPC ("ReceiveInputFromClient",RPCMode.Server,"inputKey",inputName,key.ToString());
					inputKey[inputName] = key;
					lastInputKey[inputName] = key;
				}
				
				bool keyUp = Input.GetKeyUp(inputName);
				if (keyUp != lastInputKeyUp[inputName]) {
					if (uMMO.singleton.authoritativeServerSetup)
						networkView.RPC ("ReceiveInputFromClient",RPCMode.Server,"inputKeyUp",inputName,keyUp.ToString());
					inputKeyUp[inputName] = keyUp;
					lastInputKeyUp[inputName] = keyUp;
				}	
				
				bool keyDown = Input.GetKeyDown(inputName);
				if (keyDown != lastInputKeyDown[inputName]) {
					if (uMMO.singleton.authoritativeServerSetup)
						networkView.RPC ("ReceiveInputFromClient",RPCMode.Server,"inputKeyDown",inputName,keyDown.ToString());
					inputKeyDown[inputName] = keyDown;
					lastInputKeyDown[inputName] = keyDown;
				}	*/			
				
			}
		}
	}	
	
	/* cheating/nagging protection on authoritative server setup: client must not instantiate anything */
	protected void OnNetworkInstantiate(NetworkMessageInfo info) {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server && uMMO.get.authoritativeServerSetup) {
			if (info.sender != Network.player && int.Parse(info.sender+"") != -1) {
				//print ("info.sender: "+info.sender);
				uMMO.get.removePlayer(info.sender);	
					
				/*Network.DestroyPlayerObjects(info.sender);
				Network.RemoveRPCs(info.sender);
				Network.RemoveRPCs(info.sender, int.Parse(info.sender+""));
				Network.CloseConnection(info.sender,true);*/
			}
		}
	}
	
	/* main uMMO feature, read up on it in in-Editor documentation */
	protected void handleListAndRemoveUnneccessary(List<Component> list2process) {			

		List<Component> componentsToKill = new List<Component>();
		foreach(List<Component> currList in scriptLists) {
			if (currList != list2process) {		
				
				foreach(Component otherScript in currList) {
					if (otherScript != null) {
						string scriptName = uMMO_StaticLibrary.ObjectDebugName2ScriptName(otherScript+"");
	
							if (list2process.IndexOf(otherScript) == -1) {

							if (otherScript.GetType() != typeof(Transform))
								componentsToKill.Add (otherScript);
							else 
								Destroy(otherScript.gameObject);
							
						}
					}
				}	
			}
		}

		uMMO_StaticLibrary.killRequiredTaggedScripts(componentsToKill);
	
		
	}

	/* main uMMO feature, read up on it in in-Editor documentation */
	protected void handleListAndRemoveUnneccessary(List<string> list2process) {
		
		List<Component> componentsToKill = new List<Component>();
			foreach(List<string> currList in runtimeScriptLists) {
			if (currList != list2process) {		
				
				foreach(string otherScriptName in currList) {

					Component otherScript = uMMO_StaticLibrary.findComponentInChildren(otherScriptName,this.gameObject); 

					//print ("script2kill: "+otherScript.name);
					if (otherScript != null && list2process.IndexOf(otherScript.GetType().ToString()) == -1 ) {

						if (otherScript.GetType() != typeof(Transform))
							componentsToKill.Add (otherScript);
						else 
							Destroy(otherScript.gameObject);
					}
				}	
			}
		}
		
		uMMO_StaticLibrary.killRequiredTaggedScripts(componentsToKill);
		
		
	}
	
	/* switches off main scene camera, if local player camera was set */
	protected void switchOffMainSceneCamera() {
		Camera[] cameras = UnityEngine.Object.FindObjectsOfType(typeof(Camera)) as Camera[];
		
		foreach(Camera cam in cameras) {
			if (cam != cameraToActivateOnLocalPlayer)
				cam.enabled = false;
		}
		
	}
	
	/* decides if custom callback/event function call should be executed. Switch this on in the Editor */
	protected void sendCallbackToGO(string functionName, object parameter) {
		if (activateCallbackFunctions && callbackFunctionsToExecute.Contains(functionName)) {
			gameObject.SendMessage(functionName,SendMessageOptions.DontRequireReceiver);	
				
			foreach(uMMO_NetObject_Module mod in allModules) {
				mod.gameObject.SendMessage(functionName,SendMessageOptions.DontRequireReceiver);			
			}
		}
	}	
	
	/* central function to start uMMO network activity on this instance. Always call this function if you instantiated a new uMMO_NetObject. if this instance is NPC/NPO, provide NetworkPlayer of server (0) as parameter */
	[RPC]
    [System.Reflection.Obfuscation]
	public void setOwnerAndStart(NetworkPlayer nPlayerOwner) {
		
		nplayer = nPlayerOwner;
		
		isLocalPlayer = (nPlayerOwner == Network.player && objectType == uMMO_ObjectType.Player && Network.isClient )?true:false;
			
		if (isLocalPlayer) {
			LOCAL_PLAYER = this;		
		}
		//add and remove scripts depending on situation
		
		if (handelScriptsBasedOnSituation || handelScriptsBasedOnRuntimeSituation || activateCallbackFunctions || uMMO.get.kickPlayerOnTimeout) { 
			if (objectType == uMMO_ObjectType.Player) {
				
				if (Network.isClient) {
					if (isLocalPlayer) {
						
						if (cameraToActivateOnLocalPlayer != null) {
							switchOffMainSceneCamera();
							cameraToActivateOnLocalPlayer.enabled = true;
						}
						
						if (handelScriptsBasedOnSituation)
							handleListAndRemoveUnneccessary(clientLocalPlayerScripts);
						if (handelScriptsBasedOnRuntimeSituation)
							handleListAndRemoveUnneccessary(clientLocalPlayerRuntimeScripts);
						
						if (activateCallbackFunctions)
							sendCallbackToGO("__uMMO_localPlayer_init",null);
						
					} else {
						
						if (handelScriptsBasedOnSituation)
							handleListAndRemoveUnneccessary(clientRemotePlayerScripts);
						if (handelScriptsBasedOnRuntimeSituation)
							handleListAndRemoveUnneccessary(clientRemotePlayerRuntimeScripts);
											
						if (activateCallbackFunctions)
							sendCallbackToGO("__uMMO_remotePlayer_init",null);
					}
					
					
				} else if (Network.isServer) {

					if (handelScriptsBasedOnSituation)
						handleListAndRemoveUnneccessary(serverPlayerScripts);
					if (handelScriptsBasedOnRuntimeSituation)
						handleListAndRemoveUnneccessary(serverPlayerRuntimeScripts);
					
					if (activateCallbackFunctions)
						sendCallbackToGO("__uMMO_serverPlayer_init",null);
					
					updateActivityTimestamp();
	
				}
			} else if (objectType == uMMO_ObjectType.NonPlayerObject) {
				
				if (Network.isClient) {				
					if (handelScriptsBasedOnSituation)
						handleListAndRemoveUnneccessary(clientNPOScripts);
					if (handelScriptsBasedOnRuntimeSituation)
						handleListAndRemoveUnneccessary(clientNPORuntimeScripts);
						
					if (activateCallbackFunctions)
						sendCallbackToGO("__uMMO_clientNPO_init",null);
					
				} else if (Network.isServer) {
					if (handelScriptsBasedOnSituation)					
						handleListAndRemoveUnneccessary(serverNPOScripts);		
					if (handelScriptsBasedOnRuntimeSituation)
						handleListAndRemoveUnneccessary(serverNPORuntimeScripts);
						
					if (activateCallbackFunctions)
						sendCallbackToGO("__uMMO_serverNPO_init",null);
				}
				
			} else {
				throw new Exception("uMMO_Character "+this.ToString()+" char type was not set.");	
			}
		}
		
		if (Network.isServer) {
			
			//add nvs2np to server list
			uMMO.get.registerNVs2NP(gameObject.GetComponents<NetworkView>(),nPlayerOwner);
			
		}
		
	}
		
	protected void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{	
		if (stream.isWriting) {
			foreach(uMMO_NetObject_NetworkViewSerializer_Module serializerMod in networkViewSerializerMods) {
				serializerMod.onWriteToNetworkView(stream,info);		
			}
		} else {
			foreach(uMMO_NetObject_NetworkViewSerializer_Module serializerMod in networkViewSerializerMods) {
				serializerMod.onReadFromNetworkView(stream,info);		
			}				
		}
			
	}
	

}