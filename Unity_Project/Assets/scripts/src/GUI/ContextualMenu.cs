using UnityEngine;
using System.Collections;

public class ContextualMenu : MonoBehaviour {

	private bool menuActive = false;
	private Vector2 rigthClickPos;
	private Vector2 leftClickPos;
	
	private float menuLeft;
	private float menuRigth;
	private float menuTop;
	private float menuBottom;
	
	private float menuX;
	private float menuY; 
	
	private int zone;
	
	public Texture[] commandTextures;
	
	public float xFraction= 0.05f;
	public float yFraction= 0.05f;
	
	void Start() {
		menuX = xFraction*Screen.width;
		menuY = yFraction*Screen.height*commandTextures.Length;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			rigthClickPos = new Vector2(Input.mousePosition.x, Screen.height-Input.mousePosition.y);
			DrawMenu(rigthClickPos);
		}
		if(menuActive && Input.GetMouseButtonDown(0)){
			leftClickPos = new Vector2(Input.mousePosition.x, Screen.height-Input.mousePosition.y);
			if(leftClickPos.x < menuLeft || leftClickPos.x > menuRigth || leftClickPos.y<menuTop || leftClickPos.y > menuBottom){
				menuActive=false;
			}
		}
	}
	
	void OnGUI(){
		if (menuActive){
			GUI.BeginGroup(new Rect(menuLeft, menuTop, menuX, menuY));
				for (int i=0; i<commandTextures.Length; i++){
					if (GUI.Button(new Rect(0, i*(yFraction*Screen.height), menuX, (yFraction*Screen.height)), commandTextures[i])){
						CallCommand(i);
						CloseMenu();
					}
				}
			GUI.EndGroup();
		}
	}
	
	public void DrawMenu(Vector2 screen){ //recibe coordenadas del click 
		menuActive=true;
		if (screen.y < Screen.height/2){
			if(screen.x < Screen.width/2){
				//cuadrante superior izquierdo
				menuLeft=rigthClickPos.x;
				menuTop= rigthClickPos.y;
				
			} else {
				//cuadrante superior derecho
				menuLeft= rigthClickPos.x - menuX;
				menuTop= rigthClickPos.y;
			}
		} else {
			if(screen.x < Screen.width/2){
				//cuadrante inferior izquierdo
				menuLeft= rigthClickPos.x;
				menuTop= rigthClickPos.y - menuY;
			} else {
				//cuadrante inferior derecho
				menuLeft= rigthClickPos.x - menuX;
				menuTop= rigthClickPos.y - menuY;
			}
		}
		menuRigth= menuX + menuLeft;
		menuBottom= menuY + menuTop;
	}
	
	public void CloseMenu(){
		menuActive=false;
	}
	
	public void CallCommand(int numberCommand){
		Debug.Log("Command " + (numberCommand+1));
	}
}
