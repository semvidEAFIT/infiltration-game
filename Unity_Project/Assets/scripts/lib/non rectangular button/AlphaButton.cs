using UnityEngine;
using System.Collections.Generic;

//Specific for PC Input
public class AlphaButton : MonoBehaviour {
	
	#region Variables
	
	private bool pressed, down, released, enter, exit, over;

	public bool Over {
		get {
			return this.over;
		}
	}
	public bool Down {
		get {
			return this.down;
		}
	}

	public bool Enter {
		get {
			return this.enter;
		}
	}

	public bool Exit {
		get {
			return this.exit;
		}
	}

	public bool Pressed {
		get {
			return this.pressed;
		}
	}

	public bool Released {
		get {
			return this.released;
		}
	}
	
	public float alpha = 0.5f;
	
	//public bool inverse = false; TODO: Buttons with inside alpha
	
	public bool notifyPressed = false, notifyOver = false;	
	
	private Texture2D texture;
	
	private List<IButtonListener> listeners;
	
	#endregion
	
	void Awake(){
		listeners = new List<IButtonListener>();
		texture = renderer.sharedMaterial.GetTexture("_MainTex") as Texture2D;
	}
	
	private bool GetHit(ref Vector2 hitPosition){
		RaycastHit hit;
		if(collider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Abs((Camera.main.transform.position - transform.position).magnitude))){
			hitPosition = hit.textureCoord;
			hitPosition.x *= texture.width;
			hitPosition.y *= texture.height;
			return true;
		}
		return false;
	}
	
	private bool CheckAlpha(Vector2 pos){
		Color pixelColor = texture.GetPixel((int)pos.x, (int)pos.y);
		//Debug.Log(pixelColor.a);
		return pixelColor.a > alpha;
	}
	
	void Update(){
		if(Down || (Pressed && notifyPressed) || Released || Enter || Exit || (Over && notifyOver)){
			NotifyAllListeners();
		}
	}
	
	#region Event Triggers
	void OnMouseDown(){
		//Debug.Log("*Down");
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)&&!pressed){
				down = true;
				pressed =true;
			}
		}
	}
	
	void OnMouseUp(){
		//Debug.Log("*Up");
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos) && pressed){
				released = true;
				pressed = false;
			}
		}
	}
	
	void OnMouseEnter(){
		//Debug.Log("*Enter");
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)){
				enter = true;
				over = true;
			}
		}
	}
	
	void OnMouseExit(){
		//Debug.Log("*Exit");
		if(Over){
			exit = true;
		}
		pressed = false;
		released=false;
		down = false;
		enter = false;
	}
	
	void OnMouseOver(){
		//Debug.Log("*Over");
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)){
				if(!Over){
					enter = true;
					over = true;
				}
			}else{
				if(Over){
					exit = true;
					over = false;
					pressed = false;
					released = false;
					down = false;
					enter = false;
				}
			}
		}
	}
	#endregion
	
	#region Observable
	public void AddButtonListener(IButtonListener listener){
		listeners.Add(listener);
	}
	
	private void NotifyAllListeners(){
		#region Debug
		/*if(Pressed && notifyPressed){
			Debug.Log("Pressed");
		}
		if(Enter){
			Debug.Log("Enter");
		}
		if(Exit){
			Debug.Log("Exit");
		}
		if(Released){
			Debug.Log("Released");
		}
		if(Down){
			Debug.Log("Down");
		}
		if(Over && notifyOver){
			Debug.Log("Over");
		}*/
		#endregion
		foreach (IButtonListener listener in listeners) {
			listener.UpdateButton(this);
		}
		enter = false;
		exit = false;
		down = false;
		released = false;
	}
	#endregion
}
