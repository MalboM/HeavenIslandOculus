using UnityEngine;
using System.Collections;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO module class. Read in-Editor documentation for more info. Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public class uMMO_Main_DataTransmissionFilter_AlwaysFalse_TEST : uMMO_Main_DataTransmissionFilter_Module {
	
	//DO NOT USE IN PRODUCTION
	
	public override bool dataShouldBeTransmittedBetween(NetworkView nv1, NetworkView nv2) {
		
		return false;
	}
}
