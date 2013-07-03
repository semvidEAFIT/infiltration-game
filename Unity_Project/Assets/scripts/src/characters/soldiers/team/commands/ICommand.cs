using System;

public interface ICommand
{
	void CommandStarted(Command command);
	
	void CommandEnded(Command command);
}
