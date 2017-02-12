using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

/* You can safely remove this file, it has no purpose but to clean demo scene 1 once. If you do remove it, please remove the object "SceneUtils_deleteMe" in DemoScene1 manually */

[ExecuteInEditMode]
public class SoftRareCleaner327464 : MonoBehaviour {
	
	public void cleanUp() {
		GameObject toClean = GameObject.Find ("SceneUtils_deleteMe");

		if (toClean != null && toClean.GetComponent("SoftRareSceneAdjuster327464") == null) {
			
			DestroyImmediate(toClean); //cleaning up adjuster object as it is not neccessary if adjuster script is not available, which is normal and harmless.
#if UNITY_EDITOR
			AssetDatabase.DeleteAsset("Assets/uMMO/DemoScenes/SoftRareCleaner327464.cs"); //clean myself so that i am not attached to a gameObject later by accident or doing unneccessary draw calls while developing.
#endif
		}
	}
	
#if UNITY_EDITOR
	
	void OnGUI() {
		cleanUp();	
	}
	
	void Update() {
		cleanUp();
	}
	
	void OnRenderObject () {
		cleanUp();
	}
	
#endif
	
}
