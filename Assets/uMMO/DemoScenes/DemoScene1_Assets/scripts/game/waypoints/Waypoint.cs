using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class Waypoint : MonoBehaviour {
		
	private bool initialized = false;
	public bool isActivated = false;
	
	public float speed2thisPoint;
	public float time2Elapse;
	
	private WaypointManager wpManager;
	private Transform objectTransform;
	
	public int racerNo;
	
	public RacerNPC racer;
	
	public int waypointNo;
	
	protected void determineWaypointNumbers() {
		
		string wpname = gameObject.name;
		
		Match match = Regex.Match(wpname, @"WP([0-9])\-([0-9]+)$",RegexOptions.IgnoreCase);
		
		if (match.Success)
		{
		    // Finally, we get the Group value and display it.
		    racerNo = int.Parse(match.Groups[1].Value);
			waypointNo = int.Parse(match.Groups[2].Value);
		}		
		
	}
	
	void Start() {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			determineWaypointNumbers();
			
			time2Elapse = 0;
		}
	}
	
	public void setWaypointManager(WaypointManager wpManager) {
		this.wpManager  = wpManager;
		objectTransform = wpManager.gameObject.transform;
		initialized = true;
		racer = wpManager.GetComponent<RacerNPC>();
	}
	
	private void moveTowards() {
		
		Vector3 direction = transform.position - objectTransform.position;
		direction = new Vector3(direction.x,0f,direction.z);
	
		if (!direction.Equals(Vector3.zero))
			objectTransform.rotation = Quaternion.LookRotation(direction);

		// Modify speed so we slow down when we are not facing the target
		var forward = objectTransform.TransformDirection(Vector3.forward);		
		
		float currSpeed = speed2thisPoint;
		
		if (currSpeed <= 0f) {
			currSpeed = wpManager.defaultSpeed;
		}
		
		racer.speed = currSpeed;
		
		// Move the character
		objectTransform.position += forward * currSpeed * 0.01f;
		
	    Vector3 pos = objectTransform.position;
	    
		float x = objectTransform.localScale.y/4;
    	objectTransform.position = new Vector3(pos.x,Terrain.activeTerrain.SampleHeight(pos)+(x),pos.z);		

	}	
	
	IEnumerator RotateTowards(Quaternion newRot, float time)
	{
		wpManager.actionState = WPActionState.ROTATING;
	    Quaternion start = objectTransform.rotation;
	    Quaternion end = newRot;
	    float t = 0;
	
	    while(t < 1)
	    {
	        yield return null;
	        t += Time.deltaTime / time;
	        objectTransform.rotation = Quaternion.Lerp(start, end, t);
	    }
		wpManager.actionState = WPActionState.STANDING;
		
	    objectTransform.rotation = end;
	}	
	
	void FixedUpdate () {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			if (initialized ) {
				if (isActivated ) {
					Vector3 curPos = objectTransform.position;
					Vector3 curWPpos = transform.position;
					
					if (curPos.x > curWPpos.x - wpManager.Xtolerance && curPos.x < curWPpos.x + wpManager.Xtolerance 
							&& curPos.y > curWPpos.y - wpManager.Ytolerance && curPos.y < curWPpos.y + wpManager.Ytolerance 
							&& curPos.z > curWPpos.z - wpManager.Ztolerance && curPos.z < curWPpos.z + wpManager.Ztolerance ) {
						
			
						if (waypointNo == 1) {
							racer.currentLap++;
		
							((RaceEvent)EventManager.nextEvent).updateLapCount(racer);
											
							if (isActivated) {
								racer.randomizeSpeed2Waypoints()	;						
							}
						}
						
						float rotatingSpeed = 0f;
						if (waypointNo == 1 && !EventManager.nextEvent.activated) {
							rotatingSpeed = 1f;
						} else {
							rotatingSpeed = 0.3f;
						}					
						
						isActivated = false;
						
						
						StartCoroutine(RotateTowards(transform.rotation,rotatingSpeed));
						
						if ((EventManager.nextEvent.activated && racer.currentLap <= ((RaceEvent)EventManager.nextEvent).maxLaps) && wpManager.waypoints.Count > 0 ) {
							
							StartCoroutine(wpManager.activateTimer());
							
						}
						
					} else {
						
						wpManager.actionState = WPActionState.MOVING;
						
						moveTowards();
						
					}
				}
			}
		}
	}	
	
}
