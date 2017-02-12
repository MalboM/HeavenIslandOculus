using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
abstract public class Conversation : MonoBehaviour {
	
	public bool showDialog = false;
	public Texture2D btnHover;
	public Texture diaTexture;
	public GUIStyle diaStyle;
	
	void OnGUI() {
		
		if (showDialog) {
			dialogContents();
		}
		
	}
	
	protected abstract void dialogContents();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
