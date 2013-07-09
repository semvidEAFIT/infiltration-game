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
	
	public void Breach(){
		Destroy(this.gameObject);
	}
	
	public void Open(){
		this.transform.RotateAroundLocal(new Vector3(0, 1, 0), (Mathf.PI / 2));
		//TODO: set "open" material.
	}
	
	
	public bool IsLocked(){
		return locked;
	}
}
