using UnityEngine;
using System.Collections.Generic;
using PriorityQueueDemo;

public class Grid{
	
	private List<Nodo> nodos;
	private Dictionary<Nodo, int> node2int;
	private float[,] adj;
	
	public Grid(List<Nodo> nodos){
		node2int = new Dictionary<Nodo, int>(nodos.Count);
		this.nodos = nodos;
		List<Nodo> unSeen = new List<Nodo>(nodos);
		for(int i = 0; i < nodos.Count; i++){
			Nodo n = nodos[i];
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
		
		foreach(Nodo n in nodos){
			adj[node2int[n], node2int[n]] = 0;
			foreach (Nodo m in n.Neighbors) {
				float sqrDistance = Mathf.Abs((m.transform.position - n.transform.position).sqrMagnitude);
				adj[node2int[n], node2int[m]] = sqrDistance;
				adj[node2int[m], node2int[n]] = sqrDistance;
			}	
		}
	}
	
	public Nodo GetClosestNode(Vector3 point){
		Nodo closest = nodos[0];
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
	
	public Nodo[] FindPath(Nodo source, Nodo destination){
		
		//Initialize single source
		float[] d = new float[nodos.Count];
		
		for (int i = 0; i < d.Length; i++) {
			d[i] = Mathf.Infinity;
		}
		
		d[node2int[source]] = 0.0f;
		
		Nodo[] p = new Nodo[nodos.Count];
		
		//Dijkstra
		PriorityQueue<float, Nodo> q = new PriorityQueue<float, Nodo>(nodos.Count);
		
		foreach(Nodo n in nodos){
			q.Add(new KeyValuePair<float, Nodo>(d[node2int[n]], n));
		}
		
		while(q.Count > 0){
			Nodo u = q.DequeueValue();
			for(int i = 0; i < nodos.Count; i++){
				if(i != node2int[u] && adj[node2int[u], i] != Mathf.Infinity){	
					//Relaxation
					if(d[i] > d[node2int[u]] + adj[node2int[u], i]){
						//Debug.Log("Contiene" + q.Contains(new KeyValuePair<float, Nodo>(d[i], nodos[i])));
						q.Remove(new KeyValuePair<float, Nodo>(d[i], nodos[i]));
					
						d[i] = d[node2int[u]] + adj[node2int[u], i];
						p[i] = u;
						
						//Update values
						
						q.Add(new KeyValuePair<float, Nodo>(d[i], nodos[i]));
					}
				}
			}
		}
		
		Stack<Nodo> path = new Stack<Nodo>();
		
		Nodo prev = destination;
		
		while(prev != source){
			path.Push(prev);
			prev = p[node2int[prev]];
		}
		
		path.Push(source);
		return path.ToArray();
	}
}
