using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Window : MonoBehaviour {
	
	public List<AudioClip> breakSound;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OpenWindow(){
		Destroy(this.gameObject);
	}
	
	public void BreachWindow(){
		//TODO: agregar particulas, sonido y delay
		Destroy(this.gameObject);
	}
}
