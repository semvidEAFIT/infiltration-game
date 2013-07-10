using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	
	private List<Node> nodos;
	private static Level instance;

	public static Level Instance {
		get {
			return instance;
		}
	}
	
	private int objectiveCount;
	private Grid grid;

	public Grid Grid {
		get {
			return this.grid;
		}
	}	
	
	void Awake(){
		if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Solo puede haber un level activo a la vez");
            Destroy(this.gameObject);
        }
		
		objectiveCount = 0;
	}
	
	void Start(){
		MakeGrid(); //Luego de que todos los nodos se incialicen, no mover.
	}

	void MakeGrid ()
	{
		nodos = new List<Node>();
		GameObject[] gNodo = GameObject.FindGameObjectsWithTag("Node");
		foreach(GameObject g in gNodo){
			nodos.Add(g.GetComponent<Node>());	
		}
		this.grid = new Grid(nodos);
	}
	
	public void AddObjective(){
		objectiveCount++;
	}
	public void ObjectiveDone(){
		objectiveCount--;
	}
}
