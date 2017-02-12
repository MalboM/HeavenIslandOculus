using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class uMMO_NetObject_GraduallyUpdateState_PredictionConfig : uMMO_NetObject_Custom_Module {
	
	private uMMO_NetObject_NetworkViewSerializer_GraduallyUpdateState Mod=null;
	
	public bool showLagButton;
	public bool showPredictionDataGUI;
	private string str_connInfoHelper ="";	
	
	private string str_TimeThreshold = "";
	private string str_PredictionThreshold = "";
	private string str_InterpolationBackTime = "";
	
	Rect connInfoRect = new Rect (Screen.width-270,105,260,50);
	Rect connInfoHelperRect = new Rect (Screen.width-270,355,260,50);
	Rect LagButtonRect = new Rect (Screen.width-80,75,70,25);	
	
	[SerializeField]
	int m_MsgRate = 0;
	[SerializeField]
	double m_MsgLatency = 0;	
	
	// Use this for initialization
	void Start () {
		List<uMMO_NetObject_NetworkViewSerializer_Module> networkViewSerializerMods = netObject.networkViewSerializerMods;
		
		uMMO_NetObject_NetworkViewSerializer_Module found=null;
		foreach(uMMO_NetObject_NetworkViewSerializer_Module mod in networkViewSerializerMods) {
			if (mod is uMMO_NetObject_NetworkViewSerializer_GraduallyUpdateState ) {
				found = mod;
				break;
			}
		}
		
		if (found == null) {
			enabled = false;
			print ("uMMO hint: uMMO_NetObject_NetworkViewSerializer_GraduallyUpdateState is not present, disabling prediction config gui");
		}
		
		Mod = (uMMO_NetObject_NetworkViewSerializer_GraduallyUpdateState) found;
		
		str_TimeThreshold = Mod.m_TimeThreshold+"";
		str_PredictionThreshold = Mod.m_PredictionThreshold+"";
		str_InterpolationBackTime = Mod.m_InterpolationBackTime+"";
	}	

	void OnGUI() {
		
		if (Mod.m_IsMine && showLagButton) {
			
			string LagButtonText = "";
			
			if (!showPredictionDataGUI) {
				LagButtonText = "Lag/jitter?";
			} else {
				LagButtonText = "Close";
			}
			
			if (GUI.Button(LagButtonRect,LagButtonText)) {
				
				if (showPredictionDataGUI) {
					showPredictionDataGUI = false;
				} else {
					showPredictionDataGUI = true;
				}
			}
		}
		
		if (Mod.m_IsMine && showPredictionDataGUI) {
			connInfoRect = GUILayout.Window(0, connInfoRect, MakeConnInfoWindow, "uMMO client-side prediction config");
		}
		
		if (str_connInfoHelper != "" && showPredictionDataGUI) {
			connInfoHelperRect = GUILayout.Window(1, connInfoHelperRect, MakeConnInfoHelperWindow, "What is ..");			
		}
	}
	
	void MakeConnInfoWindow(int windowID) {
		
		GUILayout.Label(string.Format("Depending from where and how you connect you can experience lag/jitter. Use this interface to smooth out your networking experience:"));
		
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("?",GUILayout.Width(22))) {

			str_connInfoHelper = "..interpolation back time?\r\n\r\nHow far behind should server data be played back during remote player interpolation. The poorer the connection the higher this value needs to" +
				"be. Fast connections should do fine with 0.1. The server latency affects this the most.";

		}
		GUILayout.Label(string.Format("Interpolation back time: "));
		str_InterpolationBackTime = GUILayout.TextField(str_InterpolationBackTime,10);
		
		try {
			double.Parse(str_InterpolationBackTime);
			Mod.m_InterpolationBackTime = double.Parse(str_InterpolationBackTime);
			
			uMMO_StaticLibrary.global_InterpolationBackTime = Mod.m_InterpolationBackTime;
		} catch {
				
		}
			
		GUILayout.EndHorizontal();	
		if (!Mod.noErrorCorrection) {
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("?",GUILayout.Width(22))) {
				str_connInfoHelper = "..prediction threshold?\r\n\r\nThe position vector distance to start error correction. The higher the latency the higher this value should be or it constantly tries to correct errors in prediction, " +
					"of course this depends on the game too.";
			}
			GUILayout.Label(string.Format("Prediction threshold: "));
			str_PredictionThreshold = GUILayout.TextField(str_PredictionThreshold, 10);
			
			try {
				double.Parse(str_PredictionThreshold);
				Mod.m_PredictionThreshold = float.Parse(str_PredictionThreshold);
			} catch {
					
			}		
			GUILayout.EndHorizontal();		
				
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("?",GUILayout.Width(22))) {
	
				str_connInfoHelper = "..time threshold?\r\n\r\nTime difference in milliseconds where we check for error in position. " +
				"If the server time value of a state is too different from the local state time then the error correction " +
				"comparison is highly unreliable and you might try to correct more errors than there really are.";
	
			}
			GUILayout.Label(string.Format("Time threshold: "));
			str_TimeThreshold = GUILayout.TextField(str_TimeThreshold,10);
			
			try {
				double.Parse(str_TimeThreshold);
				Mod.m_TimeThreshold = float.Parse(str_TimeThreshold);
			} catch {
					
			}			
			GUILayout.EndHorizontal();
		}
		
		GUILayout.Label(string.Format("{0} msg/s {1,4:f3} ms", m_MsgRate, m_MsgLatency));
		GUILayout.Label(string.Format("Time Difference : {0,3:f3}", Mod.m_TimeAccuracy));
		GUILayout.Label(string.Format("Prediction Difference : {0,3:f3}", Mod.m_PredictionAccuracy));
		if (Time.time - Mod.m_Timer > 0) {
			m_MsgRate = Mod.m_MsgCounter;
			Mod.m_Timer = Time.time + 1;
			Mod.m_MsgCounter = 0;
			if (m_MsgRate != 0) {
				m_MsgLatency = (Mod.m_MsgLatencyTotal/(double)m_MsgRate)*1000F;
			} else {
				m_MsgLatency = 0;
			}
			Mod.m_MsgLatencyTotal = 0;
		}
	}
	
	void MakeConnInfoHelperWindow(int windowID) {
		
		GUILayout.Label(str_connInfoHelper);
		if (GUILayout.Button ("Close")) {
			str_connInfoHelper = "";	
		}
	}	
	
	// Update is called once per frame
	void Update () {
	
	}
}
