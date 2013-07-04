using UnityEngine;
using System.Collections;

public abstract class Grenade : Item {
	
	public float AOERadius;
	public float damage;
	public float time;
	public float strenght;
	private float timeElapsed;
	
	void Start(){
		timeElapsed = 0f;
	}
	
	void Update(){
		timeElapsed += Time.deltaTime;
		Debug.Log(timeElapsed);
		if(timeElapsed >= time){
			Explode();
		}
	}
	
	public override void Activate(){
		Throw(transform.forward, strenght);
	}
	
	public void Throw(Vector3 direction, float strenght){
		rigidbody.AddForce(direction * strenght);
	}
	
	public abstract void Explode();
}
