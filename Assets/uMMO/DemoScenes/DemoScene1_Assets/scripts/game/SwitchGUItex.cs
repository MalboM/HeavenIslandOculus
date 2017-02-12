using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class SwitchGUItex : MonoBehaviour {
	
	protected bool loggedin =  false;
	protected GUITexture guitex;

	// Use this for initialization
	void Start () {
		if (uMMO.get.architectureToCompile == uMMO_Architecture.Client) {
			
			guitex = GetComponent<GUITexture>();
			guitex.enabled = true;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!loggedin && Network.isClient) {
			
			guitex.enabled = false;	
		
			loggedin = true;
		}
	}
}
