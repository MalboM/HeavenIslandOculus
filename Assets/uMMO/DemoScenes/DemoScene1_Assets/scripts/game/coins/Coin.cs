using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class Coin : MonoBehaviour {
	
	public float speed;
	
	// Use this for initialization
	void Start () {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			
			GetComponent<Collider>().enabled = true;	
		} else {
			
			GetComponent<Collider>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		transform.localEulerAngles= new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y+speed,transform.localEulerAngles.z);
		
		if (uMMO_NetObject.LOCAL_PLAYER != null) {
			
			if (uMMO_NetObject.LOCAL_PLAYER.GetComponent<Player>().amountCoins > RaceEvent.maxAmountCoinsToCollect-1) {
				
				GetComponent<Renderer>().enabled = false;
				
			} else {
				
				GetComponent<Renderer>().enabled = true;
				
			}
		}
	}
}
