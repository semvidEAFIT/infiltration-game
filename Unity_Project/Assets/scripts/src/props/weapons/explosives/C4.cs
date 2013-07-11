using UnityEngine;
using System.Collections;

public class C4 : Explosive {
	
	public float time;
	private float timeElapsed;
	private bool exploded=false;
	
	void Update(){
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time && !exploded){
			Explode();
		}
	}
	
	public override void Use(){
		//TODO: Stick to door/wall/window
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(useSounds[Random.Range(0, useSounds.Length)]);
	}

	protected override void Explode (){
		exploded=true;
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(activateSounds[Random.Range(0, activateSounds.Length)]);
		this.renderer.enabled=false;
		Destroy(this.gameObject, 4);
		
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
		foreach (Collider c in hitColliders) {
			string t = c.collider.gameObject.tag;
			if(t.Equals("Door")){
				c.gameObject.GetComponent<Door>().Breach();
			} else {
				if(t.Equals("Terrorist") || t.Equals("Fireteam") || t.Equals("Hostages")){
					c.gameObject.GetComponent<Person>().TakeDamage(damage, transform.position);
				}
			}
		}
	}
}
