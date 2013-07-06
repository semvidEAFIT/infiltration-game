using UnityEngine;
using System.Collections.Generic;

public class FireTeam : MonoBehaviour, ICommand {
	
	public GameObject[] soldiers;
	private Point point;
	private FireTeamState state;
	private List<Command> commands;
	private Command lastCommand;
	
	public float formation;
	
	// Use this for initialization
	void Start () {
		commands = new List<Command>();
		//Go ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      		RaycastHit hit;
        	if(Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer==9){
				AddCommand(new MoveCommand(this, hit.point));
				//MoveInLineFormation(hit.point);
				ExecuteCommand();
			}
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
	
	public void MoveInArrowFormation(Vector3 target){ //en cuña
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
	
	public void MoveInLineFormation (Vector3 target){ //en "fila india"
		if (soldiers[1]==null && soldiers[2]!=null){
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
		}
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
