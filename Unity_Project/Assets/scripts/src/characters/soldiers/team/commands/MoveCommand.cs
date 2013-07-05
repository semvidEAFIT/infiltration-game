using UnityEngine;
using System.Collections;

public class MoveCommand : Command {

	private FireTeam fireTeam;
	private Vector3 targetPos;
	
	public MoveCommand(FireTeam executor, Vector3 nextPos) : base(executor){
		this.fireTeam = executor;
		this.targetPos = nextPos;
	}
	
	protected override void Execute ()
	{
		fireTeam.MoveInLineFormation(targetPos);
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
