using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class EnemySpawnController : MonoBehaviour
{

    List<Enemy> spawnableEnemy = new List<Enemy>();

    public List<EnemySpawner> spawners = new List<EnemySpawner>();

    [SerializeField] EnemyAmountIndicator[] waveEnemies;

    float spawnInterval = 4f;

    private void Start()
    {
        if (waveEnemies.Length == 0)
        {
            Debug.LogError("List of enemies is empty");
        }
        else
        {
            AddEnemiesToAvailableEnemies();
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnableEnemy.Count > 0)
        {
            float currentTime = 0;
            while (currentTime < spawnInterval)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            SpawnEnemy();

            yield return null;
        }
    }

    void SpawnEnemy()
    {
        EnemySpawner spawner = spawners[Random.Range(0, spawners.Count)];

        int enemyIndex = Random.Range(0, spawnableEnemy.Count);

        Enemy enemy = Instantiate(spawnableEnemy[enemyIndex], spawner.transform).GetComponent<Enemy>();

        GlobalController.instance.levelController.remainingEnemies.Add(enemy);

        spawnableEnemy.RemoveAt(enemyIndex);
    }

    void AddEnemiesToAvailableEnemies()
    {
        spawnableEnemy = new List<Enemy>();

        foreach (EnemyAmountIndicator indicator in waveEnemies)
        {
            for (int i = 0; i < indicator.amount; i++)
            {
                spawnableEnemy.Add(indicator.enemyType);
            }
        }
    }

    [System.Serializable]
    class EnemyAmountIndicator
    {
        public Enemy enemyType;
        public int amount;
    }


}
