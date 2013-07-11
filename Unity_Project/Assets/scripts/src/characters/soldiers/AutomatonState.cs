using UnityEngine;
using System.Collections;

public class AutomatonState {
	
	public void OnSight(RaycastHit[] hits, Automaton au){
		if(au.CurrentTarget == null){
			foreach(RaycastHit hit in hits){
				if(hit.transform.gameObject.tag.Equals("Fireteam")){
					au.CurrentTarget = hit.transform.gameObject;
					break;
				}	
			}
		}
	}
	
	public void OnHear(GameObject source, Automaton au){
		au.transform.LookAt(new Vector3 (source.transform.position.x, au.transform.position.y,source.transform.position.z));
		//au.StartCoroutine("Check", new Vector3 (source.transform.position.x, au.transform.position.y,source.transform.position.z) );
	}
	
	public void OnTakeDamage(Vector3 source, Automaton au){
	}
}
