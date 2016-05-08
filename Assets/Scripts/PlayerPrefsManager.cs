using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour 
{
	public enum Difficulty
	{
		EASY = 1,
		NORMAL = 2,
		HARD = 3,
	};

	private const string MASTER_VOLUME_KEY 	= "master_volume";
	private const string MUSIC_VOLUME_KEY 	= "music_volume";
	private const string SFX_VOLUME_KEY 	= "sfx_volume";
	private const string DIFFICULTY_KEY 	= "difficulty";

    private const float defaultVolume = 1;
    private const Difficulty defaultDifficulty = Difficulty.NORMAL;
	
	private static bool settingsLoaded = false;
    private static float masterVolume = defaultVolume;
    private static float musicVolume = defaultVolume;
    private static float sfxVolume = defaultVolume;
    private static Difficulty difficulty = defaultDifficulty;

    public static void SetDefaults()
    {
        masterVolume = defaultVolume;
        musicVolume = defaultVolume;
        sfxVolume = defaultVolume;
        difficulty = defaultDifficulty;
    }
	
	public static float GetCurrentMusicVolume()
	{
		float masterVol = GetMasterVolume();
		float musicVol = GetMusicVolume();
		
		return ( musicVol * masterVol );
	}
	
	public static float GetCurrentSfxVolume()
	{
		float masterVol = GetMasterVolume();
		float sfxVol = GetSfxVolume();
		
		return ( sfxVol * masterVol );
	}

	public static void SetMasterVolume( float volume )
	{
		LoadFromRegistry();
		
		if ( volume >= 0f && volume <= 1f ) {
			masterVolume = volume;
		}
		else {
			Debug.LogError( "Volume " + volume.ToString() + " out of range!" );
		}	
	}
	
	public static void SetMusicVolume( float volume )
	{
		LoadFromRegistry();
		
		if ( volume >= 0f && volume <= 1f ) {
			musicVolume = volume;
		}
		else {
			Debug.LogError( "Volume " + volume.ToString() + " out of range!" );
		}	
	}
	
	public static void SetSfxVolume( float volume )
	{
		LoadFromRegistry();
		
		if ( volume >= 0f && volume <= 1f ) {
			sfxVolume = volume;
		}
		else {
			Debug.LogError( "Volume " + volume.ToString() + " out of range!" );
		}	
	}
	
	public static float GetMasterVolume()
	{
		LoadFromRegistry();
		return masterVolume;
	}
	
	public static float GetMusicVolume()
	{
		LoadFromRegistry();
		return musicVolume;
	}
	
	public static float GetSfxVolume()
	{
		LoadFromRegistry();
		return sfxVolume;
	}
	
	public static void SetDifficulty( float diff )
	{
		LoadFromRegistry();
		
		Difficulty d = (Difficulty)diff;
		if ( d >= Difficulty.EASY && d <= Difficulty.HARD ) {
			difficulty = d;
		}
		else {
			Debug.LogError( "Difficulty " + diff.ToString() + " out of range!" );
		}	
	}
	
	public static Difficulty GetDifficulty()
	{
		LoadFromRegistry();
		return difficulty;
	}
	
	public static float GetDifficultyFloat()
	{
		LoadFromRegistry();
		return (float)difficulty;
	}
	
	public static void Save()
	{
		PlayerPrefs.SetFloat( MASTER_VOLUME_KEY, masterVolume );
		PlayerPrefs.SetFloat( MUSIC_VOLUME_KEY, musicVolume );
		PlayerPrefs.SetFloat( SFX_VOLUME_KEY, sfxVolume );
		PlayerPrefs.SetInt( DIFFICULTY_KEY, (int)difficulty );
	}
	
	private static void LoadFromRegistry()
	{
		if ( settingsLoaded ) return;
		
		masterVolume = PlayerPrefs.GetFloat( MASTER_VOLUME_KEY, 1 );
		musicVolume = PlayerPrefs.GetFloat( MUSIC_VOLUME_KEY, 1 );
		sfxVolume = PlayerPrefs.GetFloat( SFX_VOLUME_KEY, 1 );
		difficulty = (Difficulty)PlayerPrefs.GetInt( DIFFICULTY_KEY, (int)Difficulty.NORMAL );
		settingsLoaded = true;
	}
}
