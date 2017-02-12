using UnityEngine;
using System.Collections;

public abstract class uMMO_NetObject_Module : MonoBehaviour {
	
	protected uMMO_NetObject netObject;

	public static uMMO_NetObject_Module initialize(uMMO_NetObject_Module module, uMMO_NetObject netObject) {
		
		if (module != null && netObject != null) {
			
			uMMO_NetObject_Module[] mods = (uMMO_NetObject_Module[])netObject.gameObject.GetComponentsInChildren<uMMO_NetObject_Module>();
			
			bool alreadyInstantiated = false;
			
			foreach(uMMO_NetObject_Module mod in mods) {
				
				if (mod.transform.parent == netObject.transform  //limit scope
					&& mod.GetType() == module.GetType()) { //one module (instance) per type (class)
					
					alreadyInstantiated = true;
					module = mod;
					break;
				}
				
			}
			
			if (!alreadyInstantiated) { //exists only as prefab in the project hierarchy, not as instance in scene yet
				GameObject go = (GameObject)Instantiate(module.gameObject);
				module = go.GetComponent<uMMO_NetObject_Module>();
			}
			
			if(module.transform.parent != netObject.transform) {
				
				module.transform.parent = netObject.transform;
				
				module.transform.localPosition = Vector3.zero;
				module.transform.localRotation = Quaternion.identity;				
			}
			
			module.netObject = netObject;
		}
		
		return module;
		
	}
}
