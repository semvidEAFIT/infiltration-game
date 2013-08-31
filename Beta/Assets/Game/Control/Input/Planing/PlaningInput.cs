using UnityEngine;
using System.Collections;

public class PlaningInput : MonoBehaviour {
	/*
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
	private CommandEnum[] available;
	
	private RaycastHit leftClicked;
	
	*/
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)){
				Interact(hit);
			}
		}
	}
	
	void Interact(RaycastHit hit){
		switch(hit.collider.gameObject.tag){
		
		case "FireTeam":
			Level.Instance.SetSelectedTeam(hit.transform.GetComponent<FireTeam>());
			break;	
			
		case "":
			break;
		
		default:
			break;
		}
	}
	/*
	void OnGUI(){
		if (menuActive){
			GUI.BeginGroup(new Rect(menuLeft, menuTop, buttonX*numberRows, buttonY*maxRowIcons));
			int count=0;
			for (int j=0; j<numberRows; j++){
				for(int i=0; i<maxRowIcons && count<available.Length; i++){
					if (GUI.Button(new Rect(j*buttonX, i*(yFraction*Screen.height), buttonX, buttonY), commandTextures[(int)available[count]])){
						CallCommand(available[count], leftClicked);
						CloseMenu();
					}
					count++;
				}
			}			
			GUI.EndGroup();	
		}
		
		float r = (0.75f)*(21*Screen.height/46);
		if(GUI.Button(new Rect(0, Screen.height - r, r, r), "Restart")){
			
		}
		if(GUI.Button(new Rect(Screen.width - r, Screen.height - r, r, r), "Execute")){
			PlayerControl.Instance.Phase = PlayerControl.GamePhase.Execution;
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
		numberRows = available.Length/maxRowIcons;
		if(available.Length%maxRowIcons != 0) numberRows++;
		
		menuRigth= buttonX*numberRows + menuLeft;
		menuBottom= buttonY*maxRowIcons + menuTop;
	}
	
	public void CloseMenu(){
		menuActive=false;
	}
	
	void SetAvailableCommands(string tag){
		switch(tag){
			case "Floor":
				available = new CommandEnum[]{CommandEnum.ArmMine, CommandEnum.ThrowFlashGrenade, CommandEnum.ThrowFragGrenade, CommandEnum.Wait};
				break;
			case "OpenDoor":
				available = new CommandEnum[]{CommandEnum.OpenDoor, CommandEnum.BlowDoor};
				break;
			case "ClosedDoor":
				available = new CommandEnum[]{CommandEnum.ForceDoor, CommandEnum.BlowDoor};
				break;
			case "Hostage":
				available = new CommandEnum[]{CommandEnum.CoverHostage};
				break;
			case "OpenWindow":
				available = new CommandEnum[]{CommandEnum.OpenWindow, CommandEnum.BlowWindow};
				break;
			case "ClosedWindow":
				available = new CommandEnum[]{CommandEnum.ForceWindow, CommandEnum.BlowWindow};
				break;
			default:
				break;
		}	
	}
	*/
}
