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
		base.Update();
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time){
			Explode();
		}
	}

	public override void Use(){
		base.Use();
		Throw(transform.forward, strength);
	}

	public void Throw(Vector3 direction, float strength){
		rigidbody.AddForce(direction * strength);
	}
	
	
}