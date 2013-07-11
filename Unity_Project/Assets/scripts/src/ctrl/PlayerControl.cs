using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	private static PlayerControl instance;
	public PlayerControl Instance {
		get {
			return instance;
		}
	}	
	
	private GamePhase phase = GamePhase.Planning;

	public GamePhase Phase {
		get {
			return this.phase;
		}
		set {
			phase = value;
		}
	}
	
	void Awake(){
		if(instance == null){
			instance = this;
		}else{
			Debug.Log("No puede haber mas de un PlayerControl.");
			Destroy(this);
		}
		DontDestroyOnLoad(this);
	}
	
	// Use this for initialization
	void Start () {
		if(phase == GamePhase.Planning){
			GetComponent<PlaneationControl>().enabled = true;
			GetComponent<ExecutionControl>().enabled = false;
		}else{
			GetComponent<PlaneationControl>().enabled = false;
			GetComponent<ExecutionControl>().enabled = true;
		}
	}

	public enum GamePhase{
		Planning, Execution 
	}
}
