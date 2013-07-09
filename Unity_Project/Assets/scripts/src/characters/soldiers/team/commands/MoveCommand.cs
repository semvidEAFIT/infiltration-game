using UnityEngine;
using System.Collections;

public class MoveCommand : Command, IPersonListener {

	private Vector3 targetPos;
	
	public MoveCommand(FireTeam executor, Vector3 nextPos) : base(executor){
		this.targetPos = nextPos;
		executor.soldiers[0].GetComponent<Soldier>().AddIPersonListener(this);
	}
	
	protected override void Execute ()
	{
		//TODO: ALways in line formation?
		this.FireTeam.MoveInLineFormation(targetPos);
		//NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
	
	public void Arrived (Person person)
	{
		Debug.Log("Ahi");
		NotifyCommandEnded();
	}
}
