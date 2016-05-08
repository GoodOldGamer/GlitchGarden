using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour 
{
	public GameObject menu;

	void OnMouseDown()
	{
		Time.timeScale = 0; // pause the game
		menu.SetActive( true );
	}
}
