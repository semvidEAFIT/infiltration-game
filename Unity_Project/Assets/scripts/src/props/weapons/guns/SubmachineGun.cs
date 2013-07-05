using UnityEngine;
using System.Collections;

public class SubmachineGun : Gun {

	private int bulletsInBurst = 3;
	private float bulletSpeed = 1;
	
	private GameObject bullet;

	public SubmachineGun(GameObject ownerGameObject, int maxGunAmmo, int gunMagsLeft, float bulletDamage, float accuracyDelta, float shootingForce) : base(ownerGameObject, maxGunAmmo, gunMagsLeft, bulletDamage, accuracyDelta, shootingForce) {
		bullet = Resources.Load("bullet") as GameObject;
	}

	public override void Fire(){
		for (int i=0; i < bulletsInBurst && bulletsLeft > 0; i++){
			InstantiateBullet();
		}

		if(bulletsLeft <= 0){
			if (magsLeft > 0){
				magsLeft--;
				bulletsLeft = maxBullets;
			} else {
				//TODO: NOTIFICAR QUE NO HAY BALAS.
			}

		}
	}

	private void InstantiateBullet(){
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
				hit.collider.gameObject.GetComponent<Person>().TakeDamage(damage);
			}
			catch{
				//TODO:	OTHERWISE...
			}
		}
		owner.gameObject.GetComponent<NoiseMaker>().MakeNoise();
		
		ParticleSystem particleSystem = owner.gameObject.GetComponent<ParticleSystem>();
		particleSystem.Emit(1);

	}
}