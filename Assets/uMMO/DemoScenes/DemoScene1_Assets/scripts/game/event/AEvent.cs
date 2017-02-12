using UnityEngine;
using System.Collections;
using System;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
abstract public class AEvent : MonoBehaviour {
	
	public string eventName;
	
	public string secretEventName;
	
	public int playAfterSeconds;
	
	public double currentCounter;
	
	private double ts_eventStart;
	
	public bool recurring;
	
	public int recurringAfterSeconds;
	
	public bool preparation;
	
	//[HideInInspector]
	public bool activated = false;
	
	//[HideInInspector]
	public bool preparationActivated = false;	
	
	public int preparationWhenSecondsLeft;
	
	public bool networkedEvent;

	public abstract void startEvent();
	
	protected abstract void endEvent();
	
	protected abstract void initializeConcrete();
	
	public abstract void prepare();
	
	protected EventManager eventManager;
	
	private readonly object getTimestampLock = new object();
	
	public void Start() {
		
		initializeAbstract();
	}
	
	protected void initializeAbstract() {
		activated = false;
		preparationActivated = false;
		
		ts_eventStart = uMMO_StaticLibrary.ts_now()+playAfterSeconds;
		
		GameObject goGame = GameObject.Find ("Game");
		eventManager = goGame.GetComponent<EventManager>();
		
		if (!eventManager.events.Contains(this))
			eventManager.events.Add(this);
		
		if (transform.parent == null)
			transform.parent = goGame.transform;
		
		initializeConcrete();
	}
	
	public void destroyEvent() {
		
		StopAllCoroutines();
		
		lock(EventManager.findNextEventLock) {
			
			playAfterSeconds = recurringAfterSeconds;
		
			if (uMMO.get.architectureToCompile == uMMO_Architecture.Server) {
				
				if (networkedEvent) {
					
					Network.RemoveRPCs(GetComponent<NetworkView>().viewID);
							
				}			
				
				endEvent();	
				
				if (recurring) {
					
					initializeAbstract();
					
				}
			}
		}

	}
	
	protected void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (networkedEvent) {
			if (stream.isWriting)
			{
				
				stream.Serialize(ref preparationActivated);
				stream.Serialize(ref activated);
				
			}
			else
			{	
	
				stream.Serialize(ref preparationActivated);
				stream.Serialize(ref activated);
			}
		}
	}	
	
    public double eventStart
    {
        get
        {
			lock (getTimestampLock) {
            
            	return ts_eventStart;
			}
        }
		set
		{
			lock (getTimestampLock) {
				ts_eventStart = value;		
			}
		}
    }	
	
}
