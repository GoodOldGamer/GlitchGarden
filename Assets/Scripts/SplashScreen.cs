using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class SplashScreen : MonoBehaviour 
{
	public LevelManager levelManager;
	public float 		startTime = 2f;

    private AudioSource audioSource;
	private Image       fadePanel;
	private Color       currentColor;

	void Start() 
	{
		fadePanel = this.gameObject.GetComponent<Image>();
		currentColor = fadePanel.color;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetCurrentMusicVolume();

        startTime += audioSource.clip.length;

        audioSource.Play();
	}
	
	void Update() 
	{
        if ( ! audioSource.isPlaying ) {
    		if ( Time.timeSinceLevelLoad < startTime ) {
                float alphaChange = Time.deltaTime / (startTime - audioSource.clip.length);
    			currentColor.a -= alphaChange;
    			fadePanel.color = currentColor;
    		}
    		else {
    			this.gameObject.SetActive( false );
                LoadStartScene();
    		}
        }
	}
	
	void LoadStartScene()
	{
		levelManager.LoadNextLevel();
	}
}
