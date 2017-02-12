using UnityEngine;
using System.Collections;

public class uMMO_NetObject_NetworkViewSerializer_SyncLegacyAnims : uMMO_NetObject_NetworkViewSerializer_Module {
	
	/* contains the current animation number */
	protected int currentAnimation = -1;
	/* contains the last saved animation number */
	protected int lastAnimation = -1;
	/* contains the current weight of the animation */
	protected float currentWeight;
	/* contains the current normalized speed of the animation */
	protected float currentNormalizedSpeed;	
	/* contains the highest normalized speed of an animation */
	protected float HighestNormalizedSpeed;
	/* contains the highest weight of an animation */
	protected float HighestWeight;
	/* contains the number of the animation with the heighest weight */
	protected int HighestAnim;		
	
	
	/* resets animation weights (weights determine which animation is currently playing) */
	public void resetAnimationWeights() {
		
		foreach(AnimationState aS in netObject.objectContainingAnimations.GetComponent<Animation>()) {
		
			netObject.objectContainingAnimations.GetComponent<Animation>()[aS.name].weight = 0f;
			netObject.objectContainingAnimations.GetComponent<Animation>()[aS.name].enabled = false;

		}	
	}
	
	/* sets animation weights (weights determine which animation is currently playing) */
	public void setAnimationValues(int currentAnimation, float currentWeight, float normalizedSpeed) {
		
		int c=0;
		string animString = "";
		foreach(AnimationState aS in netObject.objectContainingAnimations.GetComponent<Animation>()) {

			/*if (Marker.marked != null && Marker.marked == this)
				print ("BEFORE anim: "+c+", name: "+aS.name+", weight: "+animation[aS.name].weight+", normalizedTime: "+animation[aS.name].normalizedTime+", layer: "+animation[aS.name].layer);			
				*/

			if (currentAnimation == c ) {					
				
				
				if (currentWeight > 0f) {
					lastAnimation = currentAnimation;
					
					netObject.objectContainingAnimations.GetComponent<Animation>()[aS.name].weight = currentWeight;
					netObject.objectContainingAnimations.GetComponent<Animation>()[aS.name].normalizedSpeed = normalizedSpeed;

					netObject.objectContainingAnimations.GetComponent<Animation>().Play(aS.name);
				}				
				break; //?
				
			}	
			/*if (Marker.marked != null && Marker.marked == this)
				print ("AFTER anim: "+c+", name: "+aS.name+", weight: "+animation[aS.name].weight+", normalizedSpeed: "+animation[aS.name].normalizedSpeed+", enabled: "+animation[aS.name].enabled);			
			*/
			c++;
		}	
	}	
	
	public override void onWriteToNetworkView(BitStream stream, NetworkMessageInfo info) {
		if (netObject.synchronizeAnimations) {
			int c=0;
			
			HighestWeight= 0f;
			HighestAnim = -1;
			HighestNormalizedSpeed = 0f;
			foreach(AnimationState aS in netObject.objectContainingAnimations.GetComponent<Animation>()) {

				if (netObject.objectContainingAnimations.GetComponent<Animation>().IsPlaying(aS.name) ) {
					
					currentAnimation = c;
					currentWeight = aS.weight;
					currentNormalizedSpeed = aS.normalizedSpeed;
					if (currentWeight > 0f && currentWeight > HighestWeight) {
						HighestWeight = currentWeight;
						HighestAnim = currentAnimation;
						
						HighestNormalizedSpeed = currentNormalizedSpeed;
					}
					
				}
				c++;
			}
			
			//if (HighestAnim > -1) {
				stream.Serialize(ref HighestAnim);
				stream.Serialize(ref HighestWeight);
				stream.Serialize(ref HighestNormalizedSpeed);
			//}
		}		
			
	}
	
	public override void onReadFromNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		if (netObject.synchronizeAnimations) {			
			
			resetAnimationWeights();
							
			stream.Serialize(ref currentAnimation);
			stream.Serialize(ref currentWeight);
			stream.Serialize(ref currentNormalizedSpeed);
			
			//if(currentAnimation > -1) 
				setAnimationValues(currentAnimation, currentWeight, currentNormalizedSpeed);
			
		}
	
	}	
	
	// Use this for initialization
	[System.Reflection.Obfuscation]
	void Start () {
		//resetAnimationWeights();
		//play standard animation
		if (netObject.synchronizeRotation)
			netObject.objectContainingAnimations.GetComponent<Animation>().Play(netObject.objectContainingAnimations.GetComponent<Animation>().clip.name);
		
	}
}
