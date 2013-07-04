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

	public Gun(GameObject ownerGameObject, int maxGunBullets, int initialMags, float bulletDamage, float gunAccuracyDelta, float gunShootingForce) {
		this.maxBullets = maxGunBullets;
		this.magsLeft = initialMags;
		this.bulletsLeft = maxBullets;
		this.damage = bulletDamage;
		this.owner = ownerGameObject;
		this.accuracyDelta = gunAccuracyDelta;
		this.shootingForce = gunShootingForce;
	}

	public abstract void Fire();
}