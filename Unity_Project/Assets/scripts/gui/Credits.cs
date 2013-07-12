using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class Credits : MonoBehaviour 
{
    public GUISkin creditSkin;
    public float creditSpeed;
    private TextReader tr;
    public string path;
    private List<string> credits;
	private List<string> shownCredits;
    private List<Rect> positionRect;
	
    public List<TextMesh> scripts;
    public int linesPerScreen = 12;
	public float lineYSpace = 0.01f;
    public float typeSpeed = 24f;
	private float yDelta;
    private float elapsedTime = 0.0f;
    private int index = 0, currentChar = 0, currentStringLength = 0;
 
    // Use this for initialization
    void Start () 
    {
		credits = new List<string>();
		shownCredits = new List<string>();
		positionRect = new List<Rect>();
		yDelta = 0f;
 
        // Create reader & open file
		try{
        	tr = new StreamReader(path);
			string temp;
        	int count = 0;
	        while((temp = tr.ReadLine()) != null)
	        {
	            credits.Add(temp);
				shownCredits.Add ("");
//	            positionRect.Add(new Rect(Screen.width/4 - Screen.width/8, (float)(Screen.height * 0.07 * count + Screen.height), (float)(Screen.width/2 + Screen.width/4), (float)(Screen.height * 0.5)));
	            positionRect.Add(new Rect(Screen.width/4 - Screen.width/8, 0f, (float)(Screen.width/2 + Screen.width/4), (float)(Screen.height * 0.5)));
				count++;
	        }
	 		currentStringLength = credits[0].Length;
	        // Close the stream
	        tr.Close();
		} 
		catch(FileLoadException e) {
			Debug.LogException(e);
			credits.Add("Error while loading credits file.");
		}        
    }
	
	void Update(){		
		//NIGGAZ: Aquí hay que poner la escena que va después de que se muestren los créditos.
//		if(positionRect[positionRect.Count - 1].y < -150f){
//			Application.LoadLevel("MainMenu");
//		}
//		if(Input.GetKey(KeyCode.Escape)){
//			Application.LoadLevel("MainMenu");
//		}
	}
	
    void OnGUI() 
    {
        GUI.skin = creditSkin;
		elapsedTime += Time.deltaTime;
		if(index > linesPerScreen){
			yDelta += lineYSpace;
			linesPerScreen++;
//			for(int i = 0; i < shownCredits.Count; i++){
//	            Rect tempRect = positionRect[i];
//	            tempRect.y -= lineYSpace;
//	            positionRect[i] = tempRect;
//			}
		}
		
        if (elapsedTime > typeSpeed && index < shownCredits.Count - 1){
			if(currentChar < currentStringLength){
				shownCredits[index] += credits[index].Substring(currentChar, 1);
				currentChar++;
				elapsedTime = 0f;
			}
			else{
				index++;
				currentChar = 0;
				currentStringLength = credits[index].Length;
			}
		}
			
        for (int i = 0; i < credits.Count; i++)
        {
			Rect tempRect = positionRect[i];
            tempRect.y = i * lineYSpace - yDelta;
            positionRect[i] = tempRect;
            GUI.Label(positionRect[i], shownCredits[i], "label");
        }
    }
}