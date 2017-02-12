using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class EventManager : MonoBehaviour {
	
	public List<AEvent> events = new List<AEvent>();
	
	public static AEvent nextEvent;
	
	public GUIStyle eventTitleStyle;
	
	public GUIStyle eventSubtitleStyle;
	
	public static readonly object findNextEventLock = new object();
	
	protected IEnumerator findNextEvent() {
		
		lock(findNextEventLock) {
			
			yield return new WaitForSeconds(1f);
			
			double smallestTimestamp = -1;
			AEvent nextPresumedEvent = null;
			
			foreach(AEvent e in events) {
				e.currentCounter = e.eventStart - uMMO_StaticLibrary.ts_now();
				if (e != null) {
					
					if (e.eventStart < smallestTimestamp || nextPresumedEvent == null) {
						smallestTimestamp = e.eventStart;	
						nextPresumedEvent = e;
						
					}				
					
				}
			}
			
			if (nextPresumedEvent != null) {
				nextEvent = nextPresumedEvent;	
			
				List<AEvent> events2Delete = new List<AEvent>();
				
				foreach(AEvent e in events) {
					
					if (e != null) {
						
						if (e.preparation && uMMO_StaticLibrary.ts_now() >= (e.eventStart - e.preparationWhenSecondsLeft) && !e.preparationActivated) {
							
							e.preparationActivated = true;				
							e.prepare();
							
						} else if (uMMO_StaticLibrary.ts_now() >= e.eventStart && !e.activated) {
							
							e.activated = true;
							e.startEvent();	
							
						} 
					}				
				}
			}
		
			StartCoroutine(findNextEvent());
		}
		
	}
	
	void OnServerInitialized() {
		
		StartCoroutine(findNextEvent());
		
	}
		
}
