using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
[RequireComponent (typeof(AudioSource))]
public class EnemyDisplay : MonoBehaviour 
{
	private Text myText;
	private int enemyCnt = 0;
	
	private LevelManager levelManager;
	private Slider slider;
	private AudioSource audioSource;
	private GameObject winLabel;

	// Use this for initialization
	void Start() 
	{
		myText = GetComponent<Text>();
		UpdateDisplay();
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		if ( ! levelManager ) {
			Debug.Log( name + ": no level manager found!" );
		}
		
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetCurrentSfxVolume();
		
		InitWinLabel();
	}
	
	public void SetEnemyCount( int cnt )
	{
//        Debug.Log( "SetEnemyCount: " + cnt.ToString() );
		enemyCnt = cnt;
//        Debug.Log( "enemyCnt: " + enemyCnt.ToString() );
		UpdateDisplay();
	}
	
	public void AddEnemy()
	{
		++enemyCnt;
		UpdateDisplay();
	}
			
	public void RemoveEnemy()
	{
		--enemyCnt;
		UpdateDisplay();
		
		if ( enemyCnt <= 0 ) {
			FinishLevel();
		}
	}
	
	private void UpdateDisplay()
	{
		if ( myText ) {
			myText.text = enemyCnt.ToString();
//            Debug.Log( "Text: " + myText.text + ", enemy cnt: " + enemyCnt.ToString() );
		}
//        else {
//            Debug.Log( "No Text found!" );
//        }
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
		DestroyAllTaggedObjects();
		winLabel.SetActive( true );
		audioSource.Play();
		Invoke( "LoadNextLevel", audioSource.clip.length );
	}
	
	private void LoadNextLevel()
	{
		levelManager.LoadNextLevel();	
	}
	
	private void DestroyAllTaggedObjects()
	{
		GameObject[] taggedObjs = GameObject.FindGameObjectsWithTag( "destroyOnWin" );
		foreach ( GameObject obj in taggedObjs ) {
			Destroy( obj );
		}
	}
}
