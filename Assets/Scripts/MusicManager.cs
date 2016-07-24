using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour 
{
	public AudioClip[] levelMusicChangeArray;
	
	private AudioSource music;
    private static MusicManager instance;
	
	void Awake()
	{
        if ( instance ) {
            Destroy( gameObject );
        }
        else {
            instance = this;
        }

        DontDestroyOnLoad( gameObject );
	}
	
	void Start()
	{
		music = GetComponent<AudioSource>();
		music.loop = true;
		
		UpdateVolumeSettings();		
        OnLevelWasLoaded( SceneManager.GetActiveScene().buildIndex );
	}

	void OnLevelWasLoaded( int level )
	{
		//Debug.Log( "Music Player loaded level " + level.ToString() );
		
        AudioClip thisLevelMusic = instance.levelMusicChangeArray[ level ];
		
        if ( thisLevelMusic && music && thisLevelMusic != music.clip ) {
			music.Stop();
			music.clip = thisLevelMusic;	
			music.Play();
		}
	}
	
	public void UpdateVolumeSettings()
	{
		music.volume = PlayerPrefsManager.GetCurrentMusicVolume();
	}
}
