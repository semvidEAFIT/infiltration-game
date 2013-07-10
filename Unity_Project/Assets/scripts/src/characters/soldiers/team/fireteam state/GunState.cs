using UnityEngine;
using System.Collections;

public abstract class GunState {

	public GunState(){
		
	}
	
	public abstract void OnSight(RaycastHit[] hits, Soldier soldier);
	
	public abstract void OnHear(Soldier soldier, GameObject source);

	public abstract void OnTakeDamage(Soldier soldier, GameObject source);
}
