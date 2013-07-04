using UnityEngine;
using System.Collections;

public abstract class Grenade : Item {
	
	public float AOERadius;
	public float damage;
	public float time;
	public float throwDistance;
	private float timeElapsed;
	
	public virtual void Start(){
		timeElapsed = 0f;
	}
	
	public virtual void Update(){
		timeElapsed += Time.deltaTime;
		if(timeElapsed >= time){
			Explode();
		}
	}
	
	public virtual void Activate(){
		rigidbody.AddForce(transform.forward.x, 0, transform.forward.z);
	}
	
	public abstract void Explode();
}
