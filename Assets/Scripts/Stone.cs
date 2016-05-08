using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour 
{
	private Animator animComponent;
	
	// Use this for initialization
	void Start() 
	{
		animComponent = gameObject.GetComponent<Animator>();
	}
	
	void OnTriggerStay2D( Collider2D collider )
	{
		//Debug.Log( name + " colided with " + collider );
		
		GameObject obj = collider.gameObject;
		if ( ! obj.GetComponent<Attacker>() ) {
			return; 
		}
		
		animComponent.SetTrigger( "UnderAttackTrigger" );
	}
}
