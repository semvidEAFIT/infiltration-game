using UnityEngine;
using System.Collections;

public class Claymore : Explosive {
	
	private bool activated;
	
	void Start () {
		activated = false;
	}
	
	public override void Use(){
		StartCoroutine("Arm");
		//TODO: Cuadrar para donde mirara la claymore al ponerla?
	}
	
	public IEnumerator Arm(){
		yield return new WaitForSeconds(5);
		activated = true;
	}
	
	protected override void Explode(){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        foreach (Collider c in hitColliders) {
            c.GetComponent<Person>().TakeDamage(damage, transform.position);
        }
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter(Collider c){
		if(activated){
			Explode();
		}
	}
}
