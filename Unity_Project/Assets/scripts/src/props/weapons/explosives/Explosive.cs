using UnityEngine;
using System.Collections;

public abstract class Explosive : Item {
	
	public float AOERadius;
	public float damage;
	public LayerMask layerAffected;
	
	public AudioClip[] useSounds;
	public AudioClip[] activateSounds;
	
	protected bool exploded;
	
	void Start(){
		exploded=false;
	}

	protected virtual void Update(){
		if (exploded==true && gameObject.GetComponent<AudioSource>().isPlaying==false){
			Destroy(this.gameObject);
		}
	}

	public override void Use(){
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(useSounds[Random.Range(0, useSounds.Length)]);
	}

	public virtual void Explode(){
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(activateSounds[Random.Range(0, activateSounds.Length)]);
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        foreach (Collider c in hitColliders) {
            c.GetComponent<Person>().TakeDamage(damage);
        }
		exploded=true;
	}
}
