using UnityEngine;
using System.Collections;

public abstract class Grenade : Item {

	public float AOERadius;
	public float damage;
	public float time;
	public float strength;
	public LayerMask layerAffected;
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

	public override void Activate(){
		Throw(transform.forward, strength);
	}

	public void Throw(Vector3 direction, float strength){
		rigidbody.AddForce(direction * strength);
	}

	public abstract void Explode();
}