using UnityEngine;
using System.Collections;

public class PlanningControl : MonoBehaviour {

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
	private FireTeam team;
	
	void Start() {
		buttonX = xFraction*Screen.width;
		buttonY = yFraction*Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(1)){
			rigthClickPos = new Vector2(Input.mousePosition.x, Screen.height-Input.mousePosition.y);
			
			Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if(Physics.Raycast(ray, out hit) && ((hit.transform.tag == "Floor") || (hit.transform.tag == "ClosedDoor") || (hit.transform.tag == "OpenDoor") || (hit.transform.tag == "Hostage") || (hit.transform.tag == "ClosedWindow") || (hit.transform.tag == "OpenWindow"))){
				leftClicked = hit;
				SetAvailableCommands(hit.transform.tag);
				DrawMenu(rigthClickPos);
			}
		}
		
		if(Input.GetMouseButtonDown(0)){
			if(menuActive){
				leftClickPos = new Vector2(Input.mousePosition.x, Screen.height-Input.mousePosition.y);
				if(leftClickPos.x < menuLeft || leftClickPos.x > menuRigth || leftClickPos.y<menuTop || leftClickPos.y > menuBottom){
					menuActive=false;
				}
			}else{
				Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				
				if(Physics.Raycast(ray, out hit)){
					//Debug.Log(hit.transform.tag);
					if(hit.transform.tag == "Floor"){
						CallCommand(CommandEnum.Move, hit);	
					}
					if(hit.transform.tag == "Fireteam"){
						team = hit.transform.parent.GetComponent<FireTeam>();
						//Debug.Log(team);
					}
				}
			}
		}
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
	
	public void CallCommand(CommandEnum requested, RaycastHit clicked){
		if(clicked.Equals(null) || team == null) return;
		
		switch(requested){
			case CommandEnum.Move:
				team.AddCommand(new MoveCommand(team, clicked.transform.position));
				break;
			default:
				break;
		}
	}
}
