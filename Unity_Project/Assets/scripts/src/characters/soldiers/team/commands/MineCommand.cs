using UnityEngine;
using System.Collections;

public class MineCommand : Command {

	public MineCommand(FireTeam executor) : base(executor){
	}
	
	protected override void Execute ()
	{
		this.FireTeam.soldiers[0].GetComponent<Soldier>().PlantMine();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
