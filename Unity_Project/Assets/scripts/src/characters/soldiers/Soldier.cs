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
	public AudioClip[] burstfireSounds;
	private string alliesTag;
	private FireTeam fireTeam;
	private GameObject currentTarget;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
		mainWeapon = new SubmachineGun(this.gameObject, maxGunAmmo, initialGunMags, bulletDamage, accuracyDelta, shootingForce);
		alliesTag = gameObject.tag;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(currentTarget != null){
			transform.LookAt(currentTarget.transform.position);
			RaycastHit hit;
        	if(Physics.Raycast(transform.position, currentTarget.transform.position - transform.position, out hit)){
				if(hit.transform.gameObject.tag.Equals("Terrorist")){		
					Shoot();
				}else{
					currentTarget = null;
				}
			}
		}
		//FOR TESTING PURPOSES.
//		if(Input.GetKeyDown(KeyCode.Space)){
//			Shoot();
//		}
//		if(Input.GetKeyDown(KeyCode.G)){
//			ThrowFragGrenade();
//		}
//		if(Input.GetKeyDown(KeyCode.F)){
//			ThrowFlashbang();
//		}
//		if(Input.GetKeyDown(KeyCode.C)){
//			PlantC4();
//		}
//		if(Input.GetKey(KeyCode.A)){
//			transform.Translate(new Vector3(1, 0, 0));
//		}
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
	
	public void Shoot(){
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag!=alliesTag){
			mainWeapon.Fire();
		}
	}
	
	public void ThrowFragGrenade(){
		GameObject fragGrenade = GameObject.Instantiate(Resources.Load("fragGrenade"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
		fragGrenade.GetComponent<FragGrenade>().Use();
	}
	
	public void ThrowFlashbang(){
		GameObject flashbang = GameObject.Instantiate(Resources.Load("flashGrenade"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
		flashbang.GetComponent<Flashbang>().Use();
	}
	
	public void PlantC4(){
		GameObject c4 = GameObject.Instantiate(Resources.Load("c4"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
		c4.GetComponent<C4>().Use();
	}
	
	public void PlantMine(){
		GameObject mine = GameObject.Instantiate(Resources.Load("mine"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
		mine.GetComponent<Mine>().Use();
	}
	
	public void PlantClaymore(){
		GameObject claymore = GameObject.Instantiate(Resources.Load("claymore"), this.gameObject.transform.position + this.gameObject.transform.forward, this.gameObject.transform.rotation) as GameObject;
		claymore.GetComponent<Claymore>().Use();
	}
	
	public void getIntel(GameObject intel){
		GameObject.Destroy(intel);
	}
	
	public virtual void Blind(float blindForSeconds){
		//Debug.Log("AH! I'M A BLIND SOLDIER!");
	}
	
	public void playShootSound(){
		this.gameObject.GetComponent<AudioSource>().PlayOneShot(burstfireSounds[Random.Range(0,burstfireSounds.Length)]);
	}

	public GameObject CurrentTarget {
		get {
			return this.currentTarget;
		}
		set {
			currentTarget = value;
		}
	}
}
