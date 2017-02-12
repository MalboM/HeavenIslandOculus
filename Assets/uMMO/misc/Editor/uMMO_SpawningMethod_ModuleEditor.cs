using UnityEngine;
using System.Collections;
using UnityEditor;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO custom inspector class.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[CustomEditor (typeof (uMMO_Main_SpawningMethod_Module),true)]
public class uMMO_Main_SpawningMethod_ModuleEditor : PropertyEditor {
	
	protected uMMO_Main_SpawningMethod_Module instance;
	
	protected override void Initialize () {

	}
	
	public override void OnInspectorGUI () {
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);
		
		instance = (uMMO_Main_SpawningMethod_Module) target;
		
		DrawDefaultInspector ();
		
		if (uMMO.get.showDocumentationInEditor)
			Comment (	"A SpawningMethod module is a module that enables/obligates you to implement 5 functions: instantiateUMMONetObject, OnPlayerConnected, OnConnectedToServer, OnPlayerDisconnected and OnDisconnectedFromServer. " +
						"i.e. OnPlayerConnected() is being called on the server once a player connects to it (read about specific unity events in the official Unity documentation). It is a function which is probably needed by " +
						"every MMO/multiplayer game. You can just implement your own code here, and in this way you don't overwrite uMMO native plugin code, and don't have to hassle with it, once you upgrade to a newer version of the plugin.\r\n\r\n" +
						"Furthermore you can set the number of simultaneous players here. uMMO plugin does not limit you to certain number of players. You can define other attributes here, if you inherit from uMMO_SpawningMethod_Module.");
	}

}
