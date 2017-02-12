using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class NPCSpawner : MonoBehaviour {
	
	private int NPCcount = 0;
	public uMMO_NetObject NPC2Spawn;

	// Use this for initialization
	void Start () {
		
		StartCoroutine(SpawnNPC(3f));
	}
	
	IEnumerator SpawnNPC(float secs) {
		yield return new WaitForSeconds(secs);
		
		if (Network.isServer) {
			
			
			if (NPCcount < 20) {
				uMMO_Main_SpawningMethod_DirectlyOnEstablishedConnectionSimpleDisconnect.instantiateUMMONetObject(Network.player,NPC2Spawn,transform.position,transform.rotation);
				
				NPCcount++;
			}
			
			StartCoroutine(SpawnNPC(25f));
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
