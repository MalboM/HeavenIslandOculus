using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO module class. Read in-Editor documentation for more info. Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public class uMMO_Main_SpawningMethod_DirectlyOnEstablishedConnectionSimpleDisconnect : uMMO_Main_SpawningMethod_Module {
	
	public float spawnPosTolerance;
	public bool cheatTest1 = false;
	
	void OnGUI() {
		
		if (cheatTest1) {
			if (GUI.Button(new Rect(10,500,200,100),"CHEAT TEST: Network.Instantiate")) {
				//Network.Instantiate(uMMO.get.playerCharOnAuthoritativeSetup,Vector3.zero,Quaternion.identity,0);	
				instantiateUMMONetObject(Network.player,uMMO.get.playerCharOnAuthoritativeSetup); //"CHEATING" TEST
			}		
		}
	}	
		
	protected override void OnPlayerConnected ( NetworkPlayer newPlayer  ){		
		//A player connected to me(the server)! 
		
		if (uMMO.get.authoritativeServerSetup)
			instantiateUMMONetObject(newPlayer,uMMO.get.playerCharOnAuthoritativeSetup);
	}	
	
	protected override void  OnConnectedToServer (  ){
		//I connected to a server, Network.player is ME
		
		if (!uMMO.get.authoritativeServerSetup)
			instantiateUMMONetObject(Network.player,uMMO.get.playerCharOnNonAuthoritativeSetup);
		//instantiateUMMONetObject(Network.player,uMMO.get.playerCharOnAuthoritativeSetup); //"CHEATING" TEST
	}	
	
	protected override void OnPlayerDisconnected ( NetworkPlayer player  ){
		//if (uMMO.get.players2ts_lastActivity.ContainsKey(player))
			uMMO.get.removePlayer(player);
	}	
	
	protected override void OnDisconnectedFromServer ( NetworkDisconnection info  ){
		//Debug.Log("Simple reset of the scene to avoid having to clean up earlier networkobjects");
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Client) {
			Application.LoadLevel(Application.loadedLevel);
		}

	}	
	
	public static uMMO_NetObject instantiateUMMONetObject(NetworkPlayer newPlayer, uMMO_NetObject obj2Instantiate, Vector3 spawnPosition, Quaternion spawnRotation) {
		int playerNumber = int.Parse(newPlayer+"");

        spawnPosition = uMMO.get.transform.position;
		
		GameObject newGO = (GameObject)Network.Instantiate(obj2Instantiate.gameObject, spawnPosition, spawnRotation, playerNumber);

		newGO.GetComponent<NetworkView>().RPC ("setOwnerAndStart",RPCMode.AllBuffered, newPlayer);
		
		return newGO.GetComponent<uMMO_NetObject>();
	}	
	
	public override uMMO_NetObject instantiateUMMONetObject(NetworkPlayer newPlayer, uMMO_NetObject obj2Instantiate) {
		return instantiateUMMONetObject(newPlayer, obj2Instantiate, transform.position, transform.rotation);
	}	
}
