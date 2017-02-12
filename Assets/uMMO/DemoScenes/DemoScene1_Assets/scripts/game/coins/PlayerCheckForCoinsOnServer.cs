using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class PlayerCheckForCoinsOnServer : MonoBehaviour {
	
	public float distanceToPickUpCoin;
	public int amountCoins = 0;
	public int maxAmountCoinsToPick;
	public NetworkPlayer np;
	
	[RPC]
	public void UpdateAmountCoins(int newAmount) {
		//dummy, must be available so that RPC to a client with a script having a real implementation of that function is being sent.
	}	
	
	[RPC]
	public void AnnounceWinner(int newAmount) {
		//dummy, to be able to receive the same event on the client, it has to exist on the server aswell
		
	}	
	
	void OnCollisionEnter(Collision collision) {
		
		foreach (ContactPoint c in collision.contacts) { //for all contactpoints..
			//GameObject thisGO = c.thisCollider.gameObject; //player
			GameObject otherGO = c.otherCollider.gameObject; //could be coin
			
			if (otherGO.GetComponent<Coin>() != null) {
				//otherGO is a coin
				if (amountCoins < RaceEvent.maxAmountCoinsToCollect) {
					amountCoins++;
					GetComponent<NetworkView>().RPC ("UpdateAmountCoins",RPCMode.OthersBuffered,amountCoins);
				
					GameObject goCS = GameObject.Find("CoinSpawner");
					StartCoroutine(goCS.GetComponent<CoinSpawner>().startTimerToSpawnCoin());
					
					uMMO.get.removeNetObject(otherGO.GetComponent<uMMO_NetObject>());
					break;
				}
			}
			
		}
		
	}
}
