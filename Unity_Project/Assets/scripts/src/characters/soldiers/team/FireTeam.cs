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
			//Si esta muerto
		}
		
		try{
			soldiers[2].GetComponent<Soldier>().AddWayPoint(soldier3Tar);
		} catch (System.Exception ex) {
			//Si tambien esta muerto
		}
	}
	
	public void MoveInLineFormation (Vector3 target){ //en "fila india"
		try{
			soldiers[0].GetComponent<Soldier>().AddWayPoint(target);
		} catch {
			//dead
		}
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
