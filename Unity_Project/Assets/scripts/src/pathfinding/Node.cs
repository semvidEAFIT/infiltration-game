using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Hay un problema cuando los nodos estan alineados
public class Node : MonoBehaviour{
	
	private List<Node> neighbors;

	public List<Node> Neighbors {
		get {
			return this.neighbors;
		}
	}	
	
	void Awake(){
		this.neighbors = new List<Node>();
	}
	
	public void FindNeighbors(List<Node> nodos){
		//Debug.Log("Yeah");
		foreach (Node n in nodos){
			if (n.Equals(this)){
				continue;
			}
      		RaycastHit hit;
        	if(Physics.Raycast(transform.position,n.transform.position - transform.position, out hit)){
				//Debug.Log(hit.collider.gameObject.transform.name);
				if(hit.transform.GetComponent<Node>() != null){
					neighbors.Add(n);
					n.AddNeighbor(this);
				}
			}
		}
	}
	
	public void AddNeighbor(Node n){
		neighbors.Add(n);
	}
	
	void Update(){
		foreach(Node n in neighbors){
			Debug.DrawLine(transform.position, n.transform.position,Color.blue);
		}
	}
}
