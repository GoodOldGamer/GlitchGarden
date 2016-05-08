using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class LoseCollider : MonoBehaviour 
{
	public string loseLevelName;
	
	private EnemySpawner enemySpawner;
	private LevelManager levelManager;
	private AudioSource audioSource;
	private GameObject loseLabel;
	
	private bool isEndOfGame = false;
	
	// Use this for initialization
	void Start() 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		if ( ! levelManager ) {
			Debug.Log( name + ": no level manager found!" );
		}
		
		enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
		if ( ! enemySpawner ) {
			Debug.Log( name + ": no enemy spawner found!" );
		}
		
		audioSource = this.GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetCurrentSfxVolume();
		
		InitLoseLabel();
	}
	
	void OnTriggerEnter2D( Collider2D trigger )
	{
		if ( ! isEndOfGame ) {
			LoseGame();
			isEndOfGame = true;
		}
	}
	
	private void InitLoseLabel()
	{	
		loseLabel = GameObject.Find( "LoseLabel" );
		if ( ! loseLabel ) {
			Debug.Log( name + ": no lose label!" );
			return;
		}
		loseLabel.SetActive( false );
	}
	
	private void LoseGame()
	{
		enemySpawner.SetActive( false );
		DestroyAllTaggedObjects();
		loseLabel.SetActive( true );
		audioSource.Play();
		Invoke( "LoadLoseScreen", audioSource.clip.length );	
	}
	
	private void LoadLoseScreen()
	{
		levelManager.LoadLevel( loseLevelName );	
	}
	
	private void DestroyAllTaggedObjects()
	{
		GameObject[] taggedObjs = GameObject.FindGameObjectsWithTag( "destroyOnWin" );
		foreach ( GameObject obj in taggedObjs ) {
			Destroy( obj );
		}
	}
}
