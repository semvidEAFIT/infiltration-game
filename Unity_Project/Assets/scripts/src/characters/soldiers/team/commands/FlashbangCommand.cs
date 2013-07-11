using UnityEngine;
using System.Collections;

public class FlashbangCommand : Command {
	
	public FlashbangCommand(FireTeam executor) : base(executor){
	}
	
	protected override void Execute ()
	{
		this.FireTeam.teammates[0].GetComponent<Soldier>().ThrowFlashbang();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
