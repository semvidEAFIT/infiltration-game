using UnityEngine;
using System.Collections;

public class Claymore : Explosive {

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public override void Use(){
		//TODO: Cuadrar para donde mirara la claymore al ponerla?
	}

	public override void Explode (){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        foreach (Collider c in hitColliders) {
            c.GetComponent<Person>().TakeDamage(damage);
        }
		Destroy(this.gameObject);
	}
}
