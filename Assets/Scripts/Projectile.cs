using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody2D) )]
public class Projectile : MonoBehaviour 
{
	public float speed = 1f;
	public float damage = 10f;
	public AudioClip hitSound;

	// Update is called once per frame
	void Update () 
	{
		transform.Translate( Vector3.right * speed * Time.deltaTime );
	}
	
	void OnTriggerEnter2D( Collider2D collider )
	{
		//Debug.Log( name + " colided with " + collider );
		
		GameObject obj = collider.gameObject;
		if ( ! obj.GetComponent<Attacker>() ) {
			return; 
		}
		
		if ( hitSound ) {
			AudioSource.PlayClipAtPoint( hitSound, transform.position, PlayerPrefsManager.GetCurrentSfxVolume() );
		}
		else {
			Debug.Log( this.name + ": Hit sound missing!" );
		}
		
		Health h = obj.GetComponent<Health>();
		if ( h ) {
			h.DealDamage( damage );
			Destroy( this.gameObject );
		}
		else {
			Debug.Log( obj + ": No healt component found!" );
		}
	}
}
