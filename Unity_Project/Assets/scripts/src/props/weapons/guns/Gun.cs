using UnityEngine;
using System.Collections;

public abstract class Gun {

	public GameObject owner;
	public int maxBullets;
	public int magsLeft;
	public int bulletsLeft;
	public float damage;
	public float accuracyDelta;
	public float shootingForce;
	private ParticleEmitter particleSystem;
	
	public Gun(GameObject ownerGameObject, int maxGunBullets, int initialMags, float bulletDamage, float gunAccuracyDelta, float gunShootingForce) {
		this.maxBullets = maxGunBullets;
		this.magsLeft = initialMags;
		this.bulletsLeft = maxBullets;
		this.damage = bulletDamage;
		this.owner = ownerGameObject;
		this.accuracyDelta = gunAccuracyDelta;
		this.shootingForce = gunShootingForce;
		this.particleSystem = owner.gameObject.GetComponentInChildren<ParticleEmitter>();
	}

	public abstract void Fire();
	
	protected void InstantiateBullet(float bulletSpeed){
		bulletsLeft--;
		float angleDelta = Random.Range(-this.accuracyDelta, this.accuracyDelta) * Mathf.Deg2Rad;
		float deltaX = Mathf.Sin(angleDelta);
		Vector3 dir = new Vector3(this.owner.transform.forward.x + deltaX, 0, this.owner.transform.forward.z);
		Ray ray = new Ray(this.owner.transform.position + this.owner.transform.forward, dir);
  		RaycastHit hit;
		//TODO: IMPLEMENTAR DISTANCIA DE BALAS (TERCER PARAMETRO EN RAYCAST)
    	if(Physics.Raycast(ray, out hit)){
			//TODO: SE DEBE DEFINIR TODA LA DINÁMICA DE DAMAGE INFLICTION (SEGUIRA CON COLLIDERS??).
			try{
				hit.collider.gameObject.GetComponent<Person>().TakeDamage(damage, owner.transform.position);
			}
			catch{
				//TODO:	OTHERWISE...
			}
		}
		owner.gameObject.GetComponent<NoiseMaker>().MakeNoise();
		particleSystem.Emit(owner.transform.position, dir * bulletSpeed, 1, 5, Color.black);
	}
}