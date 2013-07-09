using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	

public class Person : MonoBehaviour {
	
	private List<Vector3> route;
	private Vector3 destination;
	private List<Vector3> last;
	public float speed;
	public GameObject follow;// se debe popner privado una vez se programe un rehen
	private CharacterController cc;
	public float distanceFollow = 6.0f;
	public float distanceSnap = 10.0f;
	private float healthPoints;
	public float initialHealth = 5.0f;
	
	private List<IPersonListener> personListeners;
	
	// Use this for initialization
	public virtual void Start () {
		last = new List<Vector3>();
		personListeners = new List<IPersonListener>();
		last.Add(transform.position);
		route = new List<Vector3>();
		destination = Vector3.zero;
		healthPoints = initialHealth;
		if(follow != null){//se quita una vez se programe un rehen
			Destination = follow.transform.position;
		}
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
	
	// Update is called once per frame
	public virtual void Update () {
		if(follow == null && destination ==  Vector3.zero && route.Count > 0){
			Destination = route[0];
			last.Add(destination);
			route.RemoveAt(0);
		}else if(destination != Vector3.zero && (follow==null || 
			(follow!=null && (follow.transform.position-transform.position).magnitude > distanceFollow))){
			cc.SimpleMove ((destination-transform.position)*speed);
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
		
	}
	
	public void TakeDamage(float damage){
		healthPoints -= damage;
		
		if (healthPoints <= 0){
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
			this.transform.LookAt(new Vector3(destination.x,1.5f,destination.z));
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
			last.RemoveAt(0); //TODO: fix for multiple followers
			return ret;
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
