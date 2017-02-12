using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

/*
 * @author SoftRare - www.softrare.eu
 * This is a library class, statically available everywhere. Changes can have significant consequences for performance and overall functionality.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 * Please read the in-Editor documentation for further information on how to use the code of this plugin.
 */
public abstract class uMMO_StaticLibrary {
	
	public static double global_InterpolationBackTime = 0.1;

	public static void killRequiredTaggedScripts(List<Component> componentsToKill) {
		//uses multiple cycles to kill components which are required by others
		
		if (uMMO.get.showDebugHints) {
			Debug.Log("uMMO hint: if you receive several errors (\"Can't remove xxx (Script) because yyy (Script) depends on it\") here, its normal (if not, its normal, too ;)");
			//Debug.Log("<-------------------------------------------------------------------------------------------------------------------------------------------------------");
		}
		int cycles = 0;
		do {
			List<Component> componentsToKillNew = componentsToKill;
			for(int i=0;i<componentsToKill.Count;i++) {
				Component comp = componentsToKill[i];

				try {
					
					GameObject.DestroyImmediate(comp);
				} finally {
					if (comp == null) {
						componentsToKillNew.RemoveAt(i);
					} else { //change order
						componentsToKillNew.Remove(comp);
						componentsToKillNew.Add(comp);
					}
				}
			}
			
			componentsToKill = componentsToKillNew;
			cycles++;
		} while (componentsToKill.Count > 0 && cycles <= 10);		
		
		//Debug.Log("------------------------------------------------------------------------------------------------------------------------------------------------------->");		
	}
	
	public static string ObjectDebugName2ScriptName(string objectDebugName) {
		Match match = Regex.Match(objectDebugName, @".*\((.*\.)?([a-zA-Z0-9\_]+)\)$",RegexOptions.IgnoreCase);
		string key = "";
		
		if (match.Success)
		{
		    // Finally, we get the Group value and display it.
		    key = match.Groups[2].Value;
		}		
		return key;
	}	
	
	public static string ScriptAndNamespace2ScriptName(string objectDebugName) {
		Match match = Regex.Match(objectDebugName, @"(.*\.)?([a-zA-Z0-9\_]+)$",RegexOptions.IgnoreCase);
		string key = "";
		
		if (match.Success)
		{
			// Finally, we get the Group value and display it.
			key = match.Groups[2].Value;
		}		
		return key;
	}		

	public static Component findComponentInChildren(string compName, GameObject parent) {
		Component result = null;
		Component[] components = parent.GetComponentsInChildren<Component>();
		
		
		foreach(Component comp in components) {
			if (comp.gameObject.name == compName) {
				result = comp;
				break;
			}
			
			//Debug.Log("ScriptAndNamespace2ScriptName(comp.GetType().ToString()): "+ScriptAndNamespace2ScriptName(comp.GetType().ToString()));
			if (ScriptAndNamespace2ScriptName(comp.GetType().ToString()) == compName) {
				result = comp;
				break;
			}
		}

		return result;
	}
	
	public static uMMO_NetObject[] getAllNetObjects() {
		return (uMMO_NetObject[])GameObject.FindSceneObjectsOfType(typeof (uMMO_NetObject));
	}
	
	public static List<uMMO_NetObject> getNetObjectsByNetworkPlayer(NetworkPlayer np) {

		uMMO_NetObject[] chars = getAllNetObjects();
		List<uMMO_NetObject> ret = new List<uMMO_NetObject>();
		
		foreach(uMMO_NetObject c in chars) {
			if (c.nplayer == np) {
				ret.Add(c);
			}
		}		
		
		return ret;
	}
	
	public static uMMO_NetObject getNetObjectByNViewID(NetworkViewID nvID) {
		uMMO_NetObject ret = null;
		NetworkView[] nvs = (NetworkView[])GameObject.FindSceneObjectsOfType(typeof (NetworkView));
		
		foreach(NetworkView nv in nvs) {
			if (nv.viewID == nvID) {
				ret = nv.gameObject.GetComponent<uMMO_NetObject>();	
				break;
			}

		}		
		
		return ret;
	}	
	
	public static void switchNetObjectVisibility(uMMO_NetObject ch, bool visible) {
		Renderer[] rs = (Renderer[])ch.gameObject.GetComponentsInChildren<Renderer>();
		
		foreach(Renderer r in rs) {
			r.enabled = visible;
		}		
	}
	
	//--------------------------------------------- Time functions
	
	public static System.DateTime dt_now() {
		return DateTime.Now;	
	}
	
	public static double ts_now() {
		return conv_Date2Timestamp(dt_now());	
	}	
	
	//http://dotnet-snippets.de/dns/unix-timestamp-in-datum-wandeln-SID164.aspx
    public static System.DateTime conv_Timestamp2DateTime (double Timestamp)
    {
        //  gerechnet wird ab der UNIX Epoche
        System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        // den Timestamp addieren           
        dateTime = dateTime.AddSeconds(Timestamp);
		return dateTime;
	}
	
	//http://dotnet-snippets.de/dns/unix-timestamp-in-datum-wandeln-SID164.aspx
    public static string conv_Timestamp2DateStr (double Timestamp)
    {
		
        //  gerechnet wird ab der UNIX Epoche
        System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        // den Timestamp addieren           
        dateTime = dateTime.AddSeconds(Timestamp);
        string Date = dateTime.ToShortDateString() +", "+ dateTime.ToShortTimeString();
        //MessageBox.Show(Date);
        return Date;
    }	
	
	//http://codeclimber.net.nz/archive/2007/07/10/convert-a-unix-timestamp-to-a-.net-datetime.aspx
	public static double conv_Date2Timestamp(DateTime date)
	{
	    DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
	    TimeSpan diff = date - origin;
	    return Math.Floor(diff.TotalSeconds);
	}	
	
}