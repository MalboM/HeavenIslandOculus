using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class RacerNPC : NPC1 {

	public WaypointManager wpManager;
	public Animation theAnimation;
	public Renderer theRenderer;
	public float speed;
	public int currentLap = 0;
	[HideInInspector]
	public bool racing = false;
	
	public Waypoint idlePos;
	
	public int racerNo;
	
	public void get2StartLine() {
		
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
				
			Waypoint[] waypointsInScene = (Waypoint[])GameObject.FindSceneObjectsOfType(typeof (Waypoint));
			
			foreach(Waypoint wpA in waypointsInScene) {
				if (wpA.waypointNo == 1)
					wpA.speed2thisPoint = 0; //init wp 1 (starting point), birds should walk here, not run.				
			}
			
			int no = 0;
			foreach(Waypoint wpA in waypointsInScene) {
	
				foreach(Waypoint wpB in waypointsInScene) {
					
					if (wpB.waypointNo == no && wpB.racerNo == racerNo) {
						
						no++;
						wpManager.waypoints.Add(wpB);
							
						break;
					}
				}		
				
				if (wpManager.waypoints.Count == waypointsInScene.Length / 4)
					break;
			}
		}		
		
		wpManager.StartCoroutine(wpManager.initiate());
		idlePos = wpManager.waypoints[0];
	}
	
	void Awake() {
		wpManager = gameObject.GetComponent<WaypointManager>();
	}
	
	void Start() {
	
	}
	
	public void randomizeSpeed2Waypoints() {
		
		foreach(Waypoint wp in wpManager.waypoints) {
			if (wp.waypointNo > 0) {
				float minbonus = 0f;
				float maxbonus = 0f;
				
				minbonus = (((float)racerNo)*0.05f);
				maxbonus = (((float)racerNo)*0.05f);
				
				float r = Random.Range(8f+minbonus,12f+maxbonus);
				
				wp.speed2thisPoint = r;
			}
		}
	}	
	
	void Update() {
		
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
			
			
		
			if (wpManager.actionState == WPActionState.STANDING) {
				
				//TODO: toggle different idle anims (random)
				
				//theAnimation.CrossFade("gotbit");
				theAnimation.CrossFade("IdleHold");
				//theAnimation.CrossFade("IdleFishingSpread");
				//theAnimation.CrossFade("IdleFishing");
				//theAnimation.CrossFade("IdleStrechNeck");
					
			} else if (wpManager.actionState == WPActionState.MOVING) {
				
				if (speed <= 1.3)
					theAnimation.CrossFade("Walk");
				else
					theAnimation.CrossFade("Run");
				
			} else if (wpManager.actionState == WPActionState.ROTATING) {
				
				theAnimation.CrossFade("Walk");
				
			}
			
		}
		
	}
}
