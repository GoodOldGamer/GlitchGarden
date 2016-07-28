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

        SceneManager.sceneLoaded += LevelLoaded;
        LevelLoaded( SceneManager.GetActiveScene(), LoadSceneMode.Single );
	}
   
    void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log( "Music Player loaded level " + level.ToString() );
        
        AudioClip thisLevelMusic = instance.levelMusicChangeArray[ scene.buildIndex ];
        
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
