using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{
	public LevelManager levelManager;
	public float 		startTime = 2f;
	
	private Image fadePanel;
	private Color currentColor;

	void Start() 
	{
		fadePanel = this.gameObject.GetComponent<Image>();
		currentColor = fadePanel.color;
		
		Invoke( "LoadStartScene", startTime );
	}
	
	void Update() 
	{
		if ( Time.timeSinceLevelLoad < startTime ) {
			float alphaChange = Time.deltaTime / startTime;
			currentColor.a -= alphaChange;
			fadePanel.color = currentColor;
		}
		else {
			this.gameObject.SetActive( false );
		}
	}
	
	void LoadStartScene()
	{
		levelManager.LoadNextLevel();
	}
}
