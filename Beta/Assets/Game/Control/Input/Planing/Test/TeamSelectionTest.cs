using UnityEngine;
using System.Collections;

public class TeamSelectionTest : Level {
	
	public override void SetSelectedTeam (FireTeam team)
	{
		base.SetSelectedTeam (team);
		Debug.Log(team.name);
	}
}
