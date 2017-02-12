using UnityEngine;
using System.Collections;
/*
 * @author SoftRare - www.softrare.eu
 * This is a uMMO enum.
 * You may only use and change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public enum uMMO_Architecture {
	Server,
	Client,
	TEST_UnityEditorIsServer_OthersAreClients, //DON'T USE IN PRODUCTION
	TEST_UnityEditorIsClient_OtherIsServer //DON'T USE IN PRODUCTION
}
