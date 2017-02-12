using UnityEngine;
using System.Collections;
using UnityEditor;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO custom inspector class.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
[CustomEditor (typeof (uMMO_Main_DataTransmissionFilter_Module),true)]
public class uMMO_Main_DataTransmissionFilter_ModuleEditor : PropertyEditor {
	
	protected uMMO_Main_DataTransmissionFilter_Module instance;
	
	protected override void Initialize () {

	}
	
	public override void OnInspectorGUI () {
		EditorGUIUtility.LookLikeControls(Screen.width/1.7f);
		
		instance = (uMMO_Main_DataTransmissionFilter_Module) target;
		
		DrawDefaultInspector ();
		
		if (uMMO.get.showDocumentationInEditor)
			Comment (	"A DataTransmissionFilter module is a module that enables/obligates you to implement the function dataShouldBeTransmittedBetween. " +
						"In dataShouldBeTransmittedBetween() you can define your own method of deciding which networkplayers should receive and send data to and from whom, a simple example module implementation is included in the package. You can just implement your own code here, and in this way you don't overwrite uMMO native plugin code, and don't have to hassle with it, once you upgrade to a newer version of the plugin.\r\n\r\n" +
						"Take a look at class uMMO_DataTransmissionFilter_Distance: Here, the distance between two NetworkViews is the deciding factor, if you set it as the module for data transmission filter attribute in the uMMO " +
						"prefab you can here in the Editor also configure from which distance two NetworkViews should exchange data. This comes in handy, if you have a huge map, and someone is in the middle of a metropolis, and another " +
						"player in a vast desert. There are obviously hundreds of miles between them, and exchanging movement data i.e. does not make a lot of sense. You can also define your own attributes here, if you inherit from uMMO_DataTransmissionFilter_Module.");
	}

}
