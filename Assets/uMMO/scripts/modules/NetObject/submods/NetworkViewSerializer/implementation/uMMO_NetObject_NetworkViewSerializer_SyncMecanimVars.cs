using UnityEngine;
using System.Collections;

public class uMMO_NetObject_NetworkViewSerializer_SyncMecanimVars : uMMO_NetObject_NetworkViewSerializer_Module {

	//Done by Michael Schumann in order to help SoftRare, and this amazing asset. 
	//In order to sync mecanim animations, one will send bools / floats / ints / strings back and forth. 
	//In order to make those work with the animations however, one has to do this a certain way. 
	//The way to do that, is actually by first grabbing reference to the Animator, then using it like so: 
	//Animator = anim. 
	//anim.GetBool("someBool");
	//anim.GetFloat("someFloat");
	//That is how one would sync mecanim animations. 
	//Now, to do this with a list of strings, one needs a method to check what kind of variable they are. 
	//As in, if(anim.GetBool("ListItem1") == null){ return; }else{ SyncThisAsBool(); } 
	//if(anim.GetFloat("ListItem1") == null){ return; }else{ SyncThisAsFloat(); }
	//Frankly, I would do this once, so call it in the start method maybe, or whatever, so its done once, then done forever. 
	//This is how you will do it. 
	
	private Animator anim;

	//Send things 
	public override void onWriteToNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (netObject.synchronizeAnimations) {
			//Get our animator 
			if (anim==null)
				anim = netObject.objectContainingAnimations.GetComponent<Animator>(); 
			//Mecanim bools 
			for(int x = 0; x < netObject.mecanimBools.Count; x++){
				bool mecanimVar = anim.GetBool(netObject.mecanimBools[x]);
				stream.Serialize(ref mecanimVar); 
			}
			//Mecanim Floats 
			for(int x = 0; x < netObject.mecanimFloats.Count; x++){
				float mecanimVar = anim.GetFloat(netObject.mecanimFloats[x]); 
				stream.Serialize(ref mecanimVar);
			}
			//Mecanim Ints 
			for(int x = 0; x < netObject.mecanimInts.Count; x++){
				int mecanimVar = anim.GetInteger(netObject.mecanimInts[x]); 
				stream.Serialize(ref mecanimVar);
			}
		}		
	}//End OnWriteToNetworkView 

	//Set / get things basically 
	public override void onReadFromNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (netObject.synchronizeAnimations) {
			//Reset weights
			//as well as get animator 
			if (anim == null)
				anim = netObject.objectContainingAnimations.GetComponent<Animator>(); 
			/*if(anim.layerCount >= 2)
				anim.SetLayerWeight(1, 1);*/
			//Mecanim bools 
			for(int x = 0; x < netObject.mecanimBools.Count; x++){
				bool mecanimVar = anim.GetBool(netObject.mecanimBools[x]);
				stream.Serialize(ref mecanimVar); 
				anim.SetBool(netObject.mecanimBools[x], mecanimVar); 
				
			}
			//Mecanim Floats 
			for(int x = 0; x < netObject.mecanimFloats.Count; x++){
				float mecanimVar = anim.GetFloat(netObject.mecanimFloats[x]); 
				stream.Serialize(ref mecanimVar);
				anim.SetFloat(netObject.mecanimFloats[x], mecanimVar); 
				
			}
			//Mecanim Ints 
			for(int x = 0; x < netObject.mecanimInts.Count; x++){
				int mecanimVar = anim.GetInteger(netObject.mecanimInts[x]); 
				stream.Serialize(ref mecanimVar);
				anim.SetInteger(netObject.mecanimInts[x], mecanimVar); 
				
			}
		}
	}//End OnReadFromNetView 
	
	// Use this for initialization
	[System.Reflection.Obfuscation]
	void Start () {
		anim = netObject.objectContainingAnimations.GetComponent<Animator>(); 
		/*if(anim.layerCount >= 2)
			anim.SetLayerWeight(1, 1);*/

	}
}
