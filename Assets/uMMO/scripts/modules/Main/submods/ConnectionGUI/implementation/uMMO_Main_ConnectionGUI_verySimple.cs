// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class uMMO_Main_ConnectionGUI_verySimple : uMMO_Main_ConnectionGUI_Module {
/* 
*  Parts of this file are part of the Unity networking tutorial by M2H (http://www.M2H.nl)
*  The original author of this code Mike Hergaarden, even though some small parts 
*  are copied from the Unity tutorials/manuals.
*  Feel free to use this code for your own projects, drop me a line if you made something exciting! 
*/

void Start()
{
    StartCoroutine(connectAutomatically());
}

public IEnumerator connectAutomatically()
{
    yield return new WaitForSeconds(.02f);
    Network.useNat = false;
    Network.Connect(connectToIP, connectPort);
}
	
public bool showIPandPort = true;	
	
	//Obviously the GUI is for both client(s) & server
	protected override void OnGUI (){
			
		if (Network.peerType == NetworkPeerType.Disconnected){
			//We are currently disconnected: Not a client or host
			GUILayout.Label("Connection status: Disconnected");
			
			if (uMMO.get.architectureToCompile == uMMO_Architecture.Client) {
				/*if (showIPandPort) {
					connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
					connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));
				}
				
				GUILayout.BeginVertical();
				if (GUILayout.Button ("Connect as client"))
				{
					//Connect to the "connectToIP" and "connectPort" as entered via the GUI
					Network.useNat = false;
					Network.Connect(connectToIP, connectPort);
				}
				GUILayout.EndVertical();*/
			}		
			
		}else{
			//We've got (a) connection(s)!
			
			if (Network.peerType == NetworkPeerType.Connecting){
			
				//GUILayout.Label("Connection status: Connecting");
				
			} else if (Network.peerType == NetworkPeerType.Client){
				
				/*GUILayout.Label("Connection status: Client!");
				GUILayout.Label("Ping to server: "+Network.GetAveragePing(  Network.connections[0] ) );		*/
				
			} else if (Network.peerType == NetworkPeerType.Server){
				
				GUILayout.Label("Connection status: Server!");
				GUILayout.Label("Connections: "+Network.connections.Length);
				if(Network.connections.Length>=1){
					GUILayout.Label("Ping to first player: "+Network.GetAveragePing(  Network.connections[0] ) );
				}			
			}
	
			/*if (GUILayout.Button ("Disconnect"))
			{
				uMMO.get.disconnectFromNetwork(200);
			}*/
		}
		
	
	}

	protected void  OnConnectedToServer (){
		if (uMMO.get.showDebugHints)
			Debug.Log("This CLIENT has connected to a server");	
	}
	
	protected void  OnDisconnectedFromServer ( NetworkDisconnection info  ){
		if (uMMO.get.showDebugHints)
			Debug.Log("This SERVER OR CLIENT has disconnected from a server");
	}
	
	protected void  OnFailedToConnect ( NetworkConnectionError error  ){
		if (uMMO.get.showDebugHints)
			Debug.Log("Could not connect to server: "+ error);
	}
	
	protected void  OnPlayerConnected ( NetworkPlayer player  ){
		if (uMMO.get.showDebugHints)
			Debug.Log("Player connected from: " + player.ipAddress +":" + player.port);
	}
	
	protected void  OnServerInitialized (){
		if (uMMO.get.showDebugHints)
			Debug.Log("Server initialized and ready");
	}
	
	protected void  OnPlayerDisconnected ( NetworkPlayer player  ){
		if (uMMO.get.showDebugHints)
			Debug.Log("Player disconnected from: " + player.ipAddress+":" + player.port);
	}
	
	protected void  OnFailedToConnectToMasterServer ( NetworkConnectionError info  ){
		if (uMMO.get.showDebugHints)
			Debug.Log("Could not connect to master server: "+ info);
	}
	
	protected void  OnNetworkInstantiate ( NetworkMessageInfo info  ){
		if (uMMO.get.showDebugHints)
			Debug.Log("New object instantiated by " + info.sender);
	}


}