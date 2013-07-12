using UnityEngine;
using System.Collections.Generic;

//Specific for PC Input
public class AlphaButton : MonoBehaviour {
	
	#region Variables
	
	private bool pressed, down, released, enter, exit;

	private bool Down {
		get {
			return this.down;
		}
		set {
			down = value;
		}
	}

	private bool Enter {
		get {
			return this.enter;
		}
		set {
			enter = value;
		}
	}

	private bool Exit {
		get {
			return this.exit;
		}
		set {
			exit = value;
		}
	}

	private bool Pressed {
		get {
			return this.pressed;
		}
		set {
			pressed = value;
		}
	}

	public bool Released {
		get {
			return this.released;
		}
		set {
			released = value;
		}
	}	
	
	public float alpha = 5.0f;
	
	public bool inverse = false;
	
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
		return pixelColor.a > 0.5f && !inverse;
	}
	
	void Update(){
		if(Down || Pressed || Released || Enter || Exit){
			NotifyAllListeners();
		}
	}
	
	#region Event Triggers
	void OnMouseDown(){
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)){
				Down = true;
				Pressed =true;
			}
		}
	}
	
	void OnMouseUp(){
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)){
				Released = true;
				Pressed = false;
			}
		}
	}
	
	void OnMouseEnter(){
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)){
				Enter = true;
			}
		}
	}
	
	void OnMouseExit(){
		Exit = true;
		Pressed = false;
		Released=false;
		Down = false;
		Enter = false;
	}
	
	void OnMouseOver(){
		Vector2 pos = Vector2.zero;
		if(GetHit(ref pos)){
			if(CheckAlpha(pos)){
				if(!enter){
					Enter = true;
					Pressed = true;
				}
			}else{
				if(!exit){
					Exit = true;
					Pressed = false;
					Released = false;
					Down = false;
					Enter = false;
				}
			}
		}
		Enter = false;
		Exit = false;
		Down = false;
		Released = false;
	}
	#endregion
	
	#region Observable
	public void AddButtonListener(IButtonListener listener){
		listeners.Add(listener);
	}
	
	private void NotifyAllListeners(){
		foreach (IButtonListener listener in listeners) {
			listener.UpdateButton(this);
		}
		Enter = false;
		Exit = false;
		Down = false;
		Released = false;
	}
	#endregion
}
