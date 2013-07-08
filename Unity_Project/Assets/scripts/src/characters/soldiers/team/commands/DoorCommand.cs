using UnityEngine;
using System.Collections;

public class DoorCommand : Command {

	private GameObject door;
	
	public DoorCommand(FireTeam executor, GameObject doorGO) : base(executor){
		this.door = doorGO;
	}
	
	protected override void Execute ()
	{
		door.GetComponent<Door>().Open();
	}
	
	public override bool Ended ()
	{
		return false;
	}
}
