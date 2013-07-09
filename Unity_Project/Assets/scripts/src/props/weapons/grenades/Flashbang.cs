using UnityEngine;
using System.Collections;

public class Flashbang : Grenade {
	
	public float secondsBlinded;
	
	protected override void Apply(Collider[] inRange){
        foreach (Collider c in inRange) {
			if(c.gameObject.GetComponent<Soldier>() != null){
				c.gameObject.GetComponent<Soldier>().Blind(secondsBlinded);
			}
			else{
				if(c.gameObject.GetComponent<Automaton>() != null){
					c.gameObject.GetComponent<Automaton>().Blind(secondsBlinded);
				}
				else{
					//Debug.Log("Shits b null");
				}
			}
        }
	}
}