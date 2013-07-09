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
	
	private float buttonX;
	private float buttonY; 
	
	private int numberRows;
	
	private int zone;
	
	public int maxRowIcons = 5;
	
	public float xFraction= 0.05f;
	public float yFraction= 0.05f;
	
	public Texture[] commandTextures;
	
	void Start() {
		buttonX = xFraction*Screen.width;
		buttonY = yFraction*Screen.height;
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
			GUI.BeginGroup(new Rect(menuLeft, menuTop, buttonX*numberRows, buttonY*maxRowIcons));
			int count=0;
			for (int j=0; j<numberRows; j++){
				for(int i=0; i<maxRowIcons && count<commandTextures.Length; i++){
					if (GUI.Button(new Rect(j*buttonX, i*(yFraction*Screen.height), buttonX, buttonY), commandTextures[count])){
						CallCommand(count);
						CloseMenu();
					}
					count++;
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
				menuLeft= rigthClickPos.x - buttonX*numberRows;
				menuTop= rigthClickPos.y;
			}
		} else {
			if(screen.x < Screen.width/2){
				//cuadrante inferior izquierdo
				menuLeft= rigthClickPos.x;
				menuTop= rigthClickPos.y - buttonY*maxRowIcons;
			} else {
				//cuadrante inferior derecho
				menuLeft= rigthClickPos.x - buttonX*numberRows;
				menuTop= rigthClickPos.y - buttonY*maxRowIcons;
			}
		}
		numberRows = commandTextures.Length/maxRowIcons;
		if(commandTextures.Length%maxRowIcons != 0) numberRows++;
		
		menuRigth= buttonX*numberRows + menuLeft;
		menuBottom= buttonY*maxRowIcons + menuTop;
	}
	
	public void CloseMenu(){
		menuActive=false;
	}
	
	public void CallCommand(int numberCommand){
		Debug.Log("Command " + (numberCommand+1));
	}
}
