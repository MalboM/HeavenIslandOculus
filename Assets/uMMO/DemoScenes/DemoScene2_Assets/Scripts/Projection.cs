using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class Projection : MonoBehaviour {
	
	public GameObject outerCircle;
	public GameObject innerCircle;
	
	protected Transform currTrans;
	
	void Start() {

		float filterDistance = ((uMMO_Main_DataTransmissionFilter_Distance)uMMO.get.dataTransmissionFilter).distanceToFilterDataTransmission;
		
		outerCircle.transform.localScale = new Vector3(filterDistance * 2f,0.01f,filterDistance * 2);
		
		innerCircle.transform.localScale = new Vector3((filterDistance * 2f) - (filterDistance * 0.2f),0.01f,(filterDistance * 2f) - (filterDistance * 0.2f));
		
		currTrans = transform.parent;
		
		//transform.parent = null;
	}
	
	void Update() {
		
		outerCircle.transform.position = new Vector3(currTrans.position.x,0.0001f,currTrans.position.z);
		innerCircle.transform.position = new Vector3(currTrans.position.x,0.0005f,currTrans.position.z);
	}
	
}
