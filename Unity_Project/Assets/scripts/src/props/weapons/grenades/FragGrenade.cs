using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {
	
	public override void Explode(){
		Destroy(this.gameObject);
	}
}
