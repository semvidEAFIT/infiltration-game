using UnityEngine;
using System.Collections;

public class C4Command : Command{

	public C4Command(FireTeam executor) : base(executor){
	}
	
	protected override void Execute ()
	{
		this.FireTeam.teammates[0].GetComponent<Soldier>().PlantC4();
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
