using UnityEngine;
using System.Collections;

public abstract class Grenade : Explosive {

	public float time;
	public float strength;
	private float timeElapsed;
	private bool exploded=false;
	
	void Start(){
		timeElapsed = 0.0f;
	}

	void Update(){
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time && !exploded){
			Explode();
		}
	}

	public override void Use(){
		Throw(transform.forward, strength);
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(useSounds[Random.Range(0, useSounds.Length)]);
	}

	protected void Throw(Vector3 direction, float strength){
		rigidbody.AddForce(direction * strength);
	}
	
	protected override void Explode ()
	{
		exploded=true;
		
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(activateSounds[Random.Range(0, activateSounds.Length)]);
		
		Collider [] hitColliders = Physics.OverlapSphere(transform.position, AOERadius, layerAffected);
        ApplyDamage(hitColliders);
		
		this.renderer.enabled=false;
		Destroy(this.gameObject, 4);
	}
	
	protected abstract void ApplyDamage(Collider[] inRange);
}