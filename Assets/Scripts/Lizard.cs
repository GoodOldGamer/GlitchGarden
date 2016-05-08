using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Attacker) )]
public class Lizard : MonoBehaviour 
{
	private Attacker attackerComponent;
	private Animator animComponent;

	// Use this for initialization
	void Start() 
	{
		attackerComponent = gameObject.GetComponent<Attacker>();
		animComponent = gameObject.GetComponent<Animator>();
	}
	
	void OnTriggerEnter2D( Collider2D collider )
	{
		//Debug.Log( name + " colided with " + collider );
		
		GameObject obj = collider.gameObject;
		if ( ! obj.GetComponent<Defender>() ) {
			return; 
		}

        if ( obj.transform.position.x > transform.position.x ) {
            // enemy is behind
            return;
        }

		animComponent.SetBool( "IsAttacking", true );
		attackerComponent.Attack( obj );
	}
}
