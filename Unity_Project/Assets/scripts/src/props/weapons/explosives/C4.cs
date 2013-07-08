using UnityEngine;
using System.Collections;

public class C4 : Explosive {
	
	public override void Use(){
		//TODO: Stick to door/wall/window
	}

	protected override void Explode (){
		//TODO: Destroy Door and damage units behind it using a spherecast
	}
}
