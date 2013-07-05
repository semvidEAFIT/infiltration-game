using UnityEngine;
using System.Collections;

public class FlashbangCommand : Command {

	private FireTeam fireTeam;
	
	public FlashbangCommand(FireTeam executor) : base(executor){
		this.fireTeam = executor;
	}
	
	protected override void Execute ()
	{
		fireTeam.soldiers[0].GetComponent<Soldier>().ThrowFlashbang();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
