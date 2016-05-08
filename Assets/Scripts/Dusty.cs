using UnityEngine;
using System.Collections.Generic;

[RequireComponent( typeof(Defender) )]
public class Dusty : MonoBehaviour 
{
	public GameObject explosionVisualPrefab;
	public AudioClip explosionSound;
	
	private float currentSpeed = 0;
	private Animator animComponent;
	private EnemySpawnerLane myLaneSpawner;
	
	private List<EnemySpawnerLane> spawnerLanes;
	
	// Use this for initialization
	void Start() 
	{
		animComponent = GameObject.FindObjectOfType<Animator>();
		if ( ! animComponent ) {
			Debug.Log( "No animator found!!" );
		}
		
		SetSpawnerLanes();
	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.Translate (Vector3.right * currentSpeed * Time.deltaTime);
		
		if ( animComponent ) {
			animComponent.SetBool( "HasEnemySpotted", isAttackerAheadInLane() );
			animComponent.SetBool( "IsAttacking", isAttackerInRange() );
		}
	}
	
	public void SetSpeed( float speed )
	{
		currentSpeed = speed;
	}
	
	public void Explode()
	{
		ShowExplosion();
		
		foreach ( EnemySpawnerLane enemySpawner in spawnerLanes ) {
			if ( enemySpawner.transform.childCount == 0 ) {
				continue;
			}
			
			foreach ( Transform child in enemySpawner.transform ) {
				if ( child.transform.position.x >= (this.transform.position.x - 1) &&
				     child.transform.position.x <= (this.transform.position.x + 1) ) 
				{
					DamageEnemy( child.gameObject );
				}
			}
		}
		
		Destroy( this.gameObject );
	}
	
	private void DamageEnemy( GameObject obj )
	{
		if ( ! obj.GetComponent<Attacker>() ) return; 
		
		Health h = obj.GetComponent<Health>();
		if ( ! h ) return;
		
		h.DealDamage( 10000 );
	}
	
	void SetSpawnerLanes()
	{
		spawnerLanes = new List<EnemySpawnerLane>();
		
		EnemySpawnerLane[] spawners = GameObject.FindObjectsOfType<EnemySpawnerLane>();
		foreach ( EnemySpawnerLane enemySpawner in spawners ) {
			if ( enemySpawner.transform.position.y >= (this.transform.position.y - 1) &&
			     enemySpawner.transform.position.y <= (this.transform.position.y + 1) ) 
			{
				spawnerLanes.Add( enemySpawner );
			}
		}
		
		if ( spawnerLanes.Count == 0 ) {
			Debug.Log( name + ": No spawner found for this lane!" );
		}
	}
	
	bool isAttackerAheadInLane()
	{
		if ( spawnerLanes.Count == 0 ) return false;
		
		foreach ( EnemySpawnerLane enemySpawner in spawnerLanes ) {
			if ( enemySpawner.transform.childCount == 0 ) {
				continue;
			}
			
			foreach ( Transform child in enemySpawner.transform ) {
				if ( child.transform.position.x > this.transform.position.x ) {
					return true;
				}
			}
		}	
		return false;
	}
	
	bool isAttackerInRange()
	{
		foreach ( EnemySpawnerLane enemySpawner in spawnerLanes ) {
			if ( enemySpawner.transform.childCount == 0 ) {
				continue;
			}
			
			foreach ( Transform child in enemySpawner.transform ) {
				if ( child.transform.position.x >= (this.transform.position.x - 1) &&
				    child.transform.position.x <= (this.transform.position.x + 1) ) 
				{
					return true;
				}
			}
		}
		return false;
	}
	
	void ShowExplosion()
	{
		AudioSource.PlayClipAtPoint( explosionSound, transform.position, PlayerPrefsManager.GetCurrentSfxVolume() );
		GameObject showExplosion = Instantiate( explosionVisualPrefab, gameObject.transform.position, Quaternion.identity ) as GameObject;
		showExplosion.transform.parent = null;
		showExplosion.GetComponent<ParticleSystem>().startColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
	}
}
