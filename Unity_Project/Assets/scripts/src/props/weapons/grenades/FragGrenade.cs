using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	public override void Start () {
		
	}
	
	public override void Update () {
	
	}
	
	public override void Activate(){
		
	}
	
	public override void Explode(){
		Destroy(this.gameObject);
	}
}
