using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour {
	
	public Material[] materials;
	public int index=0; //selected material. By default 0. 		¡¡¡¡For testing pourposes set to public!!!!
	
	// Use this for initialization
	void Start () {
		if(materials.Length!=0) //just in case the material is diferent to the one on materials.
			ChangeMaterial(this.index);
	}
	
	// Update is called once per frame
	void Update () {
		ChangeMaterial(this.index); // ¡¡¡¡¡For Testing pourposes only!!!!!!
	}
	
	public void ChangeMaterial(int index){
		this.gameObject.renderer.material=materials[index];
	}
}
