using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class only shows a hint at design time, its totally dispensable. Just remove the script if you don't need it.
 * You may only use/or and change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */

[ExecuteInEditMode]
public class uMMO_GUITextSwitch : MonoBehaviour {


	public static GUIText guiText;
	public bool spawnHint = true;
	
	void Awake() {

		if (spawnHint ) {
			guiText = findGUIText();	

#if UNITY_EDITOR
				guiText.text = "";	
#endif			
			
		}
	}
	
	GUIText findGUIText() {
		GUIText result = null;	
		GameObject guiTextGO = (GameObject) GameObject.Find("uMMO_GUIText");
		
		if (guiTextGO == null) {
			GameObject go = new GameObject("uMMO_GUIText");
			result = go.AddComponent<GUIText>();
			result.transform.position = new Vector3(0.3f,1f,0f);
			result.fontSize = 19;
			result.color = Color.black;
		
		} else {
			result = guiTextGO.GetComponent<GUIText>();	
		}

		return result;
	}
#if UNITY_EDITOR
	// Update is called once per frame
	public void setGUIText() {
		
		//guiText = findGUIText();
		if ( !Application.isPlaying) { 
			if (spawnHint && guiText != null) {
			
				GameObject go_uMMO = GameObject.Find("uMMO");
				uMMO thisuMMO = go_uMMO.GetComponent<uMMO>();
			
				if (thisuMMO.architectureToCompile == uMMO_Architecture.TEST_UnityEditorIsServer_OthersAreClients ) {
					
					guiText.text = "Testing mode: If you start this scene in Editor, it will resemble your server.\r\n" +
						"To spawn clients, make sure you exported the scene to standalone or webplayer.\r\n" +
						"Every started instance of standalone/webplayer build will represent one client.\r\n" +
						"(You can switch off this hint permanently on the uMMO prefab)";
				
				} else if (thisuMMO.architectureToCompile == uMMO_Architecture.TEST_UnityEditorIsClient_OtherIsServer ) {		
					
					guiText.text = "Testing mode: If you start the scene in Editor, it will resemble a client.\r\n" +
						"Export the scene to standalone or webplayer, the exported build will then represent\r\n" +
						"your server. (You can switch off this hint permanently on the uMMO prefab)";
				
				} else if (thisuMMO.architectureToCompile == uMMO_Architecture.Server ) {		
					
					guiText.text = "Production mode: Server! Whether you export a build or start the scene in Editor,\r\n" +
					 	"it will start as a server: Click \"Build & Run\" now to export your server.\r\n" +
					 	"(You can switch off this hint permanently on the uMMO prefab)";
					
				} else if (thisuMMO.architectureToCompile == uMMO_Architecture.Server ) {		
					
					guiText.text = "Production mode: Client! Whether you export a build or start the scene in Editor,\r\n" +
					 	"it will start as a client: Click \"Build & Run\" now to export your client.\r\n" +
					 	"(You can switch off this hint permanently on the uMMO prefab)";
					
									
				} else {
					
					guiText.text = "";
				}
			} else {
				
				if (guiText != null)
					guiText.text = "";
			}
		} else {
			if (guiText != null)
				guiText.text = "";			
		}
	}
	
	void OnGUI() {
		setGUIText();
	}	
	
	void OnRenderObject() {
		setGUIText();
	}
	
	void Update () {	
		setGUIText(); 
	}
#endif
}
