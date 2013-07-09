using UnityEngine;
using System.Collections;

public class SubmachineGun : Gun {

	private int bulletsInBurst = 3;
	private float bulletSpeed = 50;
	private GameObject bullet;

	public SubmachineGun(GameObject ownerGameObject, int maxGunAmmo, int gunMagsLeft, float bulletDamage, float accuracyDelta, float shootingForce) : base(ownerGameObject, maxGunAmmo, gunMagsLeft, bulletDamage, accuracyDelta, shootingForce) {
		bullet = Resources.Load("bullet") as GameObject;
	}

	public override void Fire(){
		for (int i=0; i < bulletsInBurst && bulletsLeft > 0; i++){
			InstantiateBullet(bulletSpeed);
			owner.gameObject.GetComponent<Soldier>().playShootSound();
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

	
}