using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody2D) )]
public class Piggy : MonoBehaviour 
{
	public AudioClip runningSound;
	public AudioClip hitSound;
	public int scorePointsLose = 200;
	
	private float currentSpeed;
	private Animator animComponent;
	private ScoreManager scoreManager;
	private bool removePoints = false;
	
	// Use this for initialization
	void Start() 
	{
		animComponent = gameObject.GetComponent<Animator>();
		
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		if ( ! scoreManager ) {
			Debug.Log( "ScoreManager not found!" );
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.Translate (Vector3.right * currentSpeed * Time.deltaTime);
	}
	
	public void SetSpeed( float speed )
	{
		currentSpeed = speed;
	}
	
	void OnTriggerEnter2D( Collider2D collider )
	{
		GameObject obj = collider.gameObject;
		if ( ! obj.GetComponent<Attacker>() ) {
			return; 
		}
		
		if ( animComponent.GetBool( "IsAttacking" ) == false ) {
			PlaySound( runningSound );
			animComponent.SetBool( "IsAttacking", true );
			removePoints = true;
		}
		
		PlaySound( hitSound );
		
		Health h = obj.GetComponent<Health>();
		if ( ! h ) {
			Debug.Log( "Attacker " + obj.name + " missing Health component!" );
		}
		h.DealDamage( 99999 );
	}
	
	void PlaySound( AudioClip clip ) 
	{
		if ( clip ) {
			AudioSource.PlayClipAtPoint( clip, transform.position, PlayerPrefsManager.GetCurrentSfxVolume() );
		}
		else {
			Debug.Log( this.name + ": Sound missing!" );
		}
	}
	
	void OnDestroy()
	{
		if ( removePoints ) {
			scoreManager.RemoveScorePoints( scorePointsLose );
		}
	}	
}
