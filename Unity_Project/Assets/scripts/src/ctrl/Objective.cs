using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Level.Instance.AddObjective();
	}
	
	void Awake(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ObjectiveDone(){
		Level.Instance.ObjectiveDone();
	}
}
