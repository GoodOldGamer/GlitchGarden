using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody2D) )]
[RequireComponent( typeof(Health) )]
public class Defender : MonoBehaviour 
{
	public int starCost = 0;
	public int scorePointsLose = 50;

	private GameObject defenderParent;
	private StarDisplay starDisplay;
	
	void Start() 
	{
		defenderParent = GameObject.Find( "Defenders" );
		if ( ! defenderParent ) {
			defenderParent = new GameObject( "Defenders" );
		}
		
		starDisplay = GameObject.FindObjectOfType<StarDisplay>();
		if ( ! starDisplay ) {
			Debug.Log( "StarDisplay not found!" );
		}
		
		Health h = GetComponent<Health>();
		PlayerPrefsManager.Difficulty difficulty = PlayerPrefsManager.GetDifficulty();
		switch ( difficulty ) {
		case PlayerPrefsManager.Difficulty.EASY:
			h.health = h.health * 2;
			scorePointsLose /= 2;
			break;
		case PlayerPrefsManager.Difficulty.HARD:
			h.health = h.health / 2;
			scorePointsLose *= 2;
			starCost += 50;
			break;
		}
	}

	public void AddStars( int amount )
	{
		starDisplay.AddStars( amount );
	}
}
