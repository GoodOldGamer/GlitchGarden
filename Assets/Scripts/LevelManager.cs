using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	public void LoadLevel( string name )
	{
		//Debug.Log( "LoadLevel called for " + name );
		
        SceneManager.LoadScene( name );
	}
	
	public void LoadNextLevel()
	{
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
	}
	
	public void Quit()
	{
		//Debug.Log( "Quit called" );
		
		Application.Quit();
	}
}
