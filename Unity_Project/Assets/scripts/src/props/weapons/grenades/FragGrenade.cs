using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	protected override void ApplyDamage(Collider[] inRange){
        foreach (Collider c in inRange) {
		Debug.Log("damage");
            c.GetComponent<Person>().TakeDamage(damage,transform.position);
        }
	}
}