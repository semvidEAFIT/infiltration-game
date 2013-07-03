using UnityEngine;
using System.Collections;

public class SubmachineGun : Gun {
	
	private GameObject bullet;	
	
	public SubmachineGun(GameObject ownerGameObject, int maxGunAmmo, int gunMagsLeft, float bulletDamage, float accuracyDelta, float shootingForce) : base(ownerGameObject, maxGunAmmo, gunMagsLeft, bulletDamage, accuracyDelta, shootingForce) {
		bullet = Resources.Load("bullet") as GameObject;
	}
	
	public override void Activate(){
		if(bulletsLeft > 0){
			InstantiateBullet();
		} else if(magsLeft > 0) {
			magsLeft--;
			bulletsLeft = maxBullets;
			InstantiateBullet();
		}
		else{
			//TODO: NOTIFICAR QUE NO HAY BALAS.
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
		//TODO:CAMBIAR POR BALA REAL.
		GameObject shotBullet = (GameObject)GameObject.Instantiate(bullet, this.owner.transform.position + this.owner.transform.forward, this.owner.transform.rotation);
		
		shotBullet.rigidbody.AddForce((this.owner.transform.forward.x + deltaX) * this.shootingForce, 0, (this.owner.transform.forward.z) * this.shootingForce);
	}
}
