using UnityEngine;
using System.Collections;

public class Automaton : Soldier {
	
	private bool going;
	private Vector3 looking;
	public GameObject[] routinePoints;
	private Vector3[] routine;
	private AutomatonState autoState;
	private int index;
	public bool agresive;
	// Use this for initialization
	public override void Start () {
		base.Start();
		looking = new Vector3 (transform.position.x+transform.forward.x,transform.position.y,transform.position.z+transform.forward.z);
		CanShoot = true;
		autoState = new AutomatonState();
		if(routinePoints.Length==0){
			routine = new Vector3[1];
			routine[0] = transform.position;
		}else{
			routine = new Vector3[routinePoints.Length];
			for(int i = 0; i < routinePoints.Length; i++){
				routine[i] = routinePoints[i].transform.position;
			}
			routinePoints = null;
		}
		index = 0;
		going = false;
	}
	
	// Update is called once per frame
	public override void Update () {
		if(!IsChecking){
			if(routine.Length == 1 && CurrentTarget == null){
				transform.LookAt(looking);
			}
			base.Update();
		}
		if(route.Count==0 && !going){
			Enque();
		}
		
	}
	
	private void Enque(){
		while(index < routine.Length){
			route.Enqueue(routine[index]);
			index++;
		}
		index = 0;
	}
	
	public void SaveIndex(){
		for(int i = 0; i < routine.Length; i++){
			if(destination == routine[i]){
				index = i;
			}
		}
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
	
	public IEnumerator Going(){
		going = true;
		yield return new WaitForSeconds(3.0f);
		going = false;
	}
	
	public IEnumerator CheckNoise(){
		yield return new WaitForSeconds(3.0f);
		IsChecking = false;
	}
	
	
	public void ClearQue(Vector3 p){
		route.Clear();
		destination = p;
	}
	
	public void AddPoint(Vector3 p){
		
	}
	#region gets sets
	
	#endregion
	
	
}