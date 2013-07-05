using UnityEngine;
using System.Collections;

public abstract class Explosive : Item {
	
	public float AOERadius;
	public float damage;
	public LayerMask layerAffected;
	
	void Start(){
	}

	void Update(){
	}

	public override void Activate(){
	}

	public abstract void Explode();
}
