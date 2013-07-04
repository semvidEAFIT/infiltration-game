using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {
	
	public Item(){
		
	}
	
	public virtual void Start();
	
	public virtual void Update();
	
	public abstract void Activate();
}
