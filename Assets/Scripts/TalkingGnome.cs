using UnityEngine;
using System.Collections;

[RequireComponent( typeof(AudioSource) )]
public class TalkingGnome : MonoBehaviour 
{
	private AudioSource audioSrc;
	
	// Use this for initialization
	void Start()
	{
		audioSrc = GetComponent<AudioSource>();
		audioSrc.volume = PlayerPrefsManager.GetCurrentSfxVolume();
	}
}
