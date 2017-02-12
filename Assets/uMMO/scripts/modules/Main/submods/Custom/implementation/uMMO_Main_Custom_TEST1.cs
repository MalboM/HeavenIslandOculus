using UnityEngine;
using System.Collections;

public class uMMO_Main_Custom_TEST1 : uMMO_Main_Custom_Module {

	void OnServerInitialized() {
		print ("custom TEST module output on successfull server start.");
	}
	
	void  OnConnectedToServer (){
		print ("custom TEST module output on successfull connection to server.");	
	}
}
