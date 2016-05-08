using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour 
{
	public LevelManager 	levelManager;
	public Slider 			masterVolumeSlider;
	public Slider 			musicVolumeSlider;
	public Slider			sfxVolumeSlider;
	public Slider 			difficultySlider;
	
	private MusicManager	musicManager;
	
	// Use this for initialization
	void Start() 
	{
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		
		masterVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
		sfxVolumeSlider.value = PlayerPrefsManager.GetSfxVolume();
		difficultySlider.value = PlayerPrefsManager.GetDifficultyFloat();
	}
	
	// Update is called once per frame
	void Update() 
	{
		PlayerPrefsManager.SetMasterVolume( masterVolumeSlider.value );
		PlayerPrefsManager.SetMusicVolume( musicVolumeSlider.value );
		PlayerPrefsManager.SetSfxVolume( sfxVolumeSlider.value );
		PlayerPrefsManager.SetDifficulty( difficultySlider.value );
		
		if ( musicManager ) {
			musicManager.UpdateVolumeSettings();
		}
	}
	
	public void SaveAndExit()
	{
		PlayerPrefsManager.Save();
		levelManager.LoadLevel( "01a_Start" );
	}

    public void SetDefaults()
    {
        PlayerPrefsManager.SetDefaults();

        masterVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        musicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
        sfxVolumeSlider.value = PlayerPrefsManager.GetSfxVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficultyFloat();
    }
}
