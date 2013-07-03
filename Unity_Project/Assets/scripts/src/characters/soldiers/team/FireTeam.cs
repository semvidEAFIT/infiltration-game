using UnityEngine;
using System.Collections;

public class FireTeam : MonoBehaviour {
	
	public GameObject[] soldiers;
	private Point point;
	private FireTeamState state;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				MoveFireTeamTo(hit.point);
			}
		}
	}
	
	public void MoveFireTeamTo(Vector3 target){
		//en cuña
		soldiers[0].GetComponent<Soldier>().AddWayPoint(target + soldiers[0].transform.forward*3);
		try{
			soldiers[1].GetComponent<Soldier>().AddWayPoint(target - (soldiers[1].transform.forward - soldiers[1].transform.right)*3);
		} catch (System.Exception ex) {
			Debug.LogError(ex.ToString());
		}
		
		try{
			soldiers[2].GetComponent<Soldier>().AddWayPoint(target - (soldiers[2].transform.forward + soldiers[2].transform.right)*3);
		} catch (System.Exception ex) {
			Debug.LogError(ex.ToString());
		}
	}
}
