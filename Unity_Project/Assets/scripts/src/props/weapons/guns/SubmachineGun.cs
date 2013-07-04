using UnityEngine;
using System.Collections;

public class SubmachineGun : Gun {
	
	private int bulletsInBurst = 3;
	
	public SubmachineGun(GameObject ownerGameObject, int maxGunAmmo, int gunMagsLeft, float bulletDamage, float accuracyDelta, float shootingForce) : base(ownerGameObject, maxGunAmmo, gunMagsLeft, bulletDamage, accuracyDelta, shootingForce) {
		
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
		Ray ray = new Ray(this.owner.transform.position + this.owner.transform.forward, new Vector3(this.owner.transform.forward.x + deltaX, 0, this.owner.transform.forward.z));
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
		//TODO: PARTICULAS DEL ARMA DISPARANDO
		
	}
}
