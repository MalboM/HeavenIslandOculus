using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class LocalPlayer: Player {
	
	protected float distanceToSpeakToNPC = 3f;
	
	public ConversationNPC npcToSpeakTo;
	
	public GUIStyle toolTipStyle;
	
	public TextMesh coinTextMesh;
	
	public bool spoke2Olaf = false;
	
	[HideInInspector]
	public bool winner = false;
	
	public GUITexture inputIntro;
	
	// Use this for initialization
	void Start () {
		GameObject goGUITexture = GameObject.Find ("input_guiTexture");
		inputIntro = goGUITexture.GetComponent<GUITexture>();
	}
	
	[RPC]
	public void UpdateAmountCoins(int newAmount) {
		amountCoins = newAmount;

		coinTextMesh.text = amountCoins+"";
		
	}
	
	[RPC]
	public void AnnounceWinner(int newAmount) {
		
		winner = true;
		UpdateAmountCoins(newAmount);
		
	}	
	
	void OnGUI() {
			
		if (npcToSpeakTo != null) {
			
			if (!npcToSpeakTo.conversation.showDialog)
				GUI.Label( new Rect (Screen.width * 0.05f,Screen.height - (Screen.height * 0.10f),245f,30f),"  Press [F] to talk to "+npcToSpeakTo.name, toolTipStyle );
		}
			
	}
	
	void FixedUpdate () {
		
		int layerMask = 1 << 8; //ConversationNPC & Coin = NPOs
		
		Collider[] colls = Physics.OverlapSphere(transform.position, distanceToSpeakToNPC, layerMask);
		bool found = false;
		foreach(Collider c in colls) {
		
			if (c.gameObject.GetComponent<ConversationNPC>() != null) {
		
				if (Vector3.Distance(c.gameObject.transform.position,this.gameObject.transform.position) <= distanceToSpeakToNPC) {
					npcToSpeakTo = c.gameObject.GetComponent<ConversationNPC>();
					found = true;
					break;
				}
			}		
		}

		if (!found) {
			npcToSpeakTo = null;	
		}
	}
	
	void Update() {
		if (Input.GetKeyDown("f")) {
			if (npcToSpeakTo != null) {
				npcToSpeakTo.conversation.showDialog = true;
				
				Vector3 direction = transform.position - npcToSpeakTo.transform.position;
				direction = new Vector3(direction.x,0f,direction.z);
			
				if (!direction.Equals(Vector3.zero))
					npcToSpeakTo.transform.rotation = Quaternion.LookRotation(direction);					
			}
		}	
		
		if (Input.GetKeyDown("i")) {
			if (inputIntro.enabled) {
				inputIntro.enabled = false;	
			} else {
				inputIntro.enabled = true;
			}
		}
		
		
	}
	
	void __uMMO_localPlayer_init() {
		
		uMMO_NetObject[] objects = uMMO_StaticLibrary.getAllNetObjects();
		
		foreach(uMMO_NetObject c in objects) {
			
			if (c.GetComponent<NPC1>() != null) {
				
				c.GetComponent<Label>().guiCamera = GetComponent<uMMO_NetObject>().cameraToActivateOnLocalPlayer;	
			}
			
		}
		
		
	}
}
