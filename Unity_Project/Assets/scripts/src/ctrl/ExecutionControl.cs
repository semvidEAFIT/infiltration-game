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
		if(selectedTeam == teams.Length){
			foreach (FireTeam team in teams) {
				team.Go();
			}
		}else{
			teams[selectedTeam].Go();
		}
	}
	
	private void Stop(){
		if(selectedTeam == teams.Length){
			foreach (FireTeam team in teams) {
				team.Stop();
			}
		}else{
			teams[selectedTeam].Stop();
		}
	}
	
	private void UseSilencers(bool on){
		if(selectedTeam == teams.Length){
			foreach (FireTeam team in teams) {
				team.UseSilencer = on;
			}
		}else{
			teams[selectedTeam].UseSilencer = on;
		}
	}
	
	void OnGUI(){
		
	}
}
