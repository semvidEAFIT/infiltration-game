using UnityEngine;
using System.Collections;

[RequireComponent (typeof(NoiseMaker))]
[RequireComponent (typeof(View))]
public class SensesTest : Person {
	
	private NoiseMaker noise;
	private View view;
	public float rotateSpeed;
	
	// Use this for initialization
	void Awake () {
		this.noise = GetComponent<NoiseMaker>();
		this.view = GetComponent<View>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			noise.MakeNoise();
		}
		
		if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
			transform.RotateAround(transform.up, -rotateSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)){
			transform.RotateAround(transform.up, rotateSpeed * Time.deltaTime);
		}
		
		Debug.DrawLine(transform.position + transform.forward,transform.position + transform.forward*10,Color.green);
	}
	
	public override void View(RaycastHit[] gs){
		foreach(RaycastHit h in gs){
			Debug.Log(h.transform.name);
		}
	}
}
