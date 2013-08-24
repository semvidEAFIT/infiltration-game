using System;

public interface ICommandListener
{
	void CommandStarted(Command command);
	
	void CommandEnded(Command command);
}
