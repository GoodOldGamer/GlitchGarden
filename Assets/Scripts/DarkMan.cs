using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Attacker) )]
public class DarkMan : MonoBehaviour 
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
