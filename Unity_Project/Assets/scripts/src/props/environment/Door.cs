using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	
	public bool locked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDestroy(){
		
	}
	
	public void Open(){
		//TODO: set "open" material.
	}
	
	
	public bool IsLocked(){
		return locked;
	}
}
