using UnityEngine;
using System.Collections;

public class FireTeam : MonoBehaviour {
	
	public GameObject[] soldiers;
	private Point point;
	private FireTeamState state;
	
	public float formation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				MoveInLineFormation(hit.point);
			}
		}
	}
	
	public void MoveInArrowFormation(Vector3 target){ //en cuña
		soldiers[0].GetComponent<Soldier>().AddWayPoint(target + soldiers[0].transform.forward*3);
		try{
			soldiers[1].GetComponent<Soldier>().AddWayPoint(target - (soldiers[1].transform.forward - soldiers[1].transform.right)*3);
		} catch (System.Exception ex) {
			Debug.LogError(ex.ToString()); //Si esta muerto
		}
		
		try{
			soldiers[2].GetComponent<Soldier>().AddWayPoint(target - (soldiers[2].transform.forward + soldiers[2].transform.right)*3);
		} catch (System.Exception ex) {
			Debug.LogError(ex.ToString());//Si tambien esta muerto
		}
	}
	
	public void MoveInLineFormation (Vector3 target){ //en "fila india"
		soldiers[0].GetComponent<Soldier>().AddWayPoint(target);
		try {
			soldiers[1].GetComponent<Soldier>().Follow = soldiers[0];
		} catch {
			//Si lo mataron :(
		}
		try {
			soldiers[2].GetComponent<Soldier>().Follow = soldiers[1];
		} catch {
			//si no existe mas
		}
	}
}
