using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class CoinSpawner : MonoBehaviour {
	
	public List<CoinSpawnPoint> coinSpawnPoints = new List<CoinSpawnPoint>();
	public float secondsToRespawnCoin;
	public int amountConcurrentCoins;
	public uMMO_NetObject coin;
	
	private readonly object spawnTimerLock = new object();

	// Use this for initialization
	void Start () {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			CoinSpawnPoint[] csps = transform.GetComponentsInChildren<CoinSpawnPoint>();
			
			foreach(CoinSpawnPoint csp in csps) {
				coinSpawnPoints.Add(csp);	
			}
		
			for(int i=0;i<amountConcurrentCoins;i++) {
				StartCoroutine(startTimerToSpawnCoin());
			}
			
		}
	}
	
	public IEnumerator startTimerToSpawnCoin() {
		
		yield return new WaitForSeconds(secondsToRespawnCoin);
		
		lock(spawnTimerLock) {		
			
			int pos2spawn = -1;
			bool emptySpawnPointFound = false;
			int c = 0;
			while(!emptySpawnPointFound) {
				
				pos2spawn = Random.Range(0,coinSpawnPoints.Count-1);

				if(coinSpawnPoints[pos2spawn].hasExistingCoin()) {
					emptySpawnPointFound = false;

				} else {
					emptySpawnPointFound = true;	

				}

			} 

			Vector3 pos = coinSpawnPoints[pos2spawn].transform.position;
			Quaternion rot = coinSpawnPoints[pos2spawn].transform.rotation;
			
			rot = Quaternion.Euler(new Vector3(rot.x,rot.y,90f));
			
			uMMO_NetObject newCoin = uMMO_Main_SpawningMethod_DirectlyOnEstablishedConnectionSimpleDisconnect.instantiateUMMONetObject(Network.player,coin,pos,rot);
			
			coinSpawnPoints[pos2spawn].coin = newCoin.GetComponent<Coin>();
			
			GameObject CoinSpawnerGO = GameObject.Find("CoinSpawner");
			
			newCoin.transform.parent = CoinSpawnerGO.transform;
			newCoin.transform.localPosition = coinSpawnPoints[pos2spawn].transform.localPosition;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
