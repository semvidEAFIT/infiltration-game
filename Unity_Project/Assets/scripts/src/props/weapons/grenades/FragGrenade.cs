using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	public override void Explode(){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius);
        
        foreach (Collider c in hitColliders) {
			try{
            	c.GetComponent<Person>().TakeDamage(damage);
			}
			catch{
				//OTHERWISE
			}
        }
		Destroy(this.gameObject);
	}
}