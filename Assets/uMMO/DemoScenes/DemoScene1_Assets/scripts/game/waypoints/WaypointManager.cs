using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class WaypointManager : MonoBehaviour {
		
	
	public WPActionState actionState = WPActionState.STANDING;
	public float Xtolerance = 2f;
	public float Ytolerance = 2f;
	public float Ztolerance = 2f;	
	public float defaultSpeed = 2f;
	public List<Waypoint> waypoints;
	public Waypoint current;
	[HideInInspector]
	private float time2init = 2f;
	
	void Start() {
		//StartCoroutine(initiate());
		
	}
	
	public IEnumerator initiate() {
		
		yield return new WaitForSeconds(time2init);

		List<Waypoint> wps2remove = new List<Waypoint>();
		
		foreach(Waypoint wp in waypoints) {
			if (wp == null || wp.gameObject == null) {
				wps2remove.Add(wp);
			}
		}
		
		foreach(Waypoint wp in wps2remove) {
			waypoints.Remove(wp);	
		}
		
		foreach(Waypoint wp in waypoints) {
			
			wp.setWaypointManager(this);
		}
		
		if (waypoints.Count > 0)
			chooseNextWaypoint(false);
	}
	
	public void chooseNextWaypoint(bool idlePosition) {
		
		int index;
		if (!idlePosition) {
			if (current == null) {
				//pick first 
				index = 1;
			} else {
				
				current.isActivated = false;
				
				int oldIndex = waypoints.IndexOf(current);
				
				if (oldIndex == waypoints.Count-1) {
					index = 1;	
				} else {
					index = oldIndex+1;
				}
				
			}
		} else {
			index = 0;	
		}
		current = waypoints[index];
		
		current.isActivated = true;
	}
	
	public IEnumerator activateTimer() {
		
		yield return new WaitForSeconds(0f);
		
		chooseNextWaypoint(false);
	}

}
