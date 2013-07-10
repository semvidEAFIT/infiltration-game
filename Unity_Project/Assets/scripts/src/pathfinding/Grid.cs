using UnityEngine;
using System.Collections.Generic;
using PriorityQueueDemo;

public class Grid{
	
	private List<Node> nodos;
	private Dictionary<Node, int> node2int;
	private float[,] adj;
	
	public Grid(List<Node> nodos){
		node2int = new Dictionary<Node, int>(nodos.Count);
		this.nodos = nodos;
		List<Node> unSeen = new List<Node>(nodos);
		for(int i = 0; i < nodos.Count; i++){
			Node n = nodos[i];
			unSeen.Remove(n);
			n.FindNeighbors(unSeen);
			node2int.Add(n, i);
		}
		MakeAdj();
	}

	public void MakeAdj ()
	{
		adj = new float[nodos.Count, nodos.Count];
		
		for(int i = 0; i < nodos.Count; i++){
			for (int j = 0; j < nodos.Count; j++) {
				adj[i, j] = Mathf.Infinity;
			}
		}
		
		foreach(Node n in nodos){
			adj[node2int[n], node2int[n]] = 0;
			foreach (Node m in n.Neighbors) {
				float distance = Mathf.Abs((m.transform.position - n.transform.position).magnitude); //No usar la sqrt
				adj[node2int[n], node2int[m]] = distance;
				adj[node2int[m], node2int[n]] = distance;
			}	
		}
	}
	
	public Node GetClosestNode(Vector3 point){
		Node closest = nodos[0];
		float sqrDistance = Mathf.Abs((point - closest.transform.position).sqrMagnitude);
		for (int i = 1; i < nodos.Count; i++) {
			float newDistance = Mathf.Abs((point - nodos[i].transform.position).sqrMagnitude);
			RaycastHit test;
			Physics.Raycast(new Ray(point, (nodos[i].transform.position - point).normalized), out test, newDistance);
			if((newDistance < sqrDistance) && (test.transform == nodos[i].transform)){ //comparison
				sqrDistance = newDistance;
				closest = nodos[i];
			}
		}
		
		return closest;
	}
	
	public Vector3[] FindPath(Vector3 origin, Vector3 target){
		List<Vector3> path = new List<Vector3>();
		Node[] pathNodos = FindPath(GetClosestNode(origin), GetClosestNode(target));
		foreach (Node n in pathNodos) {
			path.Add(n.transform.position);
		}
		path.Add(target);
		
		return path.ToArray();
	}
	
	public Node[] FindPath(Node source, Node destination){
		
		//Initialize single source
		float[] d = new float[nodos.Count];
		
		for (int i = 0; i < d.Length; i++) {
			d[i] = Mathf.Infinity;
		}
		d[node2int[source]] = 0.0f;
		Node[] p = new Node[nodos.Count];
		
		//Dijkstra
		PriorityQueue<float, Node> q = new PriorityQueue<float, Node>(nodos.Count);
		
		foreach(Node n in nodos){
			q.Add(new KeyValuePair<float, Node>(d[node2int[n]], n));
		}
		
		while(q.Count > 0){
			Node u = q.DequeueValue();
			for(int i = 0; i < nodos.Count; i++){
				if(i != node2int[u] && adj[node2int[u], i] != Mathf.Infinity){	
					//Relaxation
					if(d[i] > d[node2int[u]] + adj[node2int[u], i]){
						//Debug.Log("Contiene" + q.Contains(new KeyValuePair<float, Node>(d[i], nodos[i])));
						q.Remove(new KeyValuePair<float, Node>(d[i], nodos[i]));
					
						d[i] = d[node2int[u]] + adj[node2int[u], i];
						p[i] = u;
						//Debug.Log(i + " " + d[i]);
						//Update values
						q.Add(new KeyValuePair<float, Node>(d[i], nodos[i]));
					}
				}
			}
			//Debug.Log("Iteration" + node2int[u]);
		}
		
		Stack<Node> path = new Stack<Node>();
		
		Node prev = destination;
		
		while(prev != source){
			path.Push(prev);
			prev = p[node2int[prev]];
		}
		
		path.Push(source);
		return path.ToArray();
	}
}
