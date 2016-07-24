using UnityEngine;
using System.Collections;

public class LinkController : MonoBehaviour 
{
    public void OpenURL( string url )
    {
        Application.OpenURL ( url );
    }
}
