using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {
	
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
	}
	
    

	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	
	public void addObjective(){
		objectiveCount++;
	}
	public void objectiveDone(){
		objectiveCount--;
	}
}
