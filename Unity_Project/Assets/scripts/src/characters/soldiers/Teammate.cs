using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teammate : Soldier {

	private FireTeam fireTeam;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
		
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
	
	public void SetFireTeam(FireTeam ft){
		fireTeam = ft;
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if(hit.collider.gameObject.tag.Equals("Intel")){
			getIntel(hit.collider.gameObject);
		}
	}
	
	public override void View(RaycastHit[] gs)
	{
		base.View(gs);
		fireTeam.Sighted(gs, this);
	}
	
	public override void HearNoise(GameObject g)
	{
		base.HearNoise (g);
		fireTeam.Heard(g, this);
	}
	
	
	public override void TakeDamage (float damage, Vector3 sourcePosition)
	{
		base.TakeDamage(damage, sourcePosition);
		fireTeam.TookDamage(this, sourcePosition);
	}
	
	public void getIntel(GameObject intel){
		GameObject.Destroy(intel);
	}
	
	public override void Blind(float blindForSeconds){
		//Debug.Log("AH! I'M A BLIND SOLDIER!");
	}
}
