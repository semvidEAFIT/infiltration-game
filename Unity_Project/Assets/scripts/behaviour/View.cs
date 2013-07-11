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
		RaycastHit[] hits = Physics.SphereCastAll( new Ray(transform.position - (CalculateRadius()*transform.forward), transform.forward), CalculateRadius(), maxDistance * 3, layer);
		if(hits.Length > 0 && hits[0].transform != this.gameObject.transform){
			List<RaycastHit> hitsInView = new List<RaycastHit>();
			//Debug.Log("hit");
			foreach(RaycastHit hit in hits){
				RaycastHit check;
				//Debug.Log(CheckAngle(hit.point));
				if(CheckAngle(hit.point) && Physics.Raycast(new Ray(transform.position, (hit.point - transform.position).normalized), out check)){
					if(check.transform == hit.transform){
						hitsInView.Add(check);
					}
				}
			}
			
			if(hitsInView.Count > 0){
				viewer.View(hitsInView.ToArray());
			}
		}
	}
	
	private float CalculateRadius(){
		float radius = 2.0f * (maxDistance * Mathf.Tan(Mathf.Deg2Rad*(viewAngle / 2.0f)));
		//Debug.Log(radius);
		return radius;
	}
	
	private bool CheckAngle(Vector3 point){
		Vector3 v = point - transform.position;
		float angle = Mathf.Abs(Vector3.Angle(v, transform.forward));
		bool less = angle < (viewAngle / 2.0f);
		//Debug.Log(angle + "" + less);
		return less;
	}
	
	void OnDrawGizmos(){
		//Gizmos.DrawWireSphere(transform.position, maxDistance);
	}
}
