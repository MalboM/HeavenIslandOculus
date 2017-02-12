using UnityEngine;
using System.Collections;

public class uMMO_NetObject_NetworkViewSerializer_SimplyLerpTransform : uMMO_NetObject_NetworkViewSerializer_Module {
	
	public float positionLerpTimeFraction = 0.95f;
	public float rotationLerpTimeFraction = 0.95f;

	public override void onReadFromNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (netObject.synchronizePosition) {
			Vector3 pos = Vector3.zero;
			stream.Serialize(ref pos);

			netObject.transform.position = Vector3.Lerp(transform.position, pos, positionLerpTimeFraction );			
		} 
		if (netObject.synchronizeRotation) {
			Quaternion rot = Quaternion.identity;
			stream.Serialize(ref rot);
			netObject.transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationLerpTimeFraction);			
		}
		
	}
	
	public override void onWriteToNetworkView(BitStream stream, NetworkMessageInfo info) { 
		
		if (netObject.synchronizePosition) {
			Vector3 pos = netObject.transform.position;
			stream.Serialize(ref pos);
		} 
		if (netObject.synchronizeRotation) {
			Quaternion rot = netObject.transform.rotation;
			stream.Serialize(ref rot);			
		} 		
	}
	
}
