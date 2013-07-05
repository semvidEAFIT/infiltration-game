using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : Person {
	
	public List<Item> items;
	private Gun mainWeapon;
	public int maxGunAmmo;
	public int initialGunMags;
	public float bulletDamage;
	public float accuracyDelta;
	public float shootingForce;
	
	public LayerMask allies;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
		mainWeapon = new SubmachineGun(this.gameObject, maxGunAmmo, initialGunMags, bulletDamage, accuracyDelta, shootingForce);
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		//FOR TESTING PURPOSES.
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}
		if(Input.GetKeyDown(KeyCode.G)){
			GameObject grenade = GameObject.Instantiate(Resources.Load("fragGrenade"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
			grenade.GetComponent<FragGrenade>().Activate();
		}
		if(Input.GetKeyDown(KeyCode.F)){
			GameObject grenade = GameObject.Instantiate(Resources.Load("flashGrenade"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
			grenade.GetComponent<Flashbang>().Activate();
		}
		if(Input.GetKey(KeyCode.A)){
			transform.Translate(new Vector3(1, 0, 0));
		}
	}
	
	private void Shoot(){
		if (!Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, allies.value)) {
			mainWeapon.Fire();
		}
	}
	
	public virtual void Blind(float blindForSeconds){
		//Debug.Log("AH! I'M A BLIND SOLDIER!");
	}
}
