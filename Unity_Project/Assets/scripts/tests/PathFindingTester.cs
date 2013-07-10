using UnityEngine;
using System.Collections;

public class PathFindingTester : MonoBehaviour {
	
	private Vector3 source, destination;
	private bool newSource, newDestination;
	public LayerMask layer;
	public Node[] path;
	public Color pathColor;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			source = GetMousePoint();
			newSource = true;
		}
		
		if(Input.GetMouseButtonDown(1)){
			destination = GetMousePoint();
			newDestination = true;
		}
		
		if(newSource && newDestination){
			DrawPath();
		}
	}

	void DrawPath ()
	{
		//Debug.Log("Drawing Path");
		if(path != null){
			foreach(Node n in path){
				n.gameObject.renderer.material.color = Color.magenta;
			}
		}
		
		Node s = Level.Instance.Grid.GetClosestNode(source), d = Level.Instance.Grid.GetClosestNode(destination);
		path = Level.Instance.Grid.FindPath(s, d);
		
		foreach (Node n in path) {
			//Debug.Log(n.transform.position);
			n.gameObject.renderer.material.color = pathColor;
		}
		
		s.gameObject.renderer.material.color = Color.yellow;
		d.gameObject.renderer.material.color = Color.black;
		
		newSource = false;
		newDestination = false;
	}

	private Vector3 GetMousePoint ()
	{
		RaycastHit hit;
		if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer)){
			return hit.point;
		}else{
			Debug.Log("El punto de la pantalla no es valido.");
			return new Vector3();
		}
	}
}
