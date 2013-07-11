using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Objective))]
public class Hostage : Person {

	private bool saved;
	// Use this for initialization
	public override void Start () {
		base.Start();
		saved = false;
	}
	
	// Update is called once per frame
	public override void Update () {
//		base.Update();
		if(Input.GetKey (KeyCode.D)){
			transform.position += transform.right.normalized;
		}
	}
	
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Exit")){
			GetComponent<Objective>().ObjectiveDone();
			saved = true;
			Destroy(this.gameObject);
		}
    }
	
	void OnDestroy() {
        if(!saved){
			GetComponent<Objective>().ObjectiveFail();
		}
    }
}
