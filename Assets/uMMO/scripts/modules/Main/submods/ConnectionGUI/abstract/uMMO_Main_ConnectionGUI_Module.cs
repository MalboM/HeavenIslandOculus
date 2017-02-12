using UnityEngine;
using System.Collections;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO module class. Read in-Editor documentation for more info. Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public abstract class uMMO_Main_ConnectionGUI_Module : uMMO_Main_Module {
	
	public string connectToIP = "127.0.0.1";
	public int connectPort = 33334;
	
	
	protected abstract void OnGUI();
}
