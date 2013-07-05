using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	public override void Explode(){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        foreach (Collider c in hitColliders) {
            c.GetComponent<Person>().TakeDamage(damage);
        }
		Destroy(this.gameObject);
	}
}