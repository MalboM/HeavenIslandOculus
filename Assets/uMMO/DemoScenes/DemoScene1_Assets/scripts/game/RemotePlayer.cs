using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class RemotePlayer : Player {
	
	public Transform coinTransform;
	public TextMesh coinTextMesh;
	
	protected bool hideWhenBalanceZero = false;
	
	[RPC]
	public void UpdateAmountCoins(int newAmount) {
		amountCoins = newAmount;

		coinTextMesh.text = amountCoins+"";
		
		if (amountCoins > 0) {
			coinTransform.gameObject.SetActiveRecursively(true);	
		} else if (hideWhenBalanceZero) {
			coinTransform.gameObject.SetActiveRecursively(false);	
		}
	}	
	
	[RPC]
	public void AnnounceWinner(int newAmount) {
		UpdateAmountCoins(newAmount);
	}		

	// Use this for initialization
	void Start () {
		if (amountCoins < 1 && hideWhenBalanceZero)
			coinTransform.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		Camera localPlayerCam = Camera.current;
		
		if (localPlayerCam != null) {
			coinTransform.LookAt(localPlayerCam.transform);
			
			Vector3 euler = new Vector3(0f,coinTransform.eulerAngles.y-90f,90f);
			coinTransform.eulerAngles = euler;
			
			Vector3 direction = localPlayerCam.transform.position - coinTransform.position;
			
				
			
			RaycastHit hit;
			
			if (Physics.Raycast(coinTransform.transform.position,direction, out hit, Vector3.Distance(coinTransform.transform.position, localPlayerCam.transform.position))) {
				
				//Debug.DrawRay(coinTransform.transform.position, direction, Color.red);	
				coinTextMesh.GetComponent<Renderer>().enabled = false;
				
			} else {
				//print ("no hit");	
				coinTextMesh.GetComponent<Renderer>().enabled = true;
			}
		}
		
	}
}
