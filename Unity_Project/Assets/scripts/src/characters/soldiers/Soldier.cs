using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : Person {
	
	private List<IItem> items;
	private IItem mainWeapon;
	public int maxGunAmmo;
	public int initialGunMags;
	public float bulletDamage;
	public float accuracyDelta;
	public float shootingForce;
	
	// Use this for initialization
	void Start () {
		mainWeapon = new SubmachineGun(this.gameObject, maxGunAmmo, initialGunMags, bulletDamage, accuracyDelta, shootingForce);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}
	}
	
	private void Shoot(){
		mainWeapon.Activate();
//		float angleDelta = Random.Range(-maxAccuracyAngleDelta, maxAccuracyAngleDelta) * Mathf.Deg2Rad;
//		float deltaX = Mathf.Sin(angleDelta);
//		Ray ray = new Ray(transform.position + transform.forward, new Vector3(transform.forward.x + deltaX, 0, transform.forward.z));
//  		RaycastHit hit;
//		//TODO: IMPLEMENTAR DISTANCIA DE BALAS (TERCER PARAMETRO EN RAYCAST)
//    	if(Physics.Raycast(ray, out hit)){
//			//TODO: SE DEBE DEFINIR TODA LA DINÁMICA DE DAMAGE INFLICTION.
//			try{
//				hit.collider.gameObject.GetComponent<Person>().TakeDamage(damage);
//			}
//			catch{
//				//TODO:	OTHERWISE...
//			}
//		}
//		
//		GameObject shotBullet = (GameObject)GameObject.Instantiate(bullet, transform.forward, transform.rotation);
//		
//		shotBullet.rigidbody.AddForce((transform.forward.x + deltaX) * forceMultiplier, 0, (transform.forward.z) * forceMultiplier);
	}
}
