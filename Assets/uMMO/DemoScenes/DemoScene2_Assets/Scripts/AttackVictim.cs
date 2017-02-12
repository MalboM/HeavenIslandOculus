using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
[RequireComponent (typeof (uMMO_NetObject))]
public class AttackVictim : MonoBehaviour {
	
	public int health;
	public bool alive = true;
	
	private uMMO_NetObject thisChar;
	
	
	[RPC]
	private void sufferAttack(NetworkPlayer attackerPlayer, int attackNo) {
		
		Attack attack = AttackManager.getAttack(attackNo);
		
		int damage = attack.getDamage();
		
		health -= damage;
		
		print (thisChar.nplayer+": I got hit. health: "+health);
		
		if (thisChar.nplayer == Network.player) { //TODO: if Network.isServer ?
			
			//print (thisChar.nplayer+": I got hit. health: "+health);
			GetComponent<ThirdPersonSimpleAnimation_TEST>().ApplyDamage();
			
			
			if (health < 1) {
				
				alive = false;
			
				StartCoroutine(dieAnim());
				
			}
		}
	}
	
	private IEnumerator dieAnim() {
		yield return new WaitForSeconds(0.8f);
		GetComponent<ThirdPersonSimpleAnimation_TEST>().Die();
		
		if (thisChar.objectType == uMMO_ObjectType.NonPlayerObject) {
			uMMO.get.removeNetObject(thisChar);	
		}		
	}

	// Use this for initialization
	void Start () {
		thisChar = GetComponent<uMMO_NetObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
