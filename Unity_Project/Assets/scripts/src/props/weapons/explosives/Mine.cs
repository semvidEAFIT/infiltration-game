using UnityEngine;
using System.Collections;

public class Mine : Explosive {

	public float detectionRadius;
	public float secondsToArm;
	private bool activated;
	
	void Update () {
//		if(activated){
//			Collider [] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, layerAffected);
//			if(hitColliders.Length > 0){
//				Explode();
//			}
//		}
	}
	
	void OnTriggerEnter(Collider c){
		if(activated){
			Explode();
		}
	}
	
	public override void Use(){
		StartCoroutine("Arm");
	}
	
	protected override void Explode(){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        foreach (Collider c in hitColliders) {
            c.GetComponent<Person>().TakeDamage(damage, transform.position);
        }
		Destroy(this.gameObject);	
	}
	
	public void Deactivate(){
		activated = false;
	}
	
	public IEnumerator Arm(){
		yield return new WaitForSeconds(secondsToArm);
		activated = true;
	}
}
