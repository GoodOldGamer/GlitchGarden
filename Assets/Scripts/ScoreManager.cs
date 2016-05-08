using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	private Text scoreText;	
	private static int scoreCnt = 0;
	
	void Start()
	{
		scoreText = GetComponent<Text>();
		UpdateScoreText();
	}

	public static void Reset()
	{
		scoreCnt = 0;
	}
	
	public void AddScorePoints( int points )
	{
		scoreCnt += points;
		UpdateScoreText();
	}
	
	public void RemoveScorePoints( int points )
	{
		scoreCnt -= points;
		if ( scoreCnt < 0 ) {
			scoreCnt = 0;
		}
		UpdateScoreText();	
	}
	
	public static int GetScore()
	{
		return scoreCnt;
	}
	
	void UpdateScoreText()
	{
		if ( scoreText ) {
			scoreText.text = scoreCnt.ToString();
		}
	}
}
