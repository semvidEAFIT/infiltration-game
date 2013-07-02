using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {
	
	private int healthPoints;
	public int initialHealth=5;
	
	// Use this for initialization
	void Start () {
		healthPoints=initialHealth;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void takeDamage(int damage){
		healthPoints -= damage;
		
		if (healthPoints<=0){
			//person dead
		}
	}
}
