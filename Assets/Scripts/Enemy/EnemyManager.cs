using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<Enemy> spawnableEnemies;

    [HideInInspector] public List<Enemy> waveEnemies = new List<Enemy>();

    [HideInInspector] public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    float interval = 7;
    float currentTime = 0;

    private void Start()
    {
        foreach(Enemy enemy in spawnableEnemies)
        {
            waveEnemies.Add(enemy);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > interval)
        {
            SpawnEnemies();
            currentTime = 0;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            if(spawnableEnemies.Count > 0)
            {
                var enemy = Instantiate(spawnableEnemies[0]);
                enemy.transform.position = enemySpawners[i].transform.position;

                spawnableEnemies.Remove(spawnableEnemies[0]);
            }
        }
    }

    public void CheckWinningStatus()
    {
        if(waveEnemies.Count == 0)
        {
            Debug.Log("Won!");
        }
    }

    
}
