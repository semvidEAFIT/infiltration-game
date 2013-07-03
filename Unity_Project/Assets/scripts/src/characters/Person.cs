using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	

public class Person : MonoBehaviour {
	
	private List<Vector3> route;
	private Vector3 destination;
	public float speed;
	public GameObject follow;// se debe popner privado una ves se programe un rehen
	private CharacterController cc;
	public float distance;
	private float healthPoints;
	public float initialHealth = 5f;
	
	// Use this for initialization
	void Start () {
		route = new List<Vector3>();
		destination = Vector3.zero;
		healthPoints=initialHealth;
		if(follow != null){//se quita una vez se programe un rehen
			destination = follow.transform.position;
		}
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(follow == null && destination ==  Vector3.zero && route.Count > 0){
			destination = route[0];
			route.RemoveAt(0);
		}else if(destination != Vector3.zero){
			cc.SimpleMove ((destination-transform.position)*speed);
			if((transform.position-destination).magnitude < 10){
				destination = Vector3.zero;
			}
		}else if(follow != null && (follow.transform.position-transform.position).magnitude > distance){
			destination =  follow.GetComponent<Person>().Destination;
		}
		
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				AddWayPoint(hit.point);
			}
		}
		
	}
	
	public void TakeDamage(float damage){
		healthPoints -= damage;
		
		if (healthPoints<=0){
			//person dead
			Destroy(this.gameObject);
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
	

	public GameObject Follow {
		get {
			return this.follow;
		}
		set {
			follow = value;
			destination = follow.transform.position;
		}
	}
	
	#endregion
}
