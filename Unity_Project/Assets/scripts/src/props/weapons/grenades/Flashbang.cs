using UnityEngine;
using System.Collections;

public class Flashbang : Grenade {

	public override void Explode(){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        
        foreach (Collider c in hitColliders) {
			try{
            	c.gameObject.renderer.material.color = Color.black;
			}
			catch{
				//OTHERWISE
			}
        }
		Destroy(this.gameObject);
	}
}