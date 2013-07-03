using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	
	
	private List<Nodo> nodos;
	private static Level instance;
	
	public static Level Instance {
		get {
			return instance;
		}
	}
	
	private int objectiveCount;
	
	void Awake(){
		if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Solo puede haber un level activo a la vez");
            Destroy(this.gameObject);
        }
		
		objectiveCount=0;
		
		nodos = new List<Nodo>();
		GameObject[] gNodo = GameObject.FindGameObjectsWithTag("Nodo");
		foreach(GameObject g in gNodo){
			nodos.Add(g.GetComponent<Nodo>());	
		}
		List<Nodo> unSeen = new List<Nodo>(nodos);
		foreach(Nodo n in nodos){
			unSeen.Remove(n);
			n.FindNeighbors(unSeen);
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	
	public void AddObjective(){
		objectiveCount++;
	}
	public void ObjectiveDone(){
		objectiveCount--;
	}
}
