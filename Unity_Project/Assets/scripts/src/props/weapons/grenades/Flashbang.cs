using UnityEngine;
using System.Collections;

public class Flashbang : Grenade {
	
	public float secondsBlinded;
	
	public override void Explode(){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        
        foreach (Collider c in hitColliders) {
			if(c.gameObject.GetComponent<Soldier>() != null){
				c.gameObject.GetComponent<Soldier>().Blind(secondsBlinded);
			}
			else{
				if(c.gameObject.GetComponent<Automaton>() != null){
					c.gameObject.GetComponent<Automaton>().Blind(secondsBlinded);
				}
				else{
					//Debug.Log("Shits b null");
				}
			}
        }
		Destroy(this.gameObject);
	}
}