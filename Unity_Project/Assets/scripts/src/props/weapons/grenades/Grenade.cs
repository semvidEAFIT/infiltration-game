using UnityEngine;
using System.Collections;

public abstract class Grenade : Explosive {

	public float time;
	public float strength;
	private float timeElapsed;

	void Start(){
		timeElapsed = 0f;
	}

	void Update(){
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time){
			Explode();
		}
	}

	public override void Use(){
		Throw(transform.forward, strength);
	}

	public void Throw(Vector3 direction, float strength){
		rigidbody.AddForce(direction * strength);
	}

	public override abstract void Explode();
}