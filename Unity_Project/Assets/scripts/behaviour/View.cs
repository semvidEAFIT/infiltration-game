using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Person))]
public class View : MonoBehaviour {
	
	private Person viewer;
	
	public float maxDistance;
	public float viewAngle;
	public LayerMask layer;
	
	void Awake () {
		viewer = GetComponent<Person>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit[] hits = Physics.SphereCastAll(new Ray(transform.position, transform.forward), CalculateRadius(), maxDistance, layer);
		if(hits.Length > 0){
			viewer.View(hits);
		}
	}
	
	private float CalculateRadius(){
		return 9.0f;
	}
}
