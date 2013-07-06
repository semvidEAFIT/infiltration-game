using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nodo : MonoBehaviour{
	
	private List<Nodo> neighbors;
	
	void Awake(){
		this.neighbors = new List<Nodo>();
	}
	
	public void FindNeighbors(List<Nodo> nodos){
		foreach (Nodo n in nodos){
			if (n.Equals(this)){
				continue;
			}
      		RaycastHit hit;
        	if(Physics.Raycast(transform.position,n.transform.position - transform.position, out hit)){
				if(hit.collider.gameObject.transform.tag.Equals("Nodo")){
					neighbors.Add(n);
					n.AddNeighbor(this);
				}
			}
		}
	}
	
	public void AddNeighbor(Nodo n){
		neighbors.Add(n);
	}
	
	void Update(){
		foreach(Nodo n in neighbors){
			Debug.DrawLine(transform.position, n.transform.position,Color.blue);
		}
	}
}
