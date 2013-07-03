using UnityEngine;
using System.Collections.Generic;

public abstract class Command {
	
	private FireTeam fireTeam;
	private List<ICommand> commandListeners;
	
	public Command(FireTeam fireTeam){
		this.fireTeam = fireTeam;
		commandListeners = new List<ICommand>();
	}
	
	public void AddICommand(ICommand iCommand){
		if (!commandListeners.Contains(iCommand)){
			commandListeners.Add(iCommand);
		}
	}
	
	public void RemoveICommand(ICommand iCommand){
		commandListeners.Remove(iCommand);
	}
	
	private void NotifyCommandStarted(){
		foreach (ICommand c in commandListeners){
			c.CommandStarted(this);
		}
	}
	
	void NotifyCommandEnded(){
	foreach (ICommand c in commandListeners){
			c.CommandEnded(this);
		}
	}
	
	public void Start(){
		NotifyCommandStarted();
		Execute();
	}
	
	protected abstract void Execute();
	
	public abstract bool Ended();
	
	public FireTeam FireTeam {
		get {
			return this.fireTeam;
		}
	}
}
