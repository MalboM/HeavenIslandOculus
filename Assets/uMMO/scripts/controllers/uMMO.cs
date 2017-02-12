using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class handles all global settings on server AND client side. Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
[RequireComponent (typeof (NetworkView))]
#if !IS_UNLICENSED
public class uMMO : MonoBehaviour {
#else
public sealed class uMMO : MonoBehaviour {
#endif
	
	
	/*<-- read up on the following attributes on in-Editor documentation: */
	//####
	public bool showDocumentationInEditor;
	
	public bool initializeSecurity;
	
	public bool authoritativeServerSetup;
	
	public uMMO_NetObject playerCharOnAuthoritativeSetup;
	
	public uMMO_NetObject playerCharOnNonAuthoritativeSetup;
	
	public uMMO_Architecture architectureToCompile;
	
		/* uMMO modules:*/
	public uMMO_Main_SpawningMethod_Module spawningMethod;
	
	public uMMO_Main_DataTransmissionFilter_Module dataTransmissionFilter;
	
	public uMMO_Main_ConnectionGUI_Module connectionGUI;
		
	public List<uMMO_Main_Custom_Module> customModules;
	
	public bool kickPlayerOnTimeout;
	
	public float secondsToCheckForTimeouts = 5f;
	
	public float secondsUntilDisconnectDueToTimeout = 300f;
	
	public bool showDebugHints;
	/* #### -->*/
	
	/*
	 * this map/dictionary maps NetworkViews to NetworkPlayers, it's important to know which NetworkPlayer is associated with which character/object/networkView. 
	 * Don't fill up yourself, use spawningMethod.instantiateUMMONetObject instead.
	 */
	public Dictionary<NetworkView,NetworkPlayer> nv2np = new Dictionary<NetworkView,NetworkPlayer>();
	
	/*
	 * this map/dictionary maps a NetworkView/NetworkPlayer identification string to a boolean which determines, if data transmission should take place between the corresponding NetworkView and NetworkPlayer. 
	 * Don't fill up yourself, use the dataTransmissionFilter module functionality to update this.
	 */
	public Dictionary<string, bool> nv2np2tx = new Dictionary<string, bool>();
	
	/* this map/dictionary maps NetworkPlayers to timestamps, so that the time of the last action can be checked. 
	 * Don't fill up yourself, use the timeout functionality of this class instead.
	 */
	public Dictionary<NetworkPlayer, double> players2ts_lastActivity = new Dictionary<NetworkPlayer, double>();
	/* a string seperator used for nv2np2tx dictionary */
	public const char STR_VAL_SEPERATOR = '|';
	/* a boolean marking the value for initial data transmission can and will be overwritten shortly after player connection if you use dataTransmissionFilter */
	public const bool INITAL_DATA_TRANSMISSION_ALLOWED = true; //false: untested! not recommended at this point
	/* a thread security lock */
	private readonly object syncFilterUpdateLock = new object();
	/* the single instance of this class */
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 static uMMO singleton;
	/* Some buffer seconds to start the server? */
#if !IS_UNLICENSED
	protected
#else
	private
#endif
		 float secondsToStartServer = 1.5f;
	
	/* initialization of important attributes, instantiation of modules */
	protected void  Awake (){
		
		Application.runInBackground = true;			
		
		if (architectureToCompile == uMMO_Architecture.TEST_UnityEditorIsServer_OthersAreClients) {
			if (Application.isEditor) {
				architectureToCompile = uMMO_Architecture.Server;	
			} else {
				architectureToCompile = uMMO_Architecture.Client;			
			}
		} else if (architectureToCompile == uMMO_Architecture.TEST_UnityEditorIsClient_OtherIsServer) {
			if (Application.isEditor) {
				architectureToCompile = uMMO_Architecture.Client;
			} else {
				architectureToCompile = uMMO_Architecture.Server;			
			}
			
		}
		
		GetComponent<NetworkView>().observed = null;
		GetComponent<NetworkView>().stateSynchronization = NetworkStateSynchronization.Off;		
			
		//module initialization
		dataTransmissionFilter = (uMMO_Main_DataTransmissionFilter_Module) uMMO_Main_Module.initialize( dataTransmissionFilter );	
		spawningMethod = (uMMO_Main_SpawningMethod_Module) uMMO_Main_Module.initialize( spawningMethod );
		connectionGUI = (uMMO_Main_ConnectionGUI_Module) uMMO_Main_Module.initialize( connectionGUI );	
			
		//custom module initialization:
		List<uMMO_Main_Custom_Module> newCustomMods = new List<uMMO_Main_Custom_Module>();
		foreach(uMMO_Main_Custom_Module customMod in customModules) {
			customMod.enabled = false;
			uMMO_Main_Custom_Module mod = (uMMO_Main_Custom_Module) uMMO_Main_Module.initialize( customMod );
			mod.enabled = true;
			newCustomMods.Add (mod);
		}
		customModules = newCustomMods;
			
#if uMMO
		print ("uMMO preprocessor directive working");	//why is it not working? define-scripts in Assets/uMMO/misc/defines/	
#endif 
		
	}
	
	/* start of the timeout check timer */
	protected void Start() {		
		
		if (architectureToCompile == uMMO_Architecture.Server) {						
			
			StartCoroutine(startServer());
		}	
	}
	
	protected IEnumerator startServer() {
		yield return new WaitForSeconds(secondsToStartServer);
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
				
			Network.useNat = false;
			if (uMMO.get.initializeSecurity)
				Network.InitializeSecurity();
			
			Network.InitializeServer(spawningMethod.maxConnections, connectionGUI.connectPort);
			
			if (kickPlayerOnTimeout && secondsUntilDisconnectDueToTimeout > 0f)
				StartCoroutine( checkTimeout() );	
		}			
	}	
	
	/* use this to instantly remove a whole networkplayer and all his networkviews and corresponding GameObjects */
	public void removePlayer(NetworkPlayer player) {
		//Debug.Log("Clean up after player " + player);	
		
		if (player != Network.player) { //assuming this only gets executed on the server, this line avoids the server somehow kicking itself
			
			players2ts_lastActivity.Remove(player);
				
			List<uMMO_NetObject> chs = uMMO_StaticLibrary.getNetObjectsByNetworkPlayer(player);
				
			foreach(uMMO_NetObject ch in chs) {
			
				removeNetObject(ch);
			}
				
			int playerNumber = int.Parse(player+"");
			
			//if (playerNumber != 0) {
				Network.RemoveRPCs(player, playerNumber);	
				Network.RemoveRPCs(player, 0);
				Network.RemoveRPCs(player);
				Network.DestroyPlayerObjects(player);	
				
				List<string> keys2Remove = new List<string>();
				foreach(var entry in nv2np2tx) {
					string[] parts = entry.Key.Split(STR_VAL_SEPERATOR);
					
					if (parts[1].Equals(player+"")) {
						keys2Remove.Add(entry.Key);
					}
				}
				
				foreach(string k in keys2Remove) {
					nv2np2tx.Remove(k);
				}
			//}	
		
			
			Network.CloseConnection(player,true);
		} else {
			if (uMMO.get.showDebugHints)
				Debug.Log("uMMO hint: You tried to remove/disconnect yourself (this player). This is never a good idea using this function, not when you're server, nor when you're client. To disconnect from the network simply call Network.Disconnect()");
		}
	}
	
	/* use this to remove a whole GameObject (represented by the component uMMO_NetObject). Note: this does not necessarily remove the corresponding player from the network */
	public void removeNetObject(uMMO_NetObject obj2remove) {
		
		GameObject go = obj2remove.gameObject;
		
		NetworkView[] nvs = (NetworkView[])go.GetComponentsInChildren<NetworkView>();
		
		foreach(NetworkView nv in nvs) {
			Network.RemoveRPCs(nv.viewID);
			nv2np.Remove(nv);
			
			List<string> keys2Remove = new List<string>();
			foreach(var entry in nv2np2tx) {
				string[] parts = entry.Key.Split(STR_VAL_SEPERATOR);
				
				if (parts[0].Equals(nv.viewID+"")) {
					keys2Remove.Add(entry.Key);
				}
			}
			
			foreach(string k in keys2Remove) {
				nv2np2tx.Remove(k);
			}			
			
		}		
		Network.Destroy(go);
	}
	
	/* check all networkplayers for violation of idle time maximum. Don't call yourself. */
	protected IEnumerator checkTimeout() {
		yield return new WaitForSeconds(secondsToCheckForTimeouts);
		
		List<NetworkPlayer> nps2remove = new List<NetworkPlayer>();
		foreach(var entry in players2ts_lastActivity) {
			double lastActivity = (double)entry.Value;
			if (lastActivity < uMMO_StaticLibrary.ts_now() - secondsUntilDisconnectDueToTimeout) {
				nps2remove.Add(entry.Key);
			}
		}
		
		foreach(NetworkPlayer np in nps2remove) {
			removePlayer(np);		
		}
		
		StartCoroutine(checkTimeout());
	}
	
	/* use the module dataTransmissionFilter to decide which NetworkView can send to which NetworkPlayer. Called in class uMMO_DataTransmissionFilter_Module. Don't call yourself. */
	public void setDataTransmissionLimitations() {
		
		lock(syncFilterUpdateLock) {
		
			foreach(var entry in nv2np) {
					
				NetworkView nv1 = entry.Key;
				NetworkPlayer np1 = entry.Value;
					
				foreach(var entry2 in nv2np) {	
						
					NetworkView nv2 = entry2.Key;
					NetworkPlayer np2 = entry2.Value;	
					
					// if np1 and np2 are not the same player and np2 is not an NPC
					if (np2 != np1 && np2 != Network.player) {
									
						bool transmit = INITAL_DATA_TRANSMISSION_ALLOWED; //standard configuration is everybody transmits to everybody (change on your own risk)
	
						string nv2npKey = nv1.viewID+(STR_VAL_SEPERATOR.ToString())+np2;
						if (!nv2np2tx.ContainsKey(nv2npKey))
							nv2np2tx.Add(nv2npKey,transmit);
						
						//Condition(s) whether not to transmit between np1 and np2
					
						uMMO_Main_DataTransmissionFilter_Module[] filters = dataTransmissionFilter.GetComponents<uMMO_Main_DataTransmissionFilter_Module>();
						
						foreach(uMMO_Main_DataTransmissionFilter_Module filter in filters) {
							if (transmit) {
								if (!filter.dataShouldBeTransmittedBetween(nv1,nv2)) {
									transmit = false;
									break;
								}
							}
						}
					
						//notification needed?
						if (transmit && !nv2np2tx[nv2npKey]) {
							
							if (np2 != Network.player) {
								//print ("notify "+np2+" receiving transmissions from "+nv1.viewID);
								GetComponent<NetworkView>().RPC("notifyRecvTransmissionsFromNV",np2,nv1.viewID);
							}
						} else if (!transmit && nv2np2tx[nv2npKey]) {
							
							if (np2 != Network.player) {
								//print ("notify "+np1+" NOT receiving transmissions from "+nv1.viewID);
								GetComponent<NetworkView>().RPC("notifyRecvNoTransmissionsFromNV",np2,nv1.viewID);
							}
						}
						
						
						nv1.SetScope(np2,transmit);
							
						nv2np2tx[nv2npKey] = transmit;
						
					}
				
				}
			}
		}
	}	
	
	/* fills nv2np attribute. Don't call yourself. */
	public void registerNVs2NP(NetworkView[] nvs,NetworkPlayer np) {
		foreach(NetworkView nv in nvs) {
			nv2np.Add(nv,np);
		}	
	}	
	
	/* a client getting notified that he does not receive any syncronization transmission from a certain networkView, so its renderer can be switched off. Don't call yourself. */
	[RPC]
    [System.Reflection.Obfuscation]
	protected void notifyRecvNoTransmissionsFromNV(NetworkViewID nvID) {
		//print ("NOT transmitting to "+getCharacterByNetworkPlayer(np).gameObject);
		uMMO_StaticLibrary.switchNetObjectVisibility(uMMO_StaticLibrary.getNetObjectByNViewID(nvID),false);
	}
	
	/* a client getting notified that he does receive syncronization transmissions from a certain networkView, it's renderer should be switched on. Don't call yourself. */
	[RPC]
    [System.Reflection.Obfuscation]
	protected void notifyRecvTransmissionsFromNV(NetworkViewID nvID) {
		//print ("Transmitting to "+getCharacterByNetworkPlayer(np).gameObject);
		uMMO_StaticLibrary.switchNetObjectVisibility(uMMO_StaticLibrary.getNetObjectByNViewID(nvID),true);
	}	
	
	/* updates the timestamp of a certain NetworkPlayer to the current server timestamp, to avoid timeout kick.
	 * Don't call this directly, and if you do, only do it if you are server. But its best to call uMMO_NetObject.updateActivityTimestamp() instead.
	 */
	[RPC]
    [System.Reflection.Obfuscation]
	public void updateActivityTimestamp(NetworkPlayer np) {
		
		double now = uMMO_StaticLibrary.ts_now();
		
		if (uMMO.get.players2ts_lastActivity.ContainsKey(np) && np != Network.player) {
			uMMO.get.players2ts_lastActivity[np] = now;
		} else {
			uMMO.get.players2ts_lastActivity.Add(np,now);	
		}
	}	
	
	/* using Unity callback to initialize NPCs which were added at design time */
	protected void OnServerInitialized() {
		uMMO_NetObject[] chars = uMMO_StaticLibrary.getAllNetObjects();
		
		foreach(uMMO_NetObject c in chars) {
			if (c.objectType == uMMO_ObjectType.NonPlayerObject) {
				
				if (!nv2np.ContainsKey(c.GetComponent<NetworkView>()))
					c.GetComponent<NetworkView>().RPC ("setOwnerAndStart",RPCMode.AllBuffered, Network.player);	
			}
		}			
	}
		
#if IS_UNLICENSED
	private bool showFullVersionLicensingInfo = true;
		
	private bool showingStoppedServerMsg = false;
	private const float LICENSE_TIMER_VALUE = 60f*60f*12f; //12 hours
	//private const float LICENSE_TIMER_VALUE = 10f; //test: 10 seconds
	
	private float licenseTimer  = LICENSE_TIMER_VALUE;
		
	private void Update() {

		if (architectureToCompile == uMMO_Architecture.Server) {
		 	if(licenseTimer > 0f){
		  		licenseTimer -= Time.deltaTime;

		 	} else {					
				disconnectFromNetwork(0);					
				licenseTimer = LICENSE_TIMER_VALUE;
				StartCoroutine(showWatermark());					
			}
		}
		
	}
		
	private IEnumerator showWatermark() {
		showingStoppedServerMsg = true;
		yield return new WaitForSeconds(5f);
		showingStoppedServerMsg = false;	
	}
		
	private void OnGUI() {
			
		GUIStyle style = GUI.skin.GetStyle("box");
        style.fontStyle = FontStyle.Bold;			
			
		if (showFullVersionLicensingInfo) {
		
		    if (showingStoppedServerMsg)
	        {
	            GUI.Box(new Rect(Screen.width / 2 - (400 / 2), Screen.height / 2 - (40 / 2), 400, 40), "uMMO LIMITED VERSION - stopped server after 12 hours", style);
			}
					
			//GUI.Box(new Rect(0, Screen.height - 50, Screen.width, 25), "uMMO LIMITED VERSION - Hide this watermark by disabling \"Show licensing info\" on the uMMO-prefab.");
			
            if(GUI.Button(new Rect(0, Screen.height - 25, Screen.width, 30), "uMMO LIMITED VERSION - Click here for full unsealed source code, no watermarks and unlimited server running time.")) {
			    Application.OpenURL("https://www.assetstore.unity3d.com/#/content/13867");	
			}
            
        }
	}
#endif
		
	protected void OnApplicationQuit() {
		disconnectFromNetwork(200);		
	}
	
	public void disconnectFromNetwork(int timeout) {
		
		if (architectureToCompile == uMMO_Architecture.Server) {
			List<NetworkPlayer> players2remove = new List<NetworkPlayer>();
			foreach(var entry in players2ts_lastActivity) {
				NetworkPlayer np = entry.Key;
				
				players2remove.Add(np);
			}
				
			foreach(NetworkPlayer np in players2remove) {
				removePlayer(np);		
			}
		}
	
		
		Network.Disconnect(timeout);
	}
	
	/* public getter to be able to call the singleton instance from the outside */
    public static uMMO get
    {
        get
        {
            if (singleton == null)
            {
				GameObject uMMO_c = GameObject.Find("uMMO");
                singleton = uMMO_c.GetComponent(typeof(uMMO)) as uMMO;
            }
            return singleton;
        }
    }		
}
