using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class Attack {

	public int damage;
	
	public int getDamage() {
		return damage;	
	}
	
	private Attack() {
		//dummy
	}
	
	public Attack(int damage) {
		this.damage = damage;
	}
	
}
