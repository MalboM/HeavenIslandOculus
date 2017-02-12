using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
[RequireComponent (typeof (uMMO_NetObject))]
public class Attacker : MonoBehaviour {

	
	void attack1() {
		
		if (Marker.marked != null) {
			
			uMMO_NetObject victim = Marker.marked;
			uMMO_NetObject attacker = GetComponent<uMMO_NetObject>();
			
			transform.LookAt(victim.transform);
			
			if(GetComponent<ThirdPersonSimpleAnimation_TEST>() != null)
				GetComponent<ThirdPersonSimpleAnimation_TEST>().DidPunch();
			
			victim.GetComponent<NetworkView>().RPC ("sufferAttack",RPCMode.Others,Network.player,0);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			
			attack1();
		}
	}
}
