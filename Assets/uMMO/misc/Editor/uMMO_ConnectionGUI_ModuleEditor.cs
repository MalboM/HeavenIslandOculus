using UnityEngine;
using System.Collections;
using UnityEditor;

/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO custom inspector class.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[CustomEditor (typeof (uMMO_Main_ConnectionGUI_Module),true)]
public class uMMO_Main_ConnectionGUI_ModuleEditor : PropertyEditor {
	
	protected uMMO_Main_ConnectionGUI_Module instance;
	
	protected override void Initialize () {

	}
	
	public override void OnInspectorGUI () {
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);
		
		instance = (uMMO_Main_ConnectionGUI_Module) target;
		
		DrawDefaultInspector ();
		
		if (uMMO.get.showDocumentationInEditor)
			Comment (	"A ConnectionGUI module is a module that enables/obligates you to implement the function OnGUI. " +
						"In OnGUI() you can define your own connection GUI, a simple example module implementation is included in the package. You can just implement your own code here, and in this way you don't overwrite uMMO native plugin code, and don't have to hassle with it, once you upgrade to a newer version of the plugin.\r\n\r\n" +
						"You can set standard values for server IP and port here, if i.e. you choose to let the player decide where to connect. You can define other attributes here, if you inherit from uMMO_ConnectionGUI_Module.");
	}

}
