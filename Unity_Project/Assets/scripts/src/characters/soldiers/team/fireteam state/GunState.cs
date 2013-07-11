using UnityEngine;
using System.Collections;

public abstract class GunState {
	
	public GunState(){
	}
	
	public abstract void OnSight(RaycastHit[] hits, Soldier soldier, Soldier[] teammates);
	
	public abstract void OnHear(GameObject source, Soldier soldier, Soldier[] team);

	public abstract void OnTakeDamage(Vector3 source, Soldier soldier, Soldier[] team);
}
