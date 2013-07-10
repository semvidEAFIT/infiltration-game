using UnityEngine;
using System.Collections.Generic;

public class FireTeam : MonoBehaviour, ICommand {
	
	public GameObject[] soldiers;
	private Point point;
	private FireTeamState state;
	private List<Command> commands;
	
	private Command lastCommand;
	
	// Use this for initialization
	void Start () {
		commands = new List<Command>();
		
		//Go ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit)){
				if(hit.collider.gameObject.layer == 9){
					MoveCommand movec = new MoveCommand(this, hit.point);
					movec.AddICommand(this);
					AddCommand(movec);
					//MoveInLineFormation(hit.point);
					ExecuteCommand();
				} else {
					if(hit.collider.gameObject.layer == 11 && hit.collider.gameObject.tag == "Door"){
						MoveCommand move = new MoveCommand(this, hit.point);
						move.AddICommand(this);
						AddCommand(move);
						
						if(hit.collider.gameObject.GetComponent<Door>().IsLocked()){
							C4Command c4 = new C4Command(this);
							c4.AddICommand(this);
							AddCommand(c4);
						}
						else{
							DoorCommand door = new DoorCommand(this, hit.collider.gameObject);
							door.AddICommand(this);
							AddCommand(new DoorCommand(this, hit.collider.gameObject));
						}
					}
				}
			}
		}
		if(Input.GetMouseButton(1)){
			ExecuteCommand();
		}
		//TODO: For testing purposes.
		if(Input.GetKeyDown(KeyCode.G)){
			AddCommand(new FragGrenadeCommand(this));
		}
		if(Input.GetKeyDown(KeyCode.F)){
			AddCommand(new FlashbangCommand(this));
		}
		if(Input.GetKeyDown(KeyCode.M)){
			AddCommand(new MineCommand(this));
		}
		if(Input.GetKeyDown(KeyCode.E)){
			AddCommand(new C4Command(this));
		}
		if(Input.GetKeyDown(KeyCode.C)){
			AddCommand(new ClaymoreCommand(this));
		}
	}
	
	public void Go(){
		ExecuteCommand();
	}
	
	private void MoveInArrowFormation(Vector3 target){ //en cuña
		Vector3 frontDir = (target - transform.position).normalized;
		Vector3 sideDir = Vector3.Cross(frontDir,Vector3.up).normalized;
		Vector3 soldier1Tar = target + frontDir*2;
		Vector3 soldier2Tar = target + (sideDir-frontDir)*4;
		Vector3 soldier3Tar = target - (sideDir+frontDir)*4;
		try {
			soldiers[0].GetComponent<Soldier>().AddWayPoint(soldier1Tar);
		} catch {
			//dead
		}
		
		try{
			soldiers[1].GetComponent<Soldier>().AddWayPoint(soldier2Tar);
		} catch (System.Exception ex) {
			//dead
		}
		
		try{
			soldiers[2].GetComponent<Soldier>().AddWayPoint(soldier3Tar);
		} catch (System.Exception ex) {
			//dead
		}
	}
	
	private void MoveInLineFormation (Vector3 target){ //en "fila india"
		//Version de Ponce
		/*if (soldiers[1]==null && soldiers[2]!=null){
			soldiers[1]=soldiers[2];
			soldiers[2]=null;
		}
		if (soldiers[0]==null){
			if (soldiers[1]==null){
				if(soldiers[2]==null){
					//all dead
				} else {
					soldiers[0]=soldiers[2];
					soldiers[2]=null;
				}
			} else {
				soldiers[0]=soldiers[1];
				if (soldiers[2]==null){
					soldiers[1]=null;
				} else {
					soldiers[1]=soldiers[2];
					soldiers[2]=null;
				}
			}
		} else {
			soldiers[0].GetComponent<Soldier>().AddWayPoint(target);
			try {
				soldiers[1].GetComponent<Soldier>().Follow = soldiers[0];
			} catch {
				//dead
			}
			try {
				soldiers[2].GetComponent<Soldier>().Follow = soldiers[1];
			} catch {
				//dead
			}
		}*/
		
		Vector3[] path = Level.Instance.Grid.FindPath(soldiers[0].transform.position, target);
		foreach (Vector3 destination in path) {
			foreach (GameObject soldier in soldiers) {
				soldier.GetComponent<Soldier>().AddWayPoint(destination);
			}
		}
	}
	
	public void Move(Vector3 targetPos){
		MoveInLineFormation(targetPos);
	}
	public void AddCommand(Command command){
		if (!commands.Contains(command)){
			commands.Add(command);
			command.AddICommand(this);
		}
	}
	
	public void ExecuteCommand(){
		if (commands.Count > 0){
			lastCommand = commands[0];
			lastCommand.Start();
			CommandStarted(lastCommand);
			commands.RemoveAt(0);
		}
	}

	public Command LastCommand {
		get {
			return this.lastCommand;
		}
		set {
			lastCommand = value;
		}
	}
	
	public void CommandStarted (Command command)
	{
		
	}

	public void CommandEnded (Command command)
	{
		
	}
}
