using UnityEngine;
using System.Collections;

public class ThirdPersonCamera_robot : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	GameObject standardPos;			// the usual position for the camera, specified by a transform in the game
	Transform lookAtPos;			// the position to move the camera to when using head look
	
	void Start()
	{
		
		if(GameObject.Find ("LookAtPos"))
			lookAtPos = GameObject.Find ("LookAtPos").transform;
	}
	
	void FixedUpdate ()
	{
		if (Network.isClient) {
			// initialising references
			standardPos = GameObject.Find ("CamPos");
			
			// if we hold Alt
			if(Input.GetButton("Fire2") && lookAtPos)
			{
				// lerp the camera position to the look at position, and lerp its forward direction to match 
				transform.position = Vector3.Lerp(transform.position, lookAtPos.position, Time.deltaTime * smooth);
				transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
			}
			else if (standardPos != null)
			{	
				// return the camera to standard position and direction
				transform.position = Vector3.Lerp(transform.position, standardPos.transform.position, Time.deltaTime * smooth);	
				transform.forward = Vector3.Lerp(transform.forward, standardPos.transform.forward, Time.deltaTime * smooth);
			}
		}
		
	}
}
