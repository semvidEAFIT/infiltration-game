using UnityEngine;
using System.Collections;

public class BreachWindowCommand :  Command, IPersonListener {

	private Vector3 targetPos;
	private Window window;
	private FireTeam executor;
	
	public BreachWindowCommand(FireTeam executor, Vector3 nextPos, Window window) : base(executor){
		this.targetPos = nextPos;
		this.window = window;
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
		window.BreachWindow();
		NotifyCommandEnded();
	}
}
