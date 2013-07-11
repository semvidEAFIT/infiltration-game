using UnityEngine;
using System.Collections;

public class Automaton : Soldier {
	
	
	private AutomatonState autoState;
	// Use this for initialization
	public override void Start () {
		base.Start();
		CanShoot = true;
		autoState = new AutomatonState();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
	
	public override void Blind(float blindForSeconds){
		//Debug.Log("AH! I'M A BLIND TERRORIST!");	
	}
	
	public override void HearNoise (GameObject g)
	{
		base.HearNoise (g);
		autoState.OnHear(g,this);
	}
	
	public override void TakeDamage (float damage, Vector3 sourcePosition)
	{
		base.TakeDamage (damage, sourcePosition);
	}
	
	public override void View(RaycastHit[] gs)
	{
		base.View (gs);
		autoState.OnSight(gs,this);
	}
	
	#region gets sets
	
	#endregion
	
	
}