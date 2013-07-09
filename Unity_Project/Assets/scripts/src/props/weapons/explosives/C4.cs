using UnityEngine;
using System.Collections;

public class C4 : Explosive {
	
	public float time;
	private float timeElapsed;
	
	void Update(){
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time){
			Explode();
		}
	}
	
	public override void Use(){
		//TODO: Stick to door/wall/window
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(useSounds[Random.Range(0, useSounds.Length)]);
	}

	protected override void Explode (){
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
		foreach (Collider c in hitColliders) {
			string t = c.collider.gameObject.tag;
			if(t.Equals("Door")){
				c.gameObject.GetComponent<Door>().Breach();
			} else {
				if(t.Equals("Terrorist") || t.Equals("Fireteam") || t.Equals("Hostages")){
					c.gameObject.GetComponent<Person>().TakeDamage(damage);
				}
			}
		}
	}
}
