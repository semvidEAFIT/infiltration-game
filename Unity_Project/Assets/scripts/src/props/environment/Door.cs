using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	public bool locked;

	// Use this for initialization
	void Start () {
		if(locked){
			renderer.material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy(){
		//TODO: Play "DestroyedDoor" animation?
		renderer.material.color = Color.green;
	}
	
	public void Open(){
		//TODO: set "open" material.
	}
	
	
	public bool IsLocked(){
		return locked;
	}
}
