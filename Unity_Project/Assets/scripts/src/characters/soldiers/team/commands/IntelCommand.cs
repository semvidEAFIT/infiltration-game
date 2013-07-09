using UnityEngine;
using System.Collections;

public class IntelCommand : Command, IPersonListener {

	private Vector3 targetPos;
	private GameObject intel;
	private FireTeam executor;
	
	public IntelCommand(FireTeam executor, Vector3 nextPos, GameObject intel) : base(executor){
		this.targetPos = nextPos;
		this.intel = intel;
		this.executor = executor;
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
		executor.soldiers[0].GetComponent<Soldier>().getIntel(intel);
		NotifyCommandEnded();
	}
}
