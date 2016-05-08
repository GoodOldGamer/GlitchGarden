using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour 
{
	public Camera myCamera;
	
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
	}
	
	void OnMouseDown()
	{
		Vector2 spawnPos = SnapToGrid( CalcMousePosWorldPointCoord() );		
		//Debug.Log( "Clicked: " + spawnPos.ToString() );
		
		if ( ! IsSpawnPointempty(spawnPos) ) {
			return;
		}
		
		SpawnDefender( spawnPos );
	}
	
	void SpawnDefender( Vector2 spawnPos )
	{
		// Spawn the Defender
		GameObject defenderPrefab = Button.selectedDefender;	
		int defenderCosts = defenderPrefab.GetComponent<Defender>().starCost;
		
		if ( starDisplay.UseStars(defenderCosts) == StarDisplay.Status.SUCCESS ) {
			GameObject newDefender = Instantiate( defenderPrefab, spawnPos, Quaternion.identity ) as GameObject;
			newDefender.transform.parent = defenderParent.transform;	
		}
		//		else {
		//			Debug.Log( "Insufficient stars for " + defenderPrefab.name );
		//		}
	}
	
	Vector2 SnapToGrid( Vector2 rawWorldPos )
	{
		int newX = Mathf.RoundToInt( rawWorldPos.x );
		int newY = Mathf.RoundToInt( rawWorldPos.y );
		//Debug.Log( "Clicked on " + newX.ToString() + ", " + newY.ToString() );
		return new Vector2( newX, newY );
	}
	
	Vector2 CalcMousePosWorldPointCoord()
	{
		Vector3 pos = new Vector3( Input.mousePosition.x, Input.mousePosition.y, 10 ); // Position and distance
		Vector2 worldPos = myCamera.ScreenToWorldPoint( pos );
		return worldPos;
	}
	
	bool IsSpawnPointempty( Vector2 spawnPos )
	{
		foreach ( Transform child in defenderParent.transform ) {
			if ( child.transform.position.x == spawnPos.x && child.transform.position.y == spawnPos.y ) {
				return false;
			}
		}		
		return true;
	}
}
