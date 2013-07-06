using UnityEngine;
using System.Collections;

public class C4 : Explosive {

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public override void Use(){
		//TODO: Stick to door/wall/window
	}

	public override void Explode (){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        foreach (Collider c in hitColliders) {
            c.GetComponent<Person>().TakeDamage(damage);
			//TODO: Dañar puertas/ventanas/etc
        }
		Destroy(this.gameObject);
	}
}
