using UnityEngine;
using System.Collections.Generic;

public class Grid{
	
	private List<Nodo> nodos;
	
	public Grid(List<Nodo> nodos){
		this.nodos = nodos;
		List<Nodo> unSeen = new List<Nodo>(nodos);
		foreach(Nodo n in nodos){
			unSeen.Remove(n);
			n.FindNeighbors(unSeen);
		}
	}
}
