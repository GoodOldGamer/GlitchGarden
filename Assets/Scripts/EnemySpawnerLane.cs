using UnityEngine;
using System.Collections;

public class EnemySpawnerLane : MonoBehaviour 
{
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere( this.transform.position, 0.5f );
	}

	public void Spawn( GameObject attackerPrefab )
	{
		// Spawn the Defender
		GameObject newAttacker = Instantiate( attackerPrefab ) as GameObject;
		newAttacker.transform.parent = this.transform;
		newAttacker.transform.position = this.transform.position;
	}
}
