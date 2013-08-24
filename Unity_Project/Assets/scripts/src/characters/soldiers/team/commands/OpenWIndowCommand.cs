using UnityEngine;
using System.Collections;

public class OpenWIndowCommand :  Command, IPersonListener {

	private Window window;
	private FireTeam executor;
	
	public OpenWIndowCommand(FireTeam executor, Window window) : base(executor){
		this.window = window;
		this.executor = executor;
		executor.teammates[0].GetComponent<Soldier>().AddIPersonListener(this);
	}
	
	protected override void Execute ()
	{
		//TODO: ALways in line formation?
		//NotifyCommandEnded();
	}
	
	public override bool Ended ()
	{
		//fireTeam.CommandEnded(this);
		return false;
	}
	
	public void Arrived (Person person)
	{
		window.OpenWindow();
		NotifyCommandEnded();
	}
}
