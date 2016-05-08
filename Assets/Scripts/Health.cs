using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float health = 100f;
	
	public AudioClip dieSound;
	public GameObject dieVisualPrefab;
	
	private ScoreManager scoreManager;
	
	void Start()
	{
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		if ( ! scoreManager ) {
			Debug.Log( "ScoreManager not found!" );
		}
	}

	public void DealDamage( float damage )
	{
		health -= damage;
		if ( health <= 0f ) {
			DestroyObject();
		}
	}

	public void DestroyObject()
	{
		ShowDieVisuals();
		
		Attacker attacker = GetComponent<Attacker>();
		if ( attacker ) {
			scoreManager.AddScorePoints( attacker.scorePoints );
		}
		
		Defender defender = GetComponent<Defender>();
		if ( defender ) {
			scoreManager.RemoveScorePoints( defender.scorePointsLose );
		}
		
		Destroy( this.gameObject );
	}
	
	private void ShowDieVisuals()
	{
		if ( dieSound ) {
			AudioSource.PlayClipAtPoint( dieSound, gameObject.transform.position, PlayerPrefsManager.GetCurrentSfxVolume() );
		}
		
		if ( dieVisualPrefab ) {
			GameObject dieVisual = Instantiate( dieVisualPrefab, gameObject.transform.position, Quaternion.identity ) as GameObject;
			dieVisual.transform.parent = null;
			dieVisual.GetComponent<ParticleSystem>().startColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
		}
	}
}
