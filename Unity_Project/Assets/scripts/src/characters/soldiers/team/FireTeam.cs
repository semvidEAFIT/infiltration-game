using UnityEngine;
using System.Collections.Generic;

public class FireTeam : MonoBehaviour, ICommand {
	
	public Soldier[] soldiers;
	private Point point;
	private FireTeamState state;
	private List<Command> commands;
	
	private Command lastCommand;
	
	void Start () {
		commands = new List<Command>();
		foreach(Soldier s in soldiers){
			s.SetFireTeam(this);
		}
		//Go ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Go(){
		ExecuteCommand();
	}
	
	#region Movement
	
	public void Move(Vector3 targetPos){
		MoveInLineFormation(targetPos);
	}
	
	private void MoveInArrowFormation(Vector3 target){ //en cuña

		Vector3 frontDir = (target - transform.position).normalized;
		Vector3 sideDir = Vector3.Cross(frontDir,Vector3.up).normalized;
		Vector3 soldier1Tar = target + frontDir*2;
		Vector3 soldier2Tar = target + (sideDir-frontDir)*4;
		Vector3 soldier3Tar = target - (sideDir+frontDir)*4;
		try {
			soldiers[0].AddWayPoint(soldier1Tar);
		} catch {
			//dead
		}
		
		try{
			soldiers[1].AddWayPoint(soldier2Tar);
		} catch (System.Exception ex) {
			//dead
		}
		
		try{
			soldiers[2].AddWayPoint(soldier3Tar);
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
		soldiers[1].Follow(soldiers[0]);
		soldiers[2].Follow(soldiers[1]);
		foreach (Vector3 destination in path) {
			soldiers[0].AddWayPoint(destination);
		}
	}
	
	#endregion
	
	#region Reaction
	
	public void Sighted(RaycastHit[] hits, Soldier soldier){
		//TODO: call state
	}
	
	public void Heard(GameObject g, Soldier soldier){
		//TODO: call state
	}
	
	#endregion

	#region Commands
	public void AddCommand(Command command){
		if (!commands.Contains(command)){
			commands.Add(command);
			command.AddICommand(this);
		}
	}
	
	public void ExecuteCommand(){
		if (commands.Count > 0){
			lastCommand = commands[0];
			commands.RemoveAt(0);
			lastCommand.Start();
			CommandStarted(lastCommand);
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
		ExecuteCommand();
	}
	#endregion
}
