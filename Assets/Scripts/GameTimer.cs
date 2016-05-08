using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Slider))]
[RequireComponent (typeof(AudioSource))]
public class GameTimer : MonoBehaviour 
{
	public float levelSeconds = 120;

	private LevelManager levelManager;
	private Slider slider;
	private AudioSource audioSource;
	private GameObject winLabel;
	
	private bool isEndOfLevel = false;
	
	// Use this for initialization
	void Start() 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		if ( ! levelManager ) {
			Debug.Log( name + ": no level manager found!" );
		}
		
		slider = this.GetComponent<Slider>();
		audioSource = this.GetComponent<AudioSource>();
		
		InitWinLabel();
	}
	
	// Update is called once per frame
	void Update() 
	{
		slider.value = Time.timeSinceLevelLoad / levelSeconds;
		
		bool timeIsUp =Time.timeSinceLevelLoad >= levelSeconds;
		if ( timeIsUp && !isEndOfLevel ) {
			FinishLevel();
			isEndOfLevel = true;
		}
	}
	
	private void InitWinLabel()
	{
		winLabel = GameObject.Find( "WinLabel" );
		if ( ! winLabel ) {
			Debug.Log( name + ": no win label!" );
			return;
		}
		winLabel.SetActive( false );
	}
	
	private void FinishLevel()
	{
		winLabel.SetActive( true );
		audioSource.Play();
		Invoke( "LoadNextLevel", audioSource.clip.length );
	}
	
	private void LoadNextLevel()
	{
		levelManager.LoadNextLevel();	
	}
}
