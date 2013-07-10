using UnityEngine;
using System.Collections;

public class AgressiveGunState : GunState {
	
	#region implemented abstract members of GunState
	public override void OnSight (RaycastHit[] hits, Soldier soldier)
	{ 
		if(soldier.CurrentTarget != null){
			foreach(RaycastHit hit in hits){
				if(hit.transform.gameObject.tag.Equals("Terrorist")){
					soldier.CurrentTarget = hit.transform.gameObject;
					break;
				}	
			}
		}
	}

	public override void OnHear (Soldier soldier, GameObject source)
	{
		throw new System.NotImplementedException ();
	}

	public override void OnTakeDamage (Soldier soldier, GameObject source)
	{
		throw new System.NotImplementedException ();
	}
	#endregion
}
