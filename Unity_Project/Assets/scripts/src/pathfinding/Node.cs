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
		Debug.Log(transform.name + "-----------------" + this);
		foreach (Node n in nodos){
			//Debug.Log(n.transform.name);
			if (n.Equals(this)){
				continue;
			}
      		RaycastHit hit;
        	if(Physics.Raycast(new Ray(transform.position, (n.transform.position - transform.position).normalized), out hit, (n.transform.position - transform.position).magnitude)){
				Debug.Log(hit.transform.gameObject.name);
				//Debug.Log((bool)hit.transform.GetComponent<Node>());
				if(n.Equals(hit.collider.GetComponent<Node>())){
					//Debug.Log("Yeah");
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
