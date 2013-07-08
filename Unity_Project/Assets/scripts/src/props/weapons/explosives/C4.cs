using UnityEngine;
using System.Collections;

public class C4 : Explosive {
	
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	public override void Use(){
		//TODO: Stick to door/wall/window
		base.Use();
	}

	public override void Explode (){
		base.Explode();
		//TODO: Destroy Door
	}
}
