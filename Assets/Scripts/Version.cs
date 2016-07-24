using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text) )]
public class Version : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
	    Text text = GetComponent<Text>();
        text.text = "Version " + Application.version;
	}

}
