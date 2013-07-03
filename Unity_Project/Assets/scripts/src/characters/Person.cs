using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	

public class Person : MonoBehaviour {
	
	private List<Vector3> route;
	private Vector3 destination;
	private List<Vector3> last;
	public float speed;
	public GameObject follow;// se debe popner privado una ves se programe un rehen
	private CharacterController cc;
	public float distance;
	private int healthPoints;
	public int initialHealth=5;
	
	// Use this for initialization
	public virtual void Start () {
		last = new List<Vector3>();
		last.Add(transform.position);
		route = new List<Vector3>();
		destination = Vector3.zero;
		healthPoints = initialHealth;
		if(follow!=null){//se quita una ves se programe un rehen
			Destination = follow.transform.position;
		}
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(follow == null && destination ==  Vector3.zero && route.Count > 0){
			Destination = route[0];
			last.Add(destination);
			route.RemoveAt(0);
		}else if(destination != Vector3.zero && (follow==null || 
			(follow!=null && (follow.transform.position-transform.position).magnitude > distance))){
			cc.SimpleMove ((destination-transform.position)*speed);
			if((transform.position-destination).magnitude < 10){
				destination = Vector3.zero;
			}
		}else if(follow != null && ((destination-transform.position).magnitude < 10||destination==Vector3.zero)){
			try{
				Destination =  follow.GetComponent<Person>().Last;
				last.Add(destination);
		
			}catch{}
		}
		Debug.DrawLine(transform.position + transform.forward,transform.position + transform.forward*10,Color.blue);
		if(Input.GetMouseButtonDown(0) && follow == null){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				AddWayPoint(hit.point);	
			}
		}
		
	}
	
	public void TakeDamage(int damage){
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
			this.transform.LookAt(destination);
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
	

	public Vector3 Last {
		get {
			Vector3 ret = last[0];
			last.RemoveAt(0);
			return ret;
		}
	}
	#endregion
}
