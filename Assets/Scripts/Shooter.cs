using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour 
{
	public AudioClip throwSound;
	public GameObject projectilePrefab;
	public GameObject gun;

	private GameObject projectileParent;
	private Animator shooterAnimator;
	
	private EnemySpawnerLane myLaneSpawner;
	
	void Start() 
	{
		projectileParent = GameObject.Find( "Projectiles" );
		if ( ! projectileParent ) {
			projectileParent = new GameObject( "Projectiles" );
		}
		
		shooterAnimator = GameObject.FindObjectOfType<Animator>();
		if ( ! shooterAnimator ) {
			Debug.Log( "No animator found!!" );
		}
		
		SetMyLaneSpawner();
	}
	
	void Update()
	{
		if ( shooterAnimator ) {
			shooterAnimator.SetBool( "IsAttacking", isAttackerAheadInLane() );
		}
	}

	public void FireProjectile()
	{
		if ( ! projectilePrefab ) return;
		
		GameObject projectile = Instantiate( projectilePrefab ) as GameObject;
		projectile.transform.parent = projectileParent.transform;
		projectile.transform.position = gun.transform.position;
		
		if ( throwSound ) {
			AudioSource.PlayClipAtPoint( throwSound, transform.position, PlayerPrefsManager.GetCurrentSfxVolume() );
		}
		else {
			Debug.Log( this.name + ": Throw sound missing!" );
		}
	}
	
	void SetMyLaneSpawner()
	{
		EnemySpawnerLane[] spawners = GameObject.FindObjectsOfType<EnemySpawnerLane>();
		foreach ( EnemySpawnerLane enemySpawner in spawners ) {
			if ( enemySpawner.transform.position.y == this.transform.position.y ) {
				myLaneSpawner = enemySpawner;
				return;
			}
		}
		
		Debug.Log( name + ": No spawner found for this lane!" );
	}
	
	bool isAttackerAheadInLane()
	{
		if ( ! myLaneSpawner ) return false;
		if ( myLaneSpawner.transform.childCount == 0 ) return false;
		
		foreach ( Transform child in myLaneSpawner.transform ) {
			if ( child.transform.position.x > this.transform.position.x ) {
				return true;
			}
		}		
		return false;
	}
}
