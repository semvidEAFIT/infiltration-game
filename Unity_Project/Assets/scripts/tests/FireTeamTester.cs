using UnityEngine;
using System.Collections;

[RequireComponent (typeof (FireTeam))]
public class FireTeamTester : MonoBehaviour {
	
	private FireTeam team;
	// Use this for initialization
	void Start () {
		team = GetComponent<FireTeam>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Debug.Log("Yeah");
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Enviro")){
					//Debug.Log(hit.collider.gameObject.layer+ "vs" + LayerMask.NameToLayer("Enviro"));
					MoveCommand movec = new MoveCommand(team, hit.point);
					movec.AddICommand(team);
					team.AddCommand(movec);
					//MoveInLineFormation(hit.point);
//					ExecuteCommand();
				} else {
					if(hit.collider.gameObject.layer == 11 && hit.collider.gameObject.tag == "Door"){
						MoveCommand move = new MoveCommand(team, hit.point);
						move.AddICommand(team);
						team.AddCommand(move);
						
						if(hit.collider.gameObject.GetComponent<Door>().IsLocked()){
							C4Command c4 = new C4Command(team);
							c4.AddICommand(team);
							team.AddCommand(c4);
						}
						else{
							DoorCommand door = new DoorCommand(team, hit.collider.gameObject);
							door.AddICommand(team);
							team.AddCommand(new DoorCommand(team, hit.collider.gameObject));
						}
					}
				}
			}
		}
		
		if(Input.GetMouseButtonDown(1)){
			team.ExecuteCommand();
		}
		//TODO: For testing purposes.
		if(Input.GetKeyDown(KeyCode.G)){
			team.AddCommand(new FragGrenadeCommand(team));
		}
		if(Input.GetKeyDown(KeyCode.F)){
			team.AddCommand(new FlashbangCommand(team));
		}
		if(Input.GetKeyDown(KeyCode.M)){
			team.AddCommand(new MineCommand(team));
		}
		if(Input.GetKeyDown(KeyCode.E)){
			team.AddCommand(new C4Command(team));
		}
		if(Input.GetKeyDown(KeyCode.C)){
			team.AddCommand(new ClaymoreCommand(team));
		}
	}
}
