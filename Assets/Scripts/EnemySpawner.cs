using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour 
{
	[System.Serializable]
	public struct EnemyObjects
	{
		public GameObject 	prefab;
		public int  		count;
	}

	public EnemyObjects[] enemyObjects;
	
	public float spawnStartDelay = 30;
	public float minSpawnDelay = 2;
	public float maxSpawnDelay = 10;

	private System.DateTime spawningTime;
	private List<GameObject> spawnerLanes;
	private bool isActive = true;
	
	private int curEnemyCnt = 0;
    private int enemyCntQuarter = 0;
    private int maxSpawnMulti = 1;
    private int attackLevel = 0;
	
	public void SetActive( bool active )
	{
		isActive = active;
	}
			
	void Start() 
	{
		spawningTime = System.DateTime.Now + System.TimeSpan.FromSeconds( spawnStartDelay );
		
		spawnerLanes = new List<GameObject>();
		foreach( Transform child in this.transform ) {
			EnemySpawnerLane lane = child.gameObject.GetComponent<EnemySpawnerLane>();
			if ( lane ) {
				spawnerLanes.Add( child.gameObject );
			}	
		}
		
		EnemyDisplay enemyDisplay = GameObject.FindObjectOfType<EnemyDisplay>();
		if ( ! enemyDisplay ) {
			Debug.Log( "EnemyDisplay not found!" );
		}
		else {
			curEnemyCnt = 0;
			foreach( EnemyObjects enemyObject in enemyObjects ) {
				curEnemyCnt += enemyObject.count;
			}
            enemyCntQuarter = curEnemyCnt / 4;
            enemyDisplay.SetEnemyCount( curEnemyCnt );
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		if ( isActive && Time.timeScale != 0 ) {
			if ( IsTimeToSpawn() ) {
                SpawnEnemies();
			}
            UpdateAttackLevel();
		}
        else {
            // adjust spawn time when in game menu
            spawningTime.AddSeconds( Time.deltaTime );
        }
	}

    void UpdateAttackLevel()
    {
        // make it a little harder after some enemies passed
        if ( attackLevel == 0 ) {
            if ( curEnemyCnt <= (3*enemyCntQuarter) ) {
                attackLevel = 1;
                ++maxSpawnMulti;
            }
        }
        if ( attackLevel == 1 ) {
            if ( curEnemyCnt <= (2*enemyCntQuarter) ) {
                attackLevel = 2;
                ++maxSpawnMulti;
                minSpawnDelay /= 2;
                maxSpawnDelay /= 2;
            }
        }
        if ( attackLevel == 2 ) {
            if ( curEnemyCnt <= enemyCntQuarter ) {
                attackLevel = 3;
                ++maxSpawnMulti;
            }
        }
    }
	
	void ResetTimeToSpawn()
	{
		float spawnDelay = Random.Range( minSpawnDelay, maxSpawnDelay );
		spawningTime = System.DateTime.Now + System.TimeSpan.FromSeconds( spawnDelay );
	}
	
	bool IsTimeToSpawn()
	{
		if ( System.DateTime.Now > spawningTime ) {
			ResetTimeToSpawn();
			return true;
		}
		return false;
	}

    void SpawnEnemies()
    {
        int spawnCnt = GetSpawnMulti();
        for ( int i = 0; i < spawnCnt; ++i ) {
            GameObject prefab = GetEnemyPrefab();
            if ( prefab ) {
                EnemySpawnerLane lane = GetSpawnerLane();
                lane.Spawn( prefab );
                --curEnemyCnt;
            }
        }
    }
	
	GameObject GetEnemyPrefab()
	{
		int idx = GetNextPossibleEnemyPrefabIndex();
		if ( idx >= 0 ) {
			--enemyObjects[idx].count;
			return enemyObjects[idx].prefab;
		}
		return null;
	}
	
	int GetNextPossibleEnemyPrefabIndex()
	{
		int idx = Random.Range( 0, 1000 ) % enemyObjects.Length;
		if ( enemyObjects[idx].count > 0 ) {
			return idx;
		}
		
		for ( idx = 0; idx < enemyObjects.Length; ++idx ) {
			if ( enemyObjects[idx].count > 0 ) {
				return idx;
			}	
		}
		return -1;
	}
	
	EnemySpawnerLane GetSpawnerLane()
	{
		int idx = Random.Range( 0, 1000 ) % spawnerLanes.Count;
		return spawnerLanes[idx].GetComponent<EnemySpawnerLane>();
	}

    int GetSpawnMulti()
    {
        if ( curEnemyCnt <= 0 ) return 0;
        int msm = Mathf.Min( maxSpawnMulti, curEnemyCnt );
        return (Random.Range( 0, 1000 ) % msm) + 1;
    }
}

