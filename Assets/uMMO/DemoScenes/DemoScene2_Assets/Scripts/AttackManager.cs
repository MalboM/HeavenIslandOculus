using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class AttackManager : MonoBehaviour {
	
	private static List<Attack> attacks = new List<Attack>();
	
	// Use this for initialization
	void Start () {
		attacks.Add(new Attack(100));
	}
	
	public static Attack getAttack(int no) {
		return attacks[no];
	}
	
}
