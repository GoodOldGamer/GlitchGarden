using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour 
{
	public GameObject defenderPrefab;
	public static GameObject selectedDefender;

	private Button[] buttonArray;
	private Text starCosts;
	
	void Start() 
	{
		buttonArray = GameObject.FindObjectsOfType<Button>();
		
		starCosts = GetComponentInChildren<Text>();
		if ( ! starCosts ) {
			Debug.Log( name + ": costs text missing!" );
		}
		
		Defender thisDefender = defenderPrefab.GetComponent<Defender>();
		starCosts.text = thisDefender.starCost.ToString();
	}
	
	void OnMouseDown()
	{
		foreach ( Button thisButton in buttonArray ) {
			thisButton.GetComponent<SpriteRenderer>().color = Color.black;
		}
		
		GetComponent<SpriteRenderer>().color = Color.white;
		selectedDefender = defenderPrefab;
	}
}
