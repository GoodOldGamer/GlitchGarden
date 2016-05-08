using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{
	public AudioClip[] levelMusicChangeArray;
	
	private AudioSource music;
	
	void Awake()
	{
		DontDestroyOnLoad( gameObject );
	}
	
	void Start()
	{
		music = GetComponent<AudioSource>();
		music.loop = true;
		
		UpdateVolumeSettings();		
		OnLevelWasLoaded( 0 );
	}
	
	void OnLevelWasLoaded( int level )
	{
		Debug.Log( "Music Player loaded level " + level.ToString() );
		
		AudioClip thisLevelMusic = levelMusicChangeArray[ level ];
		
		if ( thisLevelMusic && thisLevelMusic != music.clip ) {
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
