using UnityEngine;
using System.Collections;

public class uMMO_Main_Module : MonoBehaviour {

	public static uMMO_Main_Module initialize(uMMO_Main_Module module) {
		
		if (module != null) {
			uMMO_Main_Module[] mods = (uMMO_Main_Module[])GameObject.FindSceneObjectsOfType(typeof (uMMO_Main_Module));
			
			bool alreadyInstantiated = false;
			
			foreach(uMMO_Main_Module mod in mods) {
				
				if (mod.GetType() == module.GetType()) { //one module (instance) per type (class)
					alreadyInstantiated = true;
					module = mod;
					break;
				}
				
			}
			
			if (!alreadyInstantiated) { //exists only as prefab in the project hierarchy, not as instance in scene yet
				GameObject go = (GameObject)Instantiate(module.gameObject);
				module = go.GetComponent<uMMO_Main_Module>();
			}
			
			GameObject go_uMMO = uMMO.get.gameObject;
			
			
			if(module.transform.parent != go_uMMO.transform) {
				
				module.transform.parent = go_uMMO.transform;
				
				module.transform.localPosition = Vector3.zero;
				module.transform.localRotation = Quaternion.identity;				
			}
		}
		
		return module;
		
	}	
}
