using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour, IButtonListener {
	
	void Start(){
		GetComponent<AlphaButton>().AddButtonListener(this);
	}

	#region IButtonListener implementation
	void IButtonListener.UpdateButton (AlphaButton button)
	{
		if(button.Pressed){
			Debug.Log("Pressed");
		}
		if(button.Enter){
			Debug.Log("Enter");
		}
		if(button.Exit){
			Debug.Log("Exit");
		}
		if(button.Released){
			Debug.Log("Released");
		}
		if(button.Down){
			Debug.Log("Down");
		}
	}
	#endregion
}
