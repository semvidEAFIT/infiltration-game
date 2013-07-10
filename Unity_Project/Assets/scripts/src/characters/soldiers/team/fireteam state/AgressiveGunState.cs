using UnityEngine;
using System.Collections;

public class AgressiveGunState : GunState {
	
	#region implemented abstract members of GunState
	public override void OnSight (RaycastHit[] hits, Soldier soldier, Soldier[] team)
	{
		if(soldier.CurrentTarget == null){
			foreach(RaycastHit hit in hits){
				if(hit.transform.gameObject.tag.Equals("Terrorist")){
					soldier.CurrentTarget = hit.transform.gameObject;
					break;
				}	
			}
		}else{
			foreach(Soldier tsol in team){
				if(tsol != soldier && tsol.CurrentTarget == null && !tsol.IsChecking){
					tsol.transform.LookAt(soldier.CurrentTarget.transform.position);
				}
			}
		}
	}

	public override void OnHear (GameObject source, Soldier soldier, Soldier[] team)
	{
		foreach(Soldier tSoldier in team){
			if(tSoldier != soldier && soldier.CurrentTarget == tSoldier.CurrentTarget){
				soldier.IsChecking=true;
				soldier.transform.LookAt(new Vector3(source.transform.position.x,soldier.transform.position.y,source.transform.position.z));
				soldier.CurrentTarget = null;
			}
		}
	}

	public override void OnTakeDamage (Vector3 source, Soldier soldier, Soldier[] team)
	{
		Debug.Log("damage");
		foreach(Soldier tSoldier in team){
			if(tSoldier != soldier && soldier.CurrentTarget == tSoldier.CurrentTarget){
				soldier.transform.LookAt(new Vector3 (source.x, soldier.transform.position.y, source.z));
				soldier.CurrentTarget = null;
			}
		}
	}
	#endregion
}
