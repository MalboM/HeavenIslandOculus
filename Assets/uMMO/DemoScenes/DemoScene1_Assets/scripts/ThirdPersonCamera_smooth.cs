using UnityEngine;
using System.Collections;

public class ThirdPersonCamera_smooth : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	GameObject standardPosGO;			// the usual position for the camera, specified by a transform in the game
	public static bool activated = true;
	
	private bool transportedGUIElements = false;

	
	void Start()
	{

	}
	
	void FixedUpdate ()
	{
		
		if (standardPosGO == null) {	
			
			standardPosGO = GameObject.Find ("CamPos");	
		} else if (activated) {
			
			//print ("standardPosGO: "+standardPosGO);
			
			if (!transportedGUIElements) {
				GameObject GUIElements = standardPosGO.GetComponent<GUITransporter>().guiElements;
				
				GUIElements.transform.parent = this.transform;
				GUIElements.transform.localPosition = new Vector3(0.5259867f,-0.4232833f,1.399869f);
				GUIElements.transform.localRotation = Quaternion.Euler(-1.416809f,109.5415f,104.2379f);
				transportedGUIElements = true;
			}
			
			// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPosGO.transform.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPosGO.transform.forward, Time.deltaTime * smooth);
		}
		
	}
}
