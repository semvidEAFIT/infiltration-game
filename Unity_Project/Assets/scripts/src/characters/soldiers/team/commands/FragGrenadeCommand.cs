using UnityEngine;
using System.Collections;

public class FragGrenadeCommand : Command {
	
	private FireTeam fireTeam;
	
	public FragGrenadeCommand(FireTeam executor) : base(executor){
		this.fireTeam = executor;
	}
	
	protected override void Execute ()
	{
		fireTeam.soldiers[0].GetComponent<Soldier>().ThrowFragGrenade();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
