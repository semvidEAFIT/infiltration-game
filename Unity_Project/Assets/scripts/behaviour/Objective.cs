using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {
	
	void Start(){
		Level.Instance.AddObjective();		
	}
	
	public void ObjectiveDone(){
		Level.Instance.ObjectiveDone();
	}
	
	void OnDestroy(){
		ObjectiveDone();
	}
}
