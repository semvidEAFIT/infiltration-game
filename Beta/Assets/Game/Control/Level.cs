using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	private int curFireteam = 0;

	private FireTeam[] teams;
	
	private static Level instance;

	public static Level Instance {
		get {
			return instance;
		}
	}

	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Debug.Log("No puede haber mas de un level.");
			Destroy(this);
		}
		GameObject[] teamsGO = GameObject.FindGameObjectsWithTag("FireTeam");
		teams = new FireTeam[teamsGO.Length];
		int i = 0;
		foreach (GameObject team in teamsGO) {
			teams[i] = team.GetComponent<FireTeam>();
			if(teams[i] == null){
				throw new System.Exception("Hay un objeto con el tag FireTeam sin el componente DUMBASS!");
			}
			i++;
		}
		if(teams.Length == 0){
			throw new System.Exception("No hay ningun FireTeam");
		}
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
	
	public virtual void SetSelectedTeam(FireTeam team){
		this.curFireteam = System.Array.IndexOf(teams, team);
	}
	#endregion
}
