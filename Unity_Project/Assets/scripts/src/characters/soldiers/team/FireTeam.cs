using UnityEngine;
using System.Collections.Generic;

public class FireTeam : MonoBehaviour, ICommand {
	
	public Teammate[] teammates;
	private Point point;
	private GunState currentGunState;
	private List<Command> commands;
	private Command lastCommand;
	private bool useSilencer;

	public bool UseSilencer {
		get {
			return this.useSilencer;
		}
		set {
			useSilencer = value;
			//TODO:Use silencer
		}
	}	
	void Start () {
		currentGunState = new AgressiveGunState();
		commands = new List<Command>();
		foreach(Teammate t in teammates){
			t.SetFireTeam(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Go(){
		ExecuteCommand();
	}
	
	public void Stop(){
		
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
			teammates[0].AddWayPoint(soldier1Tar);
		} catch {
			//dead
		}
		
		try{
			teammates[1].AddWayPoint(soldier2Tar);
		} catch (System.Exception ex) {
			//dead
		}
		
		try{
			teammates[2].AddWayPoint(soldier3Tar);
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
		
		Vector3[] path = Level.Instance.Grid.FindPath(teammates[0].transform.position, target);
		teammates[1].Follow(teammates[0]);
		teammates[2].Follow(teammates[1]);
		foreach (Vector3 destination in path) {
			teammates[0].AddWayPoint(destination);
		}
	}
	
	#endregion
	
	#region Reaction
	
	public void Sighted(RaycastHit[] hits, Teammate teammate){
		currentGunState.OnSight(hits, teammate, teammates);
	}
	
	public void Heard(GameObject g, Teammate teammate){
		currentGunState.OnHear(g, teammate, teammates);
	}
	
	public void TookDamage(Teammate teammate, Vector3 source){
		currentGunState.OnTakeDamage(source, teammate, teammates);
	}
	
	#endregion

	#region Commands
	public void AddCommand(Command command){
		if (!commands.Contains(command)){
			Debug.Log(command);
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
