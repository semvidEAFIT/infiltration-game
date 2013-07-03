using UnityEngine;
using System.Collections;

public class NoiseMaker : MonoBehaviour {
	public LayerMask personLayer;
	public float defaultRadius;
	//private static const
	void MakeNoise(float radius){
		//Debug.Log(LayerMask.NameToLayer("Person"));
		Collider[] hits =  Physics.OverlapSphere(transform.position, radius, personLayer);
		foreach(Collider hit in hits){
			if(hit.gameObject != gameObject){
				Person p = hit.transform.gameObject.GetComponent<Person>();
				if(p != null){
					p.HearNoise(gameObject);
					Debug.Log((p.transform.position - transform.position).sqrMagnitude);
				}
			}
		}
	}
	
	void MakeNoise(){
		MakeNoise(defaultRadius);
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			MakeNoise();
		}
	}
}
