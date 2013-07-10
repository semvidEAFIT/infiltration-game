using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Person))]
public class PersonMovementTest : MonoBehaviour {
	
	private Person p;
	public LayerMask layer;
	// Use this for initialization
	void Start () {
		p = GetComponent<Person>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			p.AddWayPoint(GetMousePoint());
		}	
	}
	
	private Vector3 GetMousePoint ()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.mainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer)){
			return hit.point;
		}else{
			Debug.Log("El punto de la pantalla no es valido.");
			return new Vector3();
		}
	}
}
