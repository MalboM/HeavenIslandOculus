using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO module class. Read in-Editor documentation for more info. Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public abstract class uMMO_Main_DataTransmissionFilter_Module : uMMO_Main_Module {
	
	public float dataTransmissionFilterUpdateInterval = 1f; // 1/dataTransmissionFilterUpdateInterval = checks and updates per second
	
	protected void Start() {
	
		StartCoroutine(updateDataTransmissionFilter());	
	}
	
	protected IEnumerator updateDataTransmissionFilter() {
		
		yield return new WaitForSeconds(dataTransmissionFilterUpdateInterval);
		
		if (Network.isServer ) { //only if is server and server is started
		
			//check scopes of networkviews ...
			uMMO.get.setDataTransmissionLimitations();
			
		}
		if (uMMO.get.dataTransmissionFilter != null && uMMO.get.architectureToCompile == uMMO_Architecture.Server)
			StartCoroutine(	updateDataTransmissionFilter() );
	}	
	
	public abstract bool dataShouldBeTransmittedBetween(NetworkView nv1, NetworkView nv2);
	
}
