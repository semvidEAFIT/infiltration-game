using UnityEngine;
using System.Collections.Generic;

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
		RaycastHit[] hits = Physics.SphereCastAll( new Ray(transform.position, transform.forward), CalculateRadius(), maxDistance);
		if(hits.Length > 0){
			//Debug.Log("Hit something.");
			List<RaycastHit> hitsInView = new List<RaycastHit>();
			foreach(RaycastHit hit in hits){
				RaycastHit check;
				if(CheckAngle(hit.point) && Physics.Raycast(new Ray(transform.position, hit.point), out check)){
					if(check.transform == hit.transform){
						hitsInView.Add(check);
					}
				}
			}
			
			if(hitsInView.Count > 0){
				foreach(RaycastHit hit in hitsInView){
					//Debug.Log("-'Who's there?' " + "\n-'Just me, the one at " + hit.collider.transform.position + " '");
				}
			}
		}
	}
	
	private float CalculateRadius(){
		return 2 * (maxDistance * Mathf.Tan(viewAngle / 2));
	}
	
	private bool CheckAngle(Vector3 point){
		Vector3 v = point - transform.position;
		float angle = Vector3.Angle(v, transform.forward);
		return angle < (viewAngle / 2);
	}
}
