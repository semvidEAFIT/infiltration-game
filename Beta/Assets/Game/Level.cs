using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	private int curFireteam = 0;

	private FireTeam[] teams;
	
	void OnAwake(){
		//TODO: assign fireteams
	}
	
	void Start () {
		
	}
	
	
	void Update () {
		
	}
	
	#region FireTeam Selection
	public void NextTeam(){
		
	}
	
	public void PreviousTeam(){
		
	}
	
	public void SetSelectedTeam(FireTeam team){
		this.curFireteam = System.Array.IndexOf(teams, team);
	}
	#endregion
}
