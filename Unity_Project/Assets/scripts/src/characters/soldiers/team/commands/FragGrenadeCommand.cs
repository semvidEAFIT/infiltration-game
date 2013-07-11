using UnityEngine;
using System.Collections;

public class FragGrenadeCommand : Command {
	
	public FragGrenadeCommand(FireTeam executor) : base(executor){
	}
	
	protected override void Execute ()
	{
		this.FireTeam.teammates[0].GetComponent<Soldier>().ThrowFragGrenade();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
