using UnityEngine;
using System.Collections;

[RequireComponent( typeof(ParticleSystem) )]
public class ParticleAutoDestroy : MonoBehaviour 
{
	private ParticleSystem ps;

	// Use this for initialization
	void Start () 
	{
		ps = GetComponent<ParticleSystem>();		
		
		if( ps.loop ) {
			Debug.Log( "ParticleSystem is looping!" );
		}
		else {
			StartCoroutine( InitialWait() );
		}
	}
	
	private IEnumerator InitialWait()
	{
		yield return new WaitForSeconds( ps.duration );
		StartCoroutine( IsAlive() );
	}
	
	private IEnumerator IsAlive()
	{
		while ( ps.IsAlive() ) {
			yield return new WaitForSeconds( 0.5f );
		}
		
		Destroy( this.gameObject );
	}
}
