using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour 
{
	public Slider 			masterVolumeSlider;
	public Slider 			musicVolumeSlider;
	public Slider 			sfxVolumeSlider;
	
	private LevelManager 	levelManager;
	private MusicManager	musicManager;
	
	// Use this for initialization
	void Start() 
	{
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();	
		if ( ! levelManager ) {
			Debug.Log( "No level manager found!" );
		}
		
		masterVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		sfxVolumeSlider.value = PlayerPrefsManager.GetSfxVolume();
	}
	
	// Update is called once per frame
	void Update() 
	{
		PlayerPrefsManager.SetMasterVolume( masterVolumeSlider.value );
		PlayerPrefsManager.SetMusicVolume( musicVolumeSlider.value );
		PlayerPrefsManager.SetSfxVolume( sfxVolumeSlider.value );
		
		if ( musicManager ) {
			musicManager.UpdateVolumeSettings();
		}
	}
	
	public void SaveAndExit()
	{
		HideMenu();
		PlayerPrefsManager.Save();
	}
	
	public void ExitToMainMenu()
	{
		HideMenu();
		levelManager.LoadLevel( "01a_Start" );
	}
	
	public void ExitGame()
	{
		HideMenu();
		levelManager.Quit();
	}
	
	private void HideMenu()
	{		
		Time.timeScale = 1; // resume game
		this.gameObject.SetActive( false );
	}
}
