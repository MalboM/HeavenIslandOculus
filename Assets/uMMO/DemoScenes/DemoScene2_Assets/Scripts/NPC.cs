using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
[RequireComponent (typeof (AttackVictim))]
public class NPC : MonoBehaviour {

	public float speed = 1f;
	public List<GameObject> waypoints = new List<GameObject>();
	protected GameObject physicsWrapper;
	
	private GameObject currentWaypoint;
	private float Xtolerance = 5f;
	private float Ytolerance = 5f;
	private float Ztolerance = 5f;
	
	private void Awake() {
		if (waypoints.Count < 1) {
			GameObject wpsGO = GameObject.Find("waypoints");
			
			foreach (Transform wp in wpsGO.transform)
			{
				// do whatever you want with child transform object here
				waypoints.Add(wp.gameObject);
			}	
		}
		physicsWrapper = gameObject;
	}	
	
	// Use this for initialization
	void Start () {
		
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		currentWaypoint = waypoints[0];
	}
	
	private void moveTowards(Vector3 target) {
		Vector3 direction = target - transform.position;
		direction = new Vector3(direction.x,0,direction.z);
	
		physicsWrapper.transform.rotation = Quaternion.LookRotation(direction);
	
	
		// Modify speed so we slow down when we are not facing the target
		var forward = physicsWrapper.transform.TransformDirection(Vector3.forward);

		// Move the character
		physicsWrapper.transform.position += forward * speed * 0.05f;
		
	}
	

	// Update is called once per frame
	void FixedUpdate () {
		
		AttackVictim victim = GetComponent<AttackVictim>();
		
		if (victim.alive) {
			
			GetComponent<Animation>().CrossFade("walk");

			
			Vector3 curPos = transform.position;
			Vector3 curWaypP = currentWaypoint.transform.position;
			if (curPos.x > curWaypP.x - Xtolerance && curPos.x < curWaypP.x + Xtolerance 
					&& curPos.y > curWaypP.y - Ytolerance && curPos.y < curWaypP.y + Ytolerance 
					&& curPos.z > curWaypP.z - Ztolerance && curPos.z < curWaypP.z + Ztolerance 
				) {
					
					int nextWPno = -1;
					for(int i=0;i<waypoints.Count;i++) {
						GameObject wp = waypoints[i];
						if (wp == currentWaypoint) {
							nextWPno = i+1;
							break;
						}
					}
					if (nextWPno > waypoints.Count-1)
						nextWPno = 0;
					currentWaypoint = waypoints[nextWPno];
			} 
				
			moveTowards(currentWaypoint.transform.position);

					
			
		}
	}
	
}
