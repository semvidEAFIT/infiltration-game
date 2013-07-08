using UnityEngine;
using System.Collections;

public class FragGrenade : Grenade {

	protected override void Apply(Collider[] inRange){
		//this.gameObject.GetComponent<AudioSource>().PlayOneShot(activateSounds[Random.Range(0, activateSounds.Length)]);
        foreach (Collider c in inRange) {
            c.GetComponent<Person>().TakeDamage(damage);
        }
	}
}