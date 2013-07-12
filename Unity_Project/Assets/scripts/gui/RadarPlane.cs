using UnityEngine;
using System.Collections;

public class RadarPlane : MonoBehaviour {
	
	public float speed;
	
	// Use this for initialization
	void Start () {
//		StartCoroutine("ResetLine");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
		if(transform.position.y < -7f){
			Debug.Log("caca");
			transform.Translate(0, 0, -15);
		}
	}
	
	IEnumerator ResetLine(){
		yield return new WaitForSeconds(5f);
	}
}
