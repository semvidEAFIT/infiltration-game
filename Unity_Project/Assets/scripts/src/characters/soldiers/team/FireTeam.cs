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
				MoveInArrowFormation(hit.point);
			}
		}
	}
	
	public void MoveInArrowFormation(Vector3 target){ //en cuña
		Vector3 frontDir = (target - transform.position).normalized;
		Vector3 sideDir = Vector3.Cross(frontDir,Vector3.up).normalized;
		Vector3 soldier1Tar = target + frontDir*2;
		Vector3 soldier2Tar = target + (sideDir-frontDir)*4;
		Vector3 soldier3Tar = target - (sideDir+frontDir)*4;
		
		soldiers[0].GetComponent<Soldier>().AddWayPoint(soldier1Tar);
		try{
			soldiers[1].GetComponent<Soldier>().AddWayPoint(soldier2Tar);
		} catch (System.Exception ex) {
			Debug.LogError(ex.ToString()); //Si esta muerto
		}
		
		try{
			soldiers[2].GetComponent<Soldier>().AddWayPoint(soldier3Tar);
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
