using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	

public class Person : MonoBehaviour {
	
	private List<Vector3> route;
	private Vector3 destination;
	public float speed;
	// Use this for initialization
	void Start () {
		route = new List<Vector3>();
		destination = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if(destination ==  Vector3.zero && route.Count > 0){
			destination = route[0];
			route.RemoveAt(0);
		}else if(destination != Vector3.zero){
			GetComponent<CharacterController>().SimpleMove ((destination-transform.position)*speed);
			if((transform.position-destination).magnitude < 10){
				destination = Vector3.zero;
			}
		}
		
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				AddWayPoint(hit.point);
			}
		}
		
	}
	
	public void AddWayPoint(Vector3 point){
		route.Add(point);
	}
	
	#region
	
	public Vector3 Destination {
		get {
			return this.destination;
		}
		set {
			destination = value;
		}
	}
	
	#endregion
}
