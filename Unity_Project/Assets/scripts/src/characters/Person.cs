using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	

public class Person : MonoBehaviour {
	
	private Queue<Vector3> route;
	
	private Vector3 destination;
	private bool moving = false;
	
	private List<Vector3> last;
	public float speed;
	//public GameObject follow;// se debe popner privado una vez se programe un rehen
	private CharacterController cc;
	//public float distanceFollow = 6.0f;
	public float distanceSnap = 10.0f;
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
				NotifyPersonArrived();
		
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
		
		if (healthPoints<=0){
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
	
	public virtual void Update(){
		if(moving){
			cc.SimpleMove((destination-transform.position).normalized * speed * Time.deltaTime);
			if((destination-transform.position).sqrMagnitude < distanceSnap){
				destination = Vector3.zero;		
				moving = false;
			}
		}
		
		if(!moving && route.Count > 0){
			destination = route.Dequeue();
			moving = true;
		}
	}
	
	#endregion
	
	#region Senses
	public void HearNoise(GameObject g){
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
