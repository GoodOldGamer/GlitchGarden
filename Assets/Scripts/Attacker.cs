using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody2D) )]
[RequireComponent( typeof(Health) )]
public class Attacker : MonoBehaviour 
{
	public AudioClip strikeSound;
	public int scorePoints = 50;
	
	private float currentSpeed;
	private GameObject currentTarget;
	private Animator animComponent;
	private EnemyDisplay enemyDisplay;
	
	// Use this for initialization
	void Start() 
	{
		animComponent = gameObject.GetComponent<Animator>();
		
		enemyDisplay = GameObject.FindObjectOfType<EnemyDisplay>();
		if ( ! enemyDisplay ) {
			Debug.Log( "EnemyDisplay not found!" );
		}
		
		Health h = GetComponent<Health>();
		PlayerPrefsManager.Difficulty difficulty = PlayerPrefsManager.GetDifficulty();
		switch ( difficulty ) {
		case PlayerPrefsManager.Difficulty.EASY:
			h.health = h.health / 2;
			scorePoints /= 2;
			break;
		case PlayerPrefsManager.Difficulty.HARD:
			h.health = h.health * 2;
			scorePoints *= 2;
			break;
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.Translate (Vector3.left * currentSpeed * Time.deltaTime);

		if ( ! currentTarget ) {
			animComponent.SetBool( "IsAttacking", false );
		}
	}

	public void SetSpeed( float speed )
	{
		currentSpeed = speed;
	}

	public void StrikeCurrentTarget( float damage )
	{
		if ( ! currentTarget ) return;
		
		if ( strikeSound ) {
			AudioSource.PlayClipAtPoint( strikeSound, transform.position, PlayerPrefsManager.GetCurrentSfxVolume() );
		}
		else {
			Debug.Log( this.name + ": Strike sound missing!" );
		}

		Health h = currentTarget.GetComponent<Health>();
		if ( ! h ) return;

		h.DealDamage( 50 );
	}

	public void Attack( GameObject target )
	{
		//Debug.Log( name + " attacking " + target );
		currentTarget = target;
	}
	
	void OnDestroy()
	{
		enemyDisplay.RemoveEnemy();
	}	
}
