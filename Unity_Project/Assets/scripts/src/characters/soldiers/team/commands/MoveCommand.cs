using UnityEngine;
using System.Collections;

public class MoveCommand : Command {

	private Vector3 targetPos;
	
	public MoveCommand(FireTeam executor, Vector3 nextPos) : base(executor){
		this.targetPos = nextPos;
	}
	
	protected override void Execute ()
	{
		//TODO: ALways in line formation?
		this.FireTeam.MoveInLineFormation(targetPos);
		NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
}
