using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	

public class Person : MonoBehaviour {
	
	private Queue<Vector3> route;
	
	private Vector3 destination;
	private bool moving = false;
	
	private List<Vector3> last;
	public float speed = 3000;
	public float gravity = 200.0f;
	
	private Person following;
	public float distanceFollow = 5.0f;
	
	private CharacterController cc;
	public float distanceSnap = 5.0f;
	private float healthPoints;
	public float initialHealth = 5.0f;
	
	private List<IPersonListener> personListeners;
	
	// Use this for initialization
	public virtual void Start () {
		last = new List<Vector3>();
		personListeners = new List<IPersonListener>();
		last.Add(transform.position);
		route = new Queue<Vector3>();
		destination = Vector3.zero;
		healthPoints = initialHealth;
		/*if(follow != null){//se quita una vez se programe un rehen
			Destination = follow.transform.position;
		}*/
		cc = GetComponent<CharacterController>();
	}
	
	public void AddIPersonListener(IPersonListener iPersonListener){
		if (!personListeners.Contains(iPersonListener)){
			personListeners.Add(iPersonListener);
		}
	}
	
	public void RemoveIPersonListener(IPersonListener iPersonListener){
		personListeners.Remove(iPersonListener);
	}
	
	private void NotifyPersonArrived(){
		foreach (IPersonListener p in personListeners){
			p.Arrived(this);
		}
	}
	
	// Update de Mateo
	/*public virtual void Update () {
		if(follow == null && destination ==  Vector3.zero && route.Count > 0){
			Destination = route[0];
			last.Add(destination);
			route.RemoveAt(0);
		}else if(destination != Vector3.zero && (follow==null || 
			(follow!=null && (follow.transform.position-transform.position).magnitude > distanceFollow))){
			cc.SimpleMove ((destination-transform.position).normalized*speed *Time.deltaTime);
			if((transform.position-destination).magnitude < distanceSnap){
				destination = Vector3.zero;
				//Notify arrived
				NotifyPersonArrived();
			}
			//EN LA SIGUIENTE LINEA distanceFollow ERA ANTES UN '10' (por si algun problema)
		}else if(follow != null && ((destination-transform.position).magnitude < distanceFollow||destination==Vector3.zero)){
			try{
				Destination =  follow.GetComponent<Person>().Last;
				last.Add(destination);
				//Notify arrived
//				NotifyPersonArrived();
		
			}catch{}
		}
		Debug.DrawLine(transform.position + transform.forward,transform.position + transform.forward*10,Color.blue);
//		if(Input.GetMouseButtonDown(0) && follow == null){
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//      		RaycastHit hit;
//        	if(Physics.Raycast(ray, out hit)){
//				AddWayPoint(hit.point);	
//			}
//		}
		
	}*/
	
	public void TakeDamage(float damage){
		healthPoints -= damage;
		
		if (healthPoints <= 0){
			//person dead
			Destroy(this.gameObject);
		}
	}

	
	#region Movement
	
	public Vector3 Destination {
		get {
			return this.destination;
		}
	}
	
	public void AddWayPoint(Vector3 wayPoint){
		route.Enqueue(wayPoint);
	}
	
	/*
	public GameObject Follow {
		get {
			return this.follow;
		}
		set {
			follow = value;
			destination = follow.transform.position;
		}
	}*/
	

	public Vector3 Last {
		get {
			Vector3 ret = last[0];
			last.RemoveAt(0); //TODO: fix for multiple followers
			return ret;
		}
	}
	
	public void Follow(Person p){
		following = p;
		moving = true;
		route.Clear();
	}
	
	public void StopFollowing(){
		following = null;
		moving = false;
	}
	
	public virtual void Update(){
		if(cc.isGrounded){
			
			if(following != null){
				destination = following.transform.position + (transform.position - following.transform.position).normalized * distanceFollow;
			}
			
			if(moving){
				Vector3 distance = (destination-transform.position);
				distance.y = 0;
				if(distance.sqrMagnitude < distanceSnap){
					if(following == null){
						destination = Vector3.zero;		
						moving = false;
					}
				}else{
					cc.SimpleMove(distance.normalized * speed * Time.deltaTime);
					Vector3 lookingPos = destination;
					lookingPos.y = transform.position.y;
					transform.LookAt(lookingPos);
				}
			}else{
				if(route.Count > 0){
					destination = route.Dequeue();
					moving = true;
				}
			}	
		}else{
			cc.SimpleMove(Vector3.down * gravity * Time.deltaTime);			
		}
	}
	
	#endregion
	
	#region Senses
	public virtual void HearNoise(GameObject g){
		StartCoroutine(TurnColor(Color.green));	
	}
	
	IEnumerator TurnColor(Color color){
		Color c = renderer.material.color;
		renderer.material.color = color;
		yield return new WaitForSeconds(3.0f);
		renderer.material.color = c;
	}
	
	public virtual void View(RaycastHit[] gs){
		foreach(RaycastHit hit in gs){
			Debug.DrawRay(transform.position, (hit.transform.position-transform.position).normalized * 20, Color.red);
		}
	}
	#endregion
}
