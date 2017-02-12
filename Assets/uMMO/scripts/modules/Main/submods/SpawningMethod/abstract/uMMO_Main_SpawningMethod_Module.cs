using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO module class. Read in-Editor documentation for more info. Changes can have significant consequences for performance and overall functionality.
 * You may only use and change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public abstract class uMMO_Main_SpawningMethod_Module : uMMO_Main_Module {
	
	public int maxConnections = 500;
	
	public abstract uMMO_NetObject instantiateUMMONetObject(NetworkPlayer newPlayer, uMMO_NetObject obj2Instantiate);
	
	protected abstract void OnPlayerConnected(NetworkPlayer newPlayer);
	
	protected abstract void OnConnectedToServer();
	
	protected abstract void OnPlayerDisconnected(NetworkPlayer player );
	
	protected abstract void OnDisconnectedFromServer(NetworkDisconnection info);
}
