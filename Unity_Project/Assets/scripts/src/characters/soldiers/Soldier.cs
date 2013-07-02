using UnityEngine;
using System.Collections;

public class Soldier : Person {
	
	public Gun gun;
	public Item item;
	public GameObject bullet;
	public float forceMultiplier;
	public float maxAccuracyAngleDelta;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			shoot();
		}
	}
	
	private void shoot(){
		float angleDelta = Random.Range(-maxAccuracyAngleDelta, maxAccuracyAngleDelta) * Mathf.Deg2Rad;
		float deltaX = Mathf.Sin(angleDelta);
		GameObject shotBullet = (GameObject)GameObject.Instantiate(bullet, transform.forward, transform.rotation);
		shotBullet.rigidbody.AddForce((transform.forward.x + deltaX) * forceMultiplier, 0, (transform.forward.z) * forceMultiplier);
	}
}
