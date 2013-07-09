using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	protected override void ApplyDamage(Collider[] inRange){
        foreach (Collider c in inRange) {
            c.GetComponent<Person>().TakeDamage(damage);
        }
	}
}