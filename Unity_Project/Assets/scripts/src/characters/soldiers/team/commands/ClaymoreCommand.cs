using UnityEngine;
using System.Collections;

public class ClaymoreCommand : Command{

	public ClaymoreCommand(FireTeam executor) : base(executor){
	}
	
	protected override void Execute ()
	{
		this.FireTeam.teammates[0].GetComponent<Soldier>().PlantClaymore();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
