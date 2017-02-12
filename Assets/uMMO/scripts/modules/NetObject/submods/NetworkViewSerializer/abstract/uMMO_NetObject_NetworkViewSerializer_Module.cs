using UnityEngine;
using System.Collections;

public abstract class uMMO_NetObject_NetworkViewSerializer_Module : uMMO_NetObject_Module {

	public abstract void onReadFromNetworkView(BitStream stream, NetworkMessageInfo info);
	
	public abstract void onWriteToNetworkView(BitStream stream, NetworkMessageInfo info);
	
}
