using UnityEngine;
using System.Collections;

public class ContextualMenu : MonoBehaviour {

	private bool menuActive = false;
	private Vector2 clickpos;
	
	private int zone;
	
	public Texture[] commandTextures;
	
	public float xFraction= 0.05f;
	public float yFraction= 0.05f;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			clickpos = new Vector2(Input.mousePosition.x, Screen.height-Input.mousePosition.y);
			DrawMenu(clickpos);
		}
	}
	
	void OnGUI(){
		if (menuActive){
			switch(zone){
			case 1:
				GUI.BeginGroup(new Rect(clickpos.x, clickpos.y, (xFraction*Screen.width), yFraction*Screen.height*commandTextures.Length));
					for (int i=0; i<commandTextures.Length; i++){
						if (GUI.Button(new Rect(0, i*(yFraction*Screen.height), (xFraction*Screen.width), (yFraction*Screen.height)), commandTextures[i])){
							CallCommand(i);
							CloseMenu();
						}
					}
				GUI.EndGroup();
				break;
			case 2:
				GUI.BeginGroup(new Rect(clickpos.x - (xFraction*Screen.width), clickpos.y, (xFraction*Screen.width), yFraction*Screen.height*commandTextures.Length));
					for (int i=0; i<commandTextures.Length; i++){
						if (GUI.Button(new Rect(0, i*(yFraction*Screen.height), (xFraction*Screen.width), (yFraction*Screen.height)), commandTextures[i])){
							CallCommand(i);
							CloseMenu();
						}
					}
				GUI.EndGroup();
				break;
			case 3:
				GUI.BeginGroup(new Rect(clickpos.x, clickpos.y - yFraction*Screen.height*commandTextures.Length, (xFraction*Screen.width), yFraction*Screen.height*commandTextures.Length));
					for (int i=0; i<commandTextures.Length; i++){
						if (GUI.Button(new Rect(0, i*(yFraction*Screen.height), (xFraction*Screen.width), (yFraction*Screen.height)), commandTextures[i])){
							CallCommand(i);
							CloseMenu();
						}
					}
				GUI.EndGroup();
				break;
			case 4:
				GUI.BeginGroup(new Rect(clickpos.x - (xFraction*Screen.width), clickpos.y - yFraction*Screen.height*commandTextures.Length, (xFraction*Screen.width), yFraction*Screen.height*commandTextures.Length));
					for (int i=0; i<commandTextures.Length; i++){
						if (GUI.Button(new Rect(0, i*(yFraction*Screen.height), (xFraction*Screen.width), (yFraction*Screen.height)), commandTextures[i])){
							CallCommand(i);
							CloseMenu();
						}
					}
				GUI.EndGroup();
				break;
			default:
				GUI.BeginGroup(new Rect(clickpos.x, clickpos.y, (xFraction*Screen.width), yFraction*Screen.height*commandTextures.Length));
					for (int i=0; i<commandTextures.Length; i++){
						if (GUI.Button(new Rect(0, i*(yFraction*Screen.height), (xFraction*Screen.width), (yFraction*Screen.height)), commandTextures[i])){
							CallCommand(i);
							CloseMenu();
						}
					}
				GUI.EndGroup();
				break;
			}
		}
	}
	
	public void DrawMenu(Vector2 screen){ //recibe coordenadas del click 
		menuActive=true;
		if (screen.y < Screen.height/2){
			if(screen.x < Screen.width/2){
				//cuadrante superior izquierdo
				zone=1;
			} else {
				//cuadrante superior derecho
				zone=2;
			}
		} else {
			if(screen.x < Screen.width/2){
				//cuadrante inferior izquierdo
				zone=3;
			} else {
				//cuadrante inferior derecho
				zone=4;
			}
		}
	}
	
	public void CloseMenu(){
		menuActive=false;
	}
	
	public void CallCommand(int numberCommand){
		Debug.Log("Command " + (numberCommand+1));
	}
}
