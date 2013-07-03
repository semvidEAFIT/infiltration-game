using UnityEngine;
using System.Collections;

public class Soldier : Person {
	
	public Gun gun;
	public Item item;
	public GameObject bullet;
	public float forceMultiplier;
	public float maxAccuracyAngleDelta;
	public int damage;
	
	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
		}
	}
	
	private void Shoot(){
		float angleDelta = Random.Range(-maxAccuracyAngleDelta, maxAccuracyAngleDelta) * Mathf.Deg2Rad;
		float deltaX = Mathf.Sin(angleDelta);
		Ray ray = new Ray(transform.position + transform.forward, new Vector3(transform.forward.x + deltaX, 0, transform.forward.z));
  		RaycastHit hit;
		//TODO: IMPLEMENTAR DISTANCIA DE BALAS (TERCER PARAMETRO EN RAYCAST)
    	if(Physics.Raycast(ray, out hit)){
			//TODO: SE DEBE DEFINIR TODA LA DINÁMICA DE DAMAGE INFLICTION.
			try{
				hit.collider.gameObject.GetComponent<Person>().TakeDamage(damage);
			}
			catch{
				//TODO:	OTHERWISE...
			}
		}
		
		GameObject shotBullet = (GameObject)GameObject.Instantiate(bullet, transform.forward, transform.rotation);
		
		shotBullet.rigidbody.AddForce((transform.forward.x + deltaX) * forceMultiplier, 0, (transform.forward.z) * forceMultiplier);
	}
}
