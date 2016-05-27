using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int numberOfEnemies;

	public override void OnStartServer()
    {
	    for(int i = 0; i < numberOfEnemies; ++i)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));
            var spawnRos = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, spawnRos) as GameObject;
            NetworkServer.Spawn(enemy);
        }
	}

	void Update () {
	
	}
}
