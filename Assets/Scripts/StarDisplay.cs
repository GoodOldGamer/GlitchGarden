using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class StarDisplay : MonoBehaviour 
{
	public enum Status {
		SUCCESS,
		FAILURE
	};
	
	public int starCnt = 200;

	private Text myText;
	private ScoreManager scoreManager;
	private bool scoreForStars = true;

	// Use this for initialization
	void Start() 
	{
		myText = GetComponent<Text>();	
		UpdateDisplay();
		
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		if ( ! scoreManager ) {
			Debug.Log( "ScoreManager not found!" );
		}
		
		scoreForStars = PlayerPrefsManager.GetDifficulty() != PlayerPrefsManager.Difficulty.EASY;
	}
	
	public void AddStars( int amount )
	{
		starCnt += amount;
		UpdateDisplay();
		
		if ( scoreForStars ) {
			scoreManager.AddScorePoints( (amount * 10) / 100 );
		}
	}
	
	public Status UseStars( int amount )
	{
		if ( amount > starCnt ) {
			return Status.FAILURE;
		}
		
		starCnt -= amount;
		UpdateDisplay();
		return Status.SUCCESS;
	}
	
	private void UpdateDisplay()
	{
		if ( myText ) {
			myText.text = starCnt.ToString();
		}
	}
}
