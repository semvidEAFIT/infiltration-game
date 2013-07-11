using UnityEngine;
using System.Collections;

public class WaitPointCommand : Command, IPersonListener {

	private Vector3 targetPos;
	
	public WaitPointCommand(FireTeam executor, Vector3 nextPos) : base(executor){
		this.targetPos = nextPos;
		executor.teammates[0].GetComponent<Soldier>().AddIPersonListener(this);
	}
	
	protected override void Execute ()
	{
		//TODO: ALways in line formation?
		this.FireTeam.Move(targetPos);
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
	
	public void Arrived (Person person)
	{
		//TODO:Notificar llego
	}
}
