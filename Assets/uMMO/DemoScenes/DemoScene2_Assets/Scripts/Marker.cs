using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
[RequireComponent (typeof (uMMO_NetObject))]
public class Marker : MonoBehaviour {
	
	public static uMMO_NetObject marked;
	public static List<Material> backupMats = new List<Material>();
	
	protected uMMO_NetObject thisChar;
	
	// Use this for initialization
	void Start () {
		thisChar = GetComponent<uMMO_NetObject>();
	}	
	
	void OnMouseDown() {
		mark (thisChar);
	}
	
	void mark(uMMO_NetObject ch) {
		
		unmark( );
			
		if (marked == null ) {
			Renderer[] rs= ch.gameObject.GetComponentsInChildren<Renderer>();
			
			foreach(Renderer r in rs) {
				string shaderText=
								"Shader \"Alpha Additive\" {" +
								"Properties { _Color (\"Main Color\", Color) = (1,0,1,0) }" +
								"SubShader {" +
								" Tags { \"Queue\" = \"Transparent\" }" +
								" Pass {" +
								" Blend One One ZWrite Off ColorMask RGB" +
								" Material { Diffuse [_Color] Ambient [_Color] }" +
								" Lighting On" +
								" SetTexture [_Dummy] { combine primary double, primary }" +
								" }" +
								"}" +
								"}";
				
				backupMats.Add(r.material);
				r.material = new Material( shaderText );	
			}	
			
			marked = ch;
		}
	}
	
	void unmark() {
		if (marked != null ) {
			
			Renderer[] rs= marked.gameObject.GetComponentsInChildren<Renderer>();
			int c= 0;
			
			//print ("unmark: rs.Length "+rs.Length);
			//print ("unmark: backupMats.Count "+backupMats.Count);
			
			foreach(Renderer r in rs) {
				r.material = backupMats[c++];
			}
			
			backupMats.Clear();
			marked = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			unmark();	
		}
	}
}
