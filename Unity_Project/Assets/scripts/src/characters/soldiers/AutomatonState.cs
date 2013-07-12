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
		if(au.CurrentTarget==null && !au.IsChecking){
			if(!au.agresive){
				au.transform.LookAt(new Vector3 (source.transform.position.x, au.transform.position.y,source.transform.position.z));
				au.IsChecking = true;
				au.StartCoroutine("CheckNoise");
			}else{
				au.SaveIndex();
				au.ClearQue(source.transform.position);
				au.StartCoroutine("Going");
			}
		}
	}
	
	public void OnTakeDamage(Vector3 source, Automaton au){
	}
}
