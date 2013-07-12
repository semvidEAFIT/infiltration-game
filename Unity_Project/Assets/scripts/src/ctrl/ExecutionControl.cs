using UnityEngine;
using System.Collections;

public class ExecutionControl : MonoBehaviour {
	
	public FireTeam[] teams;
	private int selectedTeam = 0;
	
	private void NextTeam(){
		selectedTeam++;
		selectedTeam = selectedTeam % teams.Length;
	}
	
	private void PreviousTeam(){
		selectedTeam--;
		selectedTeam = selectedTeam % teams.Length;
	}
	
	private void Go(){
		
	}
	
	private void Stop(){
		
	}
	
	private void UseSilencers(bool on){
		
	}
	
	void OnGUI(){
		
	}
}
