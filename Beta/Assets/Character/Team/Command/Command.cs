using UnityEngine;
using System.Collections.Generic;

public abstract class Command {
	
	private FireTeam fireTeam;
	private List<ICommandListener> commandListeners;
	
	public Command(FireTeam fireTeam){
		this.fireTeam = fireTeam;
		commandListeners = new List<ICommandListener>();
	}
	
	public void AddICommand(ICommandListener iCommand){
		if (!commandListeners.Contains(iCommand)){
			commandListeners.Add(iCommand);
		}
	}
	
	public void RemoveICommand(ICommandListener iCommand){
		commandListeners.Remove(iCommand);
	}
	
	private void NotifyCommandStarted(){
		foreach (ICommandListener c in commandListeners){
			c.CommandStarted(this);
		}
	}
	
	public void NotifyCommandEnded(){
		foreach (ICommandListener c in commandListeners){
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
