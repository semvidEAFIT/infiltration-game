using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	protected override void Apply(Collider[] inRange){
        foreach (Collider c in inRange) {
            c.GetComponent<Person>().TakeDamage(damage);
        }
	}
}