using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : Person {
	
	private List<IItem> items;
	private Gun mainWeapon;
	public int maxGunAmmo;
	public int initialGunMags;
	public float bulletDamage;
	public float accuracyDelta;
	public float shootingForce;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
		mainWeapon = new SubmachineGun(this.gameObject, maxGunAmmo, initialGunMags, bulletDamage, accuracyDelta, shootingForce);
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}
		if(Input.GetKey(KeyCode.A)){
			transform.Translate(new Vector3(1, 0, 0));
		}
	}
	
	private void Shoot(){
		mainWeapon.Fire();
		
		//testing
		this.gameObject.GetComponent<NoiseMaker>().MakeNoise();
	}
}
